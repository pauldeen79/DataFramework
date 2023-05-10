namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToQueryClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToQueryClassBuilder(settings).BuildTyped();

    public static ClassBuilder ToQueryClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
    {
        var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(settings.DefaultRenderMetadataAsAttributes);

        return new ClassBuilder()
            .WithName($"{instance.Name}Query")
            .WithNamespace(instance.GetQueriesNamespace())
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(Queries.Visibility, () => instance.IsVisible.ToVisibility()))
            .WithBaseClass(typeof(SingleEntityQuery))
            .AddInterfaces(instance.Metadata
                .Where(md => md.Name == Queries.Interface)
                .Select(md => md.Value.ToStringWithNullCheck().FixGenericParameter(instance.GetEntityFullName())))
            .AddInterfaces(typeof(IValidatableObject).FullName)
            .WithRecord()
            .AddFields(GetQueryClassFields(instance))
            .AddMethods(GetQueryClassMethods(instance))
            .AddConstructors(GetQueryClassConstructors())
            .AddAttributes(GetQueryClassAttributes(instance, renderMetadataAsAttributes));
    }

    private static IEnumerable<ClassFieldBuilder> GetQueryClassFields(IDataObjectInfo instance)
    {
        yield return new ClassFieldBuilder()
            .WithName("ValidFieldNames")
            .WithType(typeof(string[]))
            .WithStatic()
            .WithReadOnly()
            .WithDefaultValue(new global::ModelFramework.Common.Literal("new[] { " + string.Join(", ", GetQueryClassValidFieldNames(instance).Select(s => string.Format("\"{0}\"", s))) + " }"));

        yield return new ClassFieldBuilder()
            .WithName("MaxLimit")
            .WithType(typeof(int))
            .WithConstant()
            .WithDefaultValue(new global::ModelFramework.Common.Literal(GetQueryMaxLimit(instance)));
    }

    private static IEnumerable<string> GetQueryClassValidFieldNames(IDataObjectInfo instance)
    {
        foreach (var s in instance.Fields.Where(x => x.UseOnSelect()).Select(f => f.Name))
        {
            yield return s;
        }

        foreach (var md in instance.Metadata.Where(x => x.Name == Queries.ValidFieldName))
        {
            var value = md.Value.ToStringWithNullCheck();
            if (!string.IsNullOrEmpty(value))
            {
                yield return value;
            }
        }
    }

    private static string GetQueryMaxLimit(IDataObjectInfo instance)
    {
        var maxLimit = instance.Metadata.GetValue<int?>(Queries.MaxLimit, () => null);
        return maxLimit == null
            ? "int.MaxValue"
            : maxLimit.Value.ToString(CultureInfo.InvariantCulture);
    }

    private static IEnumerable<ClassMethodBuilder> GetQueryClassMethods(IDataObjectInfo instance)
    {
        var validationResultType = typeof(ValidationResult).FullName;
        yield return new ClassMethodBuilder()
            .WithName(nameof(IValidatableObject.Validate))
            .WithVisibility(Visibility.Public)
            .WithType(typeof(IEnumerable<ValidationResult>))
            .AddParameter("validationContext", typeof(ValidationContext))
            .AddLiteralCodeStatements
            (
                @"if (Limit.HasValue && Limit.Value > MaxLimit)",
                @"{",
                $@"    yield return new {validationResultType}(""Limit exceeds the maximum of "" + MaxLimit, new[] {{ nameof(Limit), nameof(Limit) }});",
                @"}",
                @"foreach (var condition in Filter.Conditions)",
                @"{",
                @"    if (!IsValidExpression(condition.LeftExpression))",
                @"    {",
                $@"        yield return new {validationResultType}(""Invalid left expression in conditions: "" + condition.LeftExpression, new[] {{ nameof(Filter), nameof(Filter) }});",
                @"    }",
                @"    if (!IsValidExpression(condition.RightExpression))",
                @"    {",
                $@"        yield return new {validationResultType}(""Invalid right expression in conditions: "" + condition.RightExpression, new[] {{ nameof(Filter), nameof(Filter) }});",
                @"    }",
                @"}",
                @"foreach (var querySortOrder in OrderByFields)",
                @"{",
                @"    if (!IsValidExpression(querySortOrder.FieldNameExpression))",
                @"    {",
                $@"        yield return new {validationResultType}(""Invalid expression in order by fields: "" + querySortOrder.FieldNameExpression, new[] {{ nameof(OrderByFields), nameof(OrderByFields) }});",
                @"    }",
                @"}"
            );

        var fieldNameStatements = instance.Metadata.GetValues<ICodeStatement>(Queries.ValidFieldNameStatement)
            .Select(x => x.CreateBuilder())
            .ToList();

        if (!fieldNameStatements.Any())
        {
            fieldNameStatements.Add(new LiteralCodeStatementBuilder().WithStatement($"    return ValidFieldNames.Any(s => s.Equals(fieldExpression.FieldName, \"{nameof(StringComparison)}.{nameof(StringComparison.OrdinalIgnoreCase)}\"));"));
        }

        var expressionStatements = instance.Metadata.GetValues<ICodeStatement>(Queries.ValidExpressionStatement)
            .Select(x => x.CreateBuilder())
            .ToList();

        if (!expressionStatements.Any())
        {
            expressionStatements.Add(new LiteralCodeStatementBuilder().WithStatement("return true;"));
        }

        yield return new ClassMethodBuilder()
            .WithName("IsValidExpression")
            .WithVisibility(Visibility.Private)
            .WithType(typeof(bool))
            .AddParameter("expression", typeof(Expression))
            .AddLiteralCodeStatements
            (
                "if (expression is IFieldExpression fieldExpression)",
                "{"
            )
            .AddCodeStatements(fieldNameStatements)
            .AddLiteralCodeStatements
            (
                "}"
            )
            .AddCodeStatements(expressionStatements);
    }

    private static IEnumerable<ClassConstructorBuilder> GetQueryClassConstructors()
    {
        yield return new ClassConstructorBuilder()
            .WithChainCall($"this(null, null, new {typeof(ComposedEvaluatable).FullName}({nameof(Enumerable)}.{nameof(Enumerable.Empty)}<{typeof(ComposableEvaluatable).FullName}>()), {nameof(Enumerable)}.{nameof(Enumerable.Empty)}<{typeof(IQuerySortOrder).FullName}>())");

        yield return new ClassConstructorBuilder()
            .AddParameter("limit", typeof(int?))
            .AddParameter("offset", typeof(int?))
            .AddParameter("filter", typeof(ComposedEvaluatable))
            .AddParameter("orderByFields", typeof(IEnumerable<IQuerySortOrder>))
            .ChainCallToBaseUsingParameters();

        yield return new ClassConstructorBuilder()
            .AddParameter("simpleEntityQuery", typeof(ISingleEntityQuery))
            .WithChainCall("this(simpleEntityQuery.Limit, simpleEntityQuery.Offset, simpleEntityQuery.Filter, simpleEntityQuery.OrderByFields");
    }

    private static IEnumerable<AttributeBuilder> GetQueryClassAttributes(IDataObjectInfo instance, RenderMetadataAsAttributesTypes renderMetadataAsAttributes)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Queries.QueryGenerator");

        foreach (var attribute in instance.GetClassAttributeBuilderAttributes(renderMetadataAsAttributes, Queries.Attribute))
        {
            yield return attribute;
        }
    }
}
