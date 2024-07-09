namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider.Components;

public class SetNameComponentBuilder : IDatabaseEntityRetrieverProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<DatabaseEntityRetrieverProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}DatabaseEntityRetrieverProvider")
            .WithNamespace(context.Request.Settings.DatabaseEntityRetrieverProviderNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
