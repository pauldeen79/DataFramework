namespace DataFramework.Pipelines.Repository.Components;

public class SetVisibilityComponent : IPipelineComponent<RepositoryContext>
{
    public Task<Result> ProcessAsync(PipelineContext<RepositoryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.RepositoryVisibility);

        return Task.FromResult(Result.Continue());
    }
}
