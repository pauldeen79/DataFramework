namespace DataFramework.Pipelines.PagedEntityRetrieverSettings.Components;

public class SetVisibilityComponentBuilder : IPagedEntityRetrieverSettingsComponentBuilder
{
    public IPipelineComponent<PagedEntityRetrieverSettingsContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<PagedEntityRetrieverSettingsContext>
{
    public Task<Result> Process(PipelineContext<PagedEntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.PagedEntityRetrieverSettingsVisibility);

        return Task.FromResult(Result.Continue());
    }
}
