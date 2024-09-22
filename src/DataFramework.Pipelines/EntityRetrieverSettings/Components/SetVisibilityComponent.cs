namespace DataFramework.Pipelines.EntityRetrieverSettings.Components;

public class SetVisibilityComponentBuilder : IEntityRetrieverSettingsComponentBuilder
{
    public IPipelineComponent<EntityRetrieverSettingsContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<EntityRetrieverSettingsContext>
{
    public Task<Result> Process(PipelineContext<EntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.DatabaseEntityRetrieverSettingsVisibility);

        return Task.FromResult(Result.Continue());
    }
}
