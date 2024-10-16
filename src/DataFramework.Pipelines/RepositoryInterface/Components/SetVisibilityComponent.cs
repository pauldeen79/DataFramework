namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class SetVisibilityComponentBuilder : IRepositoryInterfaceComponentBuilder
{
    public IPipelineComponent<RepositoryInterfaceContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> Process(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.RepositoryInterfaceVisibility);

        return Task.FromResult(Result.Continue());
    }
}
