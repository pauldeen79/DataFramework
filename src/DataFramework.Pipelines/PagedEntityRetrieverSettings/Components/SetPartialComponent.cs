namespace DataFramework.Pipelines.PagedEntityRetrieverSettings.Components;

public class SetPartialComponent : IPipelineComponent<PagedEntityRetrieverSettingsContext>
{
    public Task<Result> ProcessAsync(PipelineContext<PagedEntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
