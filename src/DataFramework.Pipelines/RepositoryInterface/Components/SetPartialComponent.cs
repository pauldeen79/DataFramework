namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class SetPartialComponentBuilder : IRepositoryInterfaceComponentBuilder
{
    public IPipelineComponent<RepositoryInterfaceContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> Process(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
