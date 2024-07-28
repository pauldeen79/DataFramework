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

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}PagedEntityRetrieverSettings")
            .WithNamespace(context.Request.Settings.PagedEntityRetrieverSettingsNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
