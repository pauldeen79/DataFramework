namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class SetNameComponentBuilder : IRepositoryInterfaceComponentBuilder
{
    public IPipelineComponent<RepositoryInterfaceContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> Process(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"I{context.Request.SourceModel.Name}Repository")
            .WithNamespace(context.Request.Settings.RepositoryInterfaceNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
