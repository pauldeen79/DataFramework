namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class SetNameComponentBuilder : IRepositoryInterfaceComponentBuilder
{
    public IPipelineComponent<RepositoryInterfaceContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> Process(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetRepositoryInterfaceFullName(context.Request.Settings.RepositoryInterfaceNamespace));

        return Task.FromResult(Result.Continue());
    }
}
