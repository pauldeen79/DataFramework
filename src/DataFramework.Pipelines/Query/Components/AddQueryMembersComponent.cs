namespace DataFramework.Pipelines.Query.Components;

public class AddQueryMembersComponentBuilder : IQueryComponentBuilder
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddQueryMembersComponentBuilder(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public IPipelineComponent<QueryContext> Build()
        => new AddQueryMembersComponent(_csharpExpressionDumper);
}

public class AddQueryMembersComponent : IPipelineComponent<QueryContext>
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddQueryMembersComponent(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

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
            .WithDefaultValue(new Literal("new[] { " + string.Join(", ", GetQueryClassValidFieldNames(context).Select(s => $"\"{s}\"")) + " }", null));

        yield return new FieldBuilder()
            .WithName("MaxLimit")
            .WithType(typeof(int))
            .WithConstant()
            .WithDefaultValue(new Literal(GetQueryMaxLimit(context), null));
    }

    private static IEnumerable<string> GetQueryClassValidFieldNames(PipelineContext<QueryContext> context)
    {
        foreach (var s in context.Request.SourceModel.Fields.Where(x => x.UseOnSelect).Select(f => f.Name))
        {
            yield return s;
        }

        //foreach (var md in context.Request.SourceModel.Metadata.Where(x => x.Name == Queries.ValidFieldName))
        //{
        //    var value = md.Value.ToStringWithNullCheck();
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        yield return value;
        //    }
        //}
    }

    private static string GetQueryMaxLimit(PipelineContext<QueryContext> context)
    {
        var maxLimit = context.Request.Settings.QueryMaxLimit;
        return maxLimit == null
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

        var fieldNameStatements = new List<CodeStatementBaseBuilder>();
        //var fieldNameStatements = context.Request.SourceModel.Metadata.GetValues<CodeStatementBase>(Queries.ValidFieldNameStatement)
        //    .Select(x => x.ToBuilder())
        //    .ToList();

        if (!fieldNameStatements.Any())
        {
            fieldNameStatements.Add(new StringCodeStatementBuilder().WithStatement($"    return ValidFieldNames.Any(s => s.Equals(fieldExpression.FieldName, \"{nameof(StringComparison)}.{nameof(StringComparison.OrdinalIgnoreCase)}\"));"));
        }

        var expressionStatements = new List<CodeStatementBaseBuilder>();
        //var expressionStatements = context.Request.SourceModel.Metadata.GetValues<ICodeStatement>(Queries.ValidExpressionStatement)
        //    .Select(x => x.ToBuilder())
        //    .ToList();

        if (!expressionStatements.Any())
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
            .WithChainCall("base(limit, offset, filter, orderByFields)"); //TODO: Add ChainCallToBaseUsingParameters method to ClassFramework's ConstructorBuilder class

        yield return new ConstructorBuilder()
            .AddParameter("query", typeof(IQuery))
            .WithChainCall("this(query.Limit, query.Offset, query.Filter, query.OrderByFields");
    }
}
