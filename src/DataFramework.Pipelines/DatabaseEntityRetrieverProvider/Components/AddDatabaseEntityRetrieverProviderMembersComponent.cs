namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider.Components;

public class AddDatabaseEntityRetrieverProviderMembersComponentBuilder : IDatabaseEntityRetrieverProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverProviderContext> Build()
        => new AddDatabaseEntityRetrieverProviderMembersComponent();
}

public class AddDatabaseEntityRetrieverProviderMembersComponent : IPipelineComponent<DatabaseEntityRetrieverProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(typeof(IDatabaseEntityRetrieverProvider))
            .AddFields(GetEntityRetrieverProviderClassFields(context.Request.SourceModel, context.Request.Settings.DefaultEntityNamespace))
            .AddMethods(GetEntityRetrieverProviderClassMethods(context.Request.SourceModel, context.Request.Settings.DefaultEntityNamespace))
            .AddConstructors(GetEntityRetrieverProviderClassConstructors(context.Request.SourceModel, context.Request.Settings.DefaultEntityNamespace));

        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<FieldBuilder> GetEntityRetrieverProviderClassFields(DataObjectInfo instance, string entityNamespace)
    {
        yield return new FieldBuilder()
            .WithName("_databaseEntityRetriever")
            .WithReadOnly()
            .WithTypeName(typeof(IDatabaseEntityRetriever<>).ReplaceGenericTypeName(instance.GetEntityFullName(entityNamespace)));
    }

    private static IEnumerable<MethodBuilder> GetEntityRetrieverProviderClassMethods(DataObjectInfo instance, string entityNamespace)
    {
        yield return new MethodBuilder()
            .WithName("TryCreate")
            .WithReturnType(typeof(bool))
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName("query")
                    .WithType(typeof(IQuery)),
                new ParameterBuilder()
                    .WithName("result")
                    .WithIsNullable()
                    .WithTypeName(typeof(IDatabaseEntityRetriever<>).ReplaceGenericTypeName("TResult"))
                    .WithIsOut()
            )
            .AddStringCodeStatements
            (
                $"if (typeof(TResult) == typeof({instance.GetEntityFullName(entityNamespace)})",
                "{",
                $"    result = ({typeof(IDatabaseEntityRetriever<>).ReplaceGenericTypeName(instance.GetEntityFullName(entityNamespace))})_databaseEntityRetriever;",
                "    return true;",
                "}",
                "result = default;",
                "return false;"
            )
            .AddGenericTypeArguments("TResult")
            .AddGenericTypeArgumentConstraints("where TResult : class");
    }

    private static IEnumerable<ConstructorBuilder> GetEntityRetrieverProviderClassConstructors(DataObjectInfo instance, string entityNamespace)
    {
        yield return new ConstructorBuilder()
            .AddParameter("databaseEntityRetriever", typeof(IDatabaseEntityRetriever<>).ReplaceGenericTypeName(instance.GetEntityFullName(entityNamespace)))
            .AddStringCodeStatements("_databaseEntityRetriever = databaseEntityRetriever;");
    }
}
