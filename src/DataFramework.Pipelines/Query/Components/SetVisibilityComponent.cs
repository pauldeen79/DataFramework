namespace DataFramework.Pipelines.Query.Components;

public class SetVisibilityComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.QueryVisibility);

        return Task.FromResult(Result.Continue());
    }
}
