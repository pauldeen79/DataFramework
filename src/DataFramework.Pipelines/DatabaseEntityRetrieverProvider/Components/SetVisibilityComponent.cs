namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider.Components;

public class SetVisibilityComponent : IPipelineComponent<DatabaseEntityRetrieverProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<DatabaseEntityRetrieverProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.DatabaseEntityRetrieverProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
