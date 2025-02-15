namespace DataFramework.Pipelines.QueryBuilder.Components;

public class AddQueryBuilderMembersComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        var queryFullName = context.Request.SourceModel.GetQueryFullName(context.Request.Settings.QueryBuilderNamespace);

        context.Request.Builder
            .AddConstructors(
                new ConstructorBuilder().WithChainCall("base()"),
                new ConstructorBuilder().AddParameter("source", typeof(IQuery)).ChainCallToBaseUsingParameters()
            )
            .AddMethods(
                new MethodBuilder().WithName(nameof(IQueryBuilder.Build)).WithOverride().WithReturnType(typeof(IQuery)).AddStringCodeStatements("return BuildTyped();"),
                new MethodBuilder().WithName("BuildTyped").WithReturnTypeName(queryFullName).AddStringCodeStatements($"return new {queryFullName}(Limit, Offset, Filter?.BuildTyped() ?? new {typeof(ComposedEvaluatableBuilder).FullName}().BuildTyped(), OrderByFields?.Select(x => x.Build()) ?? {typeof(Enumerable).FullName}.Empty<{typeof(IQuerySortOrder).FullName}>());")
            )
            .WithBaseClass(typeof(QueryFramework.Core.Builders.QueryBuilder));

        return Task.FromResult(Result.Continue());
    }
}
