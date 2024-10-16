namespace DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProvider.Components;

public class SetNameComponentBuilder : IDatabaseEntityRetrieverSettingsProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverSettingsProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}DatabaseEntityRetrieverSettingsProvider")
            .WithNamespace(context.Request.Settings.DatabaseEntityRetrieverSettingsProviderNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
