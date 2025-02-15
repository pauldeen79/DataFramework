namespace DataFramework.Pipelines.QueryBuilder.Components;

public class SetVisibilityComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.QueryBuilderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
