namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class SetNameComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> ProcessAsync(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetRepositoryInterfaceFullName(context.Request.Settings.RepositoryInterfaceNamespace));

        return Task.FromResult(Result.Continue());
    }
}
