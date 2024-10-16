namespace DataFramework.Pipelines.Repository.Components;

public class AddRepositoryMembersComponentBuilder : IRepositoryComponentBuilder
{
    public IPipelineComponent<RepositoryContext> Build()
        => new AddRepositoryMembersComponent();
}

public class AddRepositoryMembersComponent : IPipelineComponent<RepositoryContext>
{
    public Task<Result> Process(PipelineContext<RepositoryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        string[] typeArgs =
            !string.IsNullOrEmpty(context.Request.Settings.DefaultIdentityNamespace)
            ? [context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace), context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace)]
            : [context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)];

        context.Request.Builder
            .WithBaseClass(typeof(Repository<,>).ReplaceGenericTypeName(typeArgs))
            .AddInterfaces(GetRepositoryClassInterfaces(context))
            .AddConstructors(GetRepositoryClassConstructors(context));

        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<string> GetRepositoryClassInterfaces(PipelineContext<RepositoryContext> context)
    {
        if (context.Request.Settings.UseRepositoryInterface)
        {
            yield return context.Request.SourceModel.GetRepositoryInterfaceFullName(context.Request.Settings.RepositoryInterfaceNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));
        }
    }

    private static IEnumerable<ConstructorBuilder> GetRepositoryClassConstructors(PipelineContext<RepositoryContext> context)
    {
        yield return new ConstructorBuilder()
            .AddParameter("commandProcessor", typeof(IDatabaseCommandProcessor<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)))
            .AddParameter("entityRetriever", typeof(IDatabaseEntityRetriever<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)))
            .AddParameter("identitySelectCommandProvider", typeof(IDatabaseCommandProvider<>).ReplaceGenericTypeName(string.IsNullOrEmpty(context.Request.Settings.DefaultIdentityNamespace)
                ? context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)
                : context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace)))
            .AddParameter("pagedEntitySelectCommandProvider", typeof(IPagedDatabaseCommandProvider))
            .AddParameter("entitySelectCommandProvider", typeof(IDatabaseCommandProvider))
            .AddParameter("entityCommandProvider", typeof(IDatabaseCommandProvider<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)))
            .ChainCallToBaseUsingParameters();
    }
}
