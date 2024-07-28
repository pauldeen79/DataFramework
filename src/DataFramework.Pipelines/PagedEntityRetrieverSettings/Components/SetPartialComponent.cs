namespace DataFramework.Pipelines.PagedEntityRetrieverSettings.Components;

public class SetPartialComponentBuilder : IPagedEntityRetrieverSettingsComponentBuilder
{
    public IPipelineComponent<PagedEntityRetrieverSettingsContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<PagedEntityRetrieverSettingsContext>
{
    public Task<Result> Process(PipelineContext<PagedEntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
