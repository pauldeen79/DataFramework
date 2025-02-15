namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class SetVisibilityComponent : IPipelineComponent<QueryFieldInfoContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.QueryFieldInfoVisibility);

        return Task.FromResult(Result.Continue());
    }
}
