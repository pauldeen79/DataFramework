namespace DataFramework.Pipelines.PagedEntityRetrieverSettings.Components;

public class SetVisibilityComponent : IPipelineComponent<PagedEntityRetrieverSettingsContext>
{
    public Task<Result> ProcessAsync(PipelineContext<PagedEntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.DatabasePagedEntityRetrieverSettingsVisibility);

        return Task.FromResult(Result.Continue());
    }
}
