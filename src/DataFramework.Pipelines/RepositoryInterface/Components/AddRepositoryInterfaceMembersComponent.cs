namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class AddRepositoryInterfaceMembersComponentBuilder : IRepositoryInterfaceComponentBuilder
{
    public IPipelineComponent<RepositoryInterfaceContext> Build()
        => new AddRepositoryInterfaceMembersComponent();
}

public class AddRepositoryInterfaceMembersComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> Process(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        string[] typeArgs =
            !string.IsNullOrEmpty(context.Request.Settings.DefaultIdentityNamespace)
            ? [context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace), context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace)]
            : [context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)];

        context.Request.Builder
            .AddInterfaces(typeof(IRepository<,>).ReplaceGenericTypeName(typeArgs));

        return Task.FromResult(Result.Continue());
    }
}
