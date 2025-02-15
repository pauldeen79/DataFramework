namespace DataFramework.Pipelines.Repository.Components;

public class SetPartialComponent : IPipelineComponent<RepositoryContext>
{
    public Task<Result> ProcessAsync(PipelineContext<RepositoryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
