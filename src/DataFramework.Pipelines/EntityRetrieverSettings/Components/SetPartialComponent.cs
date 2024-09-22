namespace DataFramework.Pipelines.EntityRetrieverSettings.Components;

public class SetPartialComponentBuilder : IEntityRetrieverSettingsComponentBuilder
{
    public IPipelineComponent<EntityRetrieverSettingsContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<EntityRetrieverSettingsContext>
{
    public Task<Result> Process(PipelineContext<EntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
