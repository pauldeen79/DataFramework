namespace DataFramework.Pipelines.PagedEntityRetrieverSettings.Components;

public class SetNameComponentBuilder : IPagedEntityRetrieverSettingsComponentBuilder
{
    public IPipelineComponent<PagedEntityRetrieverSettingsContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<PagedEntityRetrieverSettingsContext>
{
    public Task<Result> Process(PipelineContext<PagedEntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetPagedDatabaseEntityRetrieverSettingsFullName(context.Request.Settings.DatabasePagedEntityRetrieverSettingsNamespace));

        return Task.FromResult(Result.Continue());
    }
}
