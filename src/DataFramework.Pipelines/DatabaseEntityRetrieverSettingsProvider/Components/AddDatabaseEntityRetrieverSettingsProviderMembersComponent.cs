namespace DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProvider.Components;

public class AddDatabaseEntityRetrieverSettingsProviderMembersComponentBuilder : IDatabaseEntityRetrieverSettingsProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext> Build()
        => new AddDatabaseEntityRetrieverSettingsProviderMembersComponent();
}

public class AddDatabaseEntityRetrieverSettingsProviderMembersComponent : IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverSettingsProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(typeof(IDatabaseEntityRetrieverSettingsProvider), typeof(IPagedDatabaseEntityRetrieverSettingsProvider))
            .AddMethods(GetEntityRetrieverSettingsProviderClassMethods(context.Request.SourceModel, context.Request.Settings.DefaultEntityNamespace, context.Request.Settings.DefaultIdentityNamespace, context.Request.Settings.DatabasePagedEntityRetrieverSettingsNamespace, context.Request.Settings.QueryNamespace));

        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<MethodBuilder> GetEntityRetrieverSettingsProviderClassMethods(DataObjectInfo instance, string entityNamespace, string identityNamespace, string entityRetrieverSettingsNamespace, string queryNamespace)
    {
        yield return new MethodBuilder()
            .WithName(nameof(IDatabaseEntityRetrieverSettingsProvider.TryGet))
            .WithReturnType(typeof(bool))
            .WithExplicitInterfaceName(nameof(IDatabaseEntityRetrieverSettingsProvider))
            .AddGenericTypeArguments("TSource")
            .AddParameters(new ParameterBuilder()
                .WithIsOut()
                .WithName("settings")
                .WithType(typeof(IDatabaseEntityRetrieverSettings))
                .WithIsNullable())
            .AddStringCodeStatements(
               $"if (typeof(TSource) == typeof({instance.GetEntityFullName(entityNamespace)}) || typeof(TSource) == typeof({instance.GetEntityIdentityFullName(identityNamespace)}) || typeof(TSource) == typeof({instance.GetQueryFullName(queryNamespace)}))",
                "{",
               $"    settings = new {instance.GetEntityRetrieverSettingsFullName(entityRetrieverSettingsNamespace)}();",
                "    return true;",
                "}",
               $"settings = default;",
                "return false;"
            );

        yield return new MethodBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettingsProvider.TryGet))
            .WithReturnType(typeof(bool))
            .WithExplicitInterfaceName(nameof(IPagedDatabaseEntityRetrieverSettingsProvider))
            .AddGenericTypeArguments("TSource")
            .AddParameters(new ParameterBuilder()
                .WithIsOut()
                .WithName("settings")
                .WithType(typeof(IPagedDatabaseEntityRetrieverSettings))
                .WithIsNullable())
            .AddStringCodeStatements(
               $"if (typeof(TSource) == typeof({instance.GetEntityFullName(entityNamespace)}) || typeof(TSource) == typeof({instance.GetEntityIdentityFullName(identityNamespace)}) || typeof(TSource) == typeof({instance.GetQueryFullName(queryNamespace)}))",
                "{",
               $"    settings = new {instance.GetEntityRetrieverSettingsFullName(entityRetrieverSettingsNamespace)}();",
                "    return true;",
                "}",
               $"settings = default;",
                "return false;"
            );
    }
}
