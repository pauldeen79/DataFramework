namespace DataFramework.Pipelines.QueryBuilder.Components;

public class SetVisibilityComponentBuilder : IQueryBuilderComponentBuilder
{
    public IPipelineComponent<QueryBuilderContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> Process(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.QueryBuilderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
