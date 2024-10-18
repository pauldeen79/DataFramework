namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider.Components;

public class SetVisibilityComponentBuilder : IDatabaseEntityRetrieverProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverProviderContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<DatabaseEntityRetrieverProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.DatabaseEntityRetrieverProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
