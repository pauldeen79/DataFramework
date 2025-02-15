namespace DataFramework.Pipelines.PagedEntityRetrieverSettings.Components;

public class SetNameComponent : IPipelineComponent<PagedEntityRetrieverSettingsContext>
{
    public Task<Result> ProcessAsync(PipelineContext<PagedEntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetPagedDatabaseEntityRetrieverSettingsFullName(context.Request.Settings.DatabasePagedEntityRetrieverSettingsNamespace));

        return Task.FromResult(Result.Continue());
    }
}
