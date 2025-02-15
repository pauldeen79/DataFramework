namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class SetPartialComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> ProcessAsync(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
