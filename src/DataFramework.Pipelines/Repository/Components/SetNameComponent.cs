namespace DataFramework.Pipelines.Repository.Components;

public class SetNameComponent : IPipelineComponent<RepositoryContext>
{
    public Task<Result> ProcessAsync(PipelineContext<RepositoryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetRepositoryFullName(context.Request.Settings.RepositoryNamespace));

        return Task.FromResult(Result.Continue());
    }
}
