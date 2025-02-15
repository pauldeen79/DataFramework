namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class SetVisibilityComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> ProcessAsync(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.RepositoryInterfaceVisibility);

        return Task.FromResult(Result.Continue());
    }
}
