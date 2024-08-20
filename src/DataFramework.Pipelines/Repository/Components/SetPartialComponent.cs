namespace DataFramework.Pipelines.Repository.Components;

public class SetPartialComponentBuilder : IRepositoryComponentBuilder
{
    public IPipelineComponent<RepositoryContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<RepositoryContext>
{
    public Task<Result> Process(PipelineContext<RepositoryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
