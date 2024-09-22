namespace DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProvider.Components;

public class SetVisibilityComponentBuilder : IDatabaseEntityRetrieverSettingsProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverSettingsProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.DatabaseEntityRetrieverSettingsProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
