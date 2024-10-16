namespace DataFramework.Pipelines.Repository.Components;

public class SetVisibilityComponentBuilder : IRepositoryComponentBuilder
{
    public IPipelineComponent<RepositoryContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<RepositoryContext>
{
    public Task<Result> Process(PipelineContext<RepositoryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.RepositoryVisibility);

        return Task.FromResult(Result.Continue());
    }
}
