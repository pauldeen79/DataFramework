namespace DataFramework.Pipelines.QueryFieldInfoProvider.Components;

public class SetNameComponentBuilder : IQueryFieldInfoProviderComponentBuilder
{
    public IPipelineComponent<QueryFieldInfoProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<QueryFieldInfoProviderContext>
{
    public Task<Result> Process(PipelineContext<QueryFieldInfoProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}QueryFieldInfoProvider")
            .WithNamespace(context.Request.Settings.QueryFieldInfoProviderNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
