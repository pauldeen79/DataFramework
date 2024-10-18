namespace DataFramework.Pipelines.Repository.Components;

public class SetNameComponentBuilder : IRepositoryComponentBuilder
{
    public IPipelineComponent<RepositoryContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<RepositoryContext>
{
    public Task<Result> Process(PipelineContext<RepositoryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetRepositoryFullName(context.Request.Settings.RepositoryNamespace));

        return Task.FromResult(Result.Continue());
    }
}
