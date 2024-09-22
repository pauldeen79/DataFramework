namespace DataFramework.Pipelines.EntityRetrieverSettings.Components;

public class SetNameComponentBuilder : IEntityRetrieverSettingsComponentBuilder
{
    public IPipelineComponent<EntityRetrieverSettingsContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<EntityRetrieverSettingsContext>
{
    public Task<Result> Process(PipelineContext<EntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}DatabaseEntityRetrieverSettings")
            .WithNamespace(context.Request.Settings.DatabaseEntityRetrieverSettingsNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
