namespace DataFramework.Pipelines.Query.Components;

public class AddQueryMembersComponentBuilder : IQueryComponentBuilder
{
    public IPipelineComponent<QueryContext> Build()
        => new AddQueryMembersComponent();
}

public class AddQueryMembersComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> Process(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(typeof(IValidatableObject))
            .WithRecord(context.Request.Settings.CreateQueryAsRecord)
            .AddFields(GetQueryClassFields(context))
            .AddMethods(GetQueryMethods(context))
            .AddConstructors(GetQueryConstructors())
            .WithBaseClass(typeof(QueryFramework.Core.Query));

        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<FieldBuilder> GetQueryClassFields(PipelineContext<QueryContext> context)
    {
        yield return new FieldBuilder()
            .WithName("ValidFieldNames")
            .WithType(typeof(string[]))
            .WithStatic()
            .WithReadOnly()
            .WithDefaultValue(new StringLiteral("new[] { " + string.Join(", ", GetQueryClassValidFieldNames(context).Select(s => $"\"{s}\"")) + " }"));

        yield return new FieldBuilder()
            .WithName("MaxLimit")
            .WithType(typeof(int))
            .WithConstant()
            .WithDefaultValue(new StringLiteral(GetQueryMaxLimit(context)));
    }

    private static IEnumerable<string> GetQueryClassValidFieldNames(PipelineContext<QueryContext> context)
    {
        foreach (var s in context.Request.SourceModel.Fields.Where(x => x.UseOnSelect).Select(f => f.Name))
        {
            yield return s;
        }

        foreach (var s in context.Request.SourceModel.AdditionalQueryFields)
        {
            yield return s;
        }
    }

    private static string GetQueryMaxLimit(PipelineContext<QueryContext> context)
    {
        var maxLimit = context.Request.Settings.QueryMaxLimit;
        return maxLimit is null
            ? "int.MaxValue"
            : maxLimit.Value.ToString(context.Request.FormatProvider.ToCultureInfo());
    }

    private static IEnumerable<MethodBuilder> GetQueryMethods(PipelineContext<QueryContext> context)
    {
        var validationResultType = typeof(ValidationResult).FullName;
        yield return new MethodBuilder()
            .WithName(nameof(IValidatableObject.Validate))
            .WithVisibility(Visibility.Public)
            .WithReturnType(typeof(IEnumerable<ValidationResult>))
            .AddParameter("validationContext", typeof(ValidationContext))
            .AddStringCodeStatements
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

        var fieldNameStatements = context.Request.SourceModel.QueryFieldNameStatements.Select(x => x.ToBuilder()).ToList();
        if (fieldNameStatements.Count == 0)
        {
            fieldNameStatements.Add(new StringCodeStatementBuilder().WithStatement($"    return ValidFieldNames.Any(s => s.Equals(fieldExpression.FieldName, \"{nameof(StringComparison)}.{nameof(StringComparison.OrdinalIgnoreCase)}\"));"));
        }

        var expressionStatements = context.Request.SourceModel.QueryExpressionStatements.Select(x => x.ToBuilder()).ToList();
        if (expressionStatements.Count == 0)
        {
            expressionStatements.Add(new StringCodeStatementBuilder().WithStatement("return true;"));
        }

        yield return new MethodBuilder()
            .WithName("IsValidExpression")
            .WithVisibility(Visibility.Private)
            .WithReturnType(typeof(bool))
            .AddParameter("expression", typeof(Expression))
            .AddStringCodeStatements
            (
                $"if (expression is {typeof(FieldExpression).FullName} fieldExpression)",
                "{"
            )
            .AddCodeStatements(fieldNameStatements)
            .AddStringCodeStatements
            (
                "}"
            )
            .AddCodeStatements(expressionStatements);
    }

    private static IEnumerable<ConstructorBuilder> GetQueryConstructors()
    {
        yield return new ConstructorBuilder()
            .WithChainCall($"this(null, null, new {typeof(ComposedEvaluatable).FullName}({nameof(Enumerable)}.{nameof(Enumerable.Empty)}<{typeof(ComposableEvaluatable).FullName}>()), {nameof(Enumerable)}.{nameof(Enumerable.Empty)}<{typeof(IQuerySortOrder).FullName}>())");

        yield return new ConstructorBuilder()
            .AddParameter("limit", typeof(int?))
            .AddParameter("offset", typeof(int?))
            .AddParameter("filter", typeof(ComposedEvaluatable))
            .AddParameter("orderByFields", typeof(IEnumerable<IQuerySortOrder>))
            .ChainCallToBaseUsingParameters();

        yield return new ConstructorBuilder()
            .AddParameter("query", typeof(IQuery))
            .WithChainCall("this(query.Limit, query.Offset, query.Filter, query.OrderByFields)");
    }

    private sealed class StringLiteral : IStringLiteral
    {
        public string Value { get; }

        public StringLiteral(string value)
        {
            Value = value.IsNotNull("value");
        }
    }
}
