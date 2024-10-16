namespace DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProvider.Components;

public class SetPartialComponentBuilder : IDatabaseEntityRetrieverSettingsProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverSettingsProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
