namespace DataFramework.Pipelines.QueryFieldInfoProvider.Components;

public class SetVisibilityComponent : IPipelineComponent<QueryFieldInfoProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryFieldInfoProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.QueryFieldInfoProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
