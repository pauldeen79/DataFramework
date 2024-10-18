namespace DataFramework.Pipelines.DependencyInjection.Components;

public class AddDependencyInjectionMembersComponentBuilder : IDependencyInjectionComponentBuilder
{
    private readonly IFormattableStringParser _formattableStringParser;

    public AddDependencyInjectionMembersComponentBuilder(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public IPipelineComponent<DependencyInjectionContext> Build()
        => new AddDependencyInjectionMembersComponent(_formattableStringParser);
}

public class AddDependencyInjectionMembersComponent : IPipelineComponent<DependencyInjectionContext>
{
    private readonly IFormattableStringParser _formattableStringParser;

    public AddDependencyInjectionMembersComponent(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public Task<Result> Process(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        var result = _formattableStringParser.Parse(context.Request.Settings.DependencyInjectionMethodName, context.Request.FormatProvider, context.Request);
        if (!result.IsSuccessful())
        {
            return Task.FromResult((Result)result);
        }

        var entityFullName = context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace);
        var identityFullName = context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace);

        string[] typeArgs =
            !string.IsNullOrEmpty(context.Request.Settings.DefaultIdentityNamespace)
            ? [context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace), context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace)]
            : [context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)];

        string[] entityCommandProviderTypeArgs = context.Request.Settings.EntityClassType.IsImmutable()
            ? [context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace), context.Request.SourceModel.GetEntityBuilderFullName(context.Request.Settings.DefaultEntityNamespace, context.Request.Settings.DefaultBuilderNamespace, context.Request.Settings.EntityClassType.IsImmutable())]
            : [context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)];

        var commandEntityProviderFullName = context.Request.SourceModel.GetCommandEntityProviderFullName(context.Request.Settings.CommandEntityProviderNamespace);
        var commandProviderFullName = context.Request.SourceModel.GetCommandProviderFullName(context.Request.Settings.CommandProviderNamespace);
        var identityCommandProviderFullName = context.Request.SourceModel.GetIdentityCommandProviderFullName(context.Request.Settings.IdentityCommandProviderNamespace);
        var queryFieldInfoProviderFullName = context.Request.SourceModel.GetQueryFieldInfoProviderFullName(context.Request.Settings.QueryFieldInfoProviderNamespace);
        var databaseEntityRetrieverProviderFullName = context.Request.SourceModel.GetDatabaseEntityRetrieverProviderFullName(context.Request.Settings.DatabaseEntityRetrieverProviderNamespace);
        var pagedDatabaseEntityRetrieverSettingsProviderFullName = context.Request.SourceModel.GetPagedDatabaseEntityRetrieverSettingsProviderFullName(context.Request.Settings.DatabasePagedEntityRetrieverSettingsNamespace);

        context.Request.Builder
            .AddMethods(new MethodBuilder()
                .WithName(result.Value!.ToString())
                .WithVisibility(context.Request.Settings.DependencyInjectionVisibility)
                .WithStatic()
                .WithExtensionMethod()
                .AddParameter("serviceCollection", typeof(IServiceCollection))
                .WithReturnType(typeof(IServiceCollection))
                .AddStringCodeStatements(
                    $"return {typeof(QueryFramework.SqlServer.Extensions.ServiceCollectionExtensions).FullName}.{nameof(QueryFramework.SqlServer.Extensions.ServiceCollectionExtensions.AddQueryFrameworkSqlServer)}(serviceCollection, x =>",
                    "{",
                    $"    x.AddSingleton<{typeof(IDatabaseEntityRetriever<>).ReplaceGenericTypeName(entityFullName)}, {typeof(DatabaseEntityRetriever<>).ReplaceGenericTypeName(entityFullName)}>();",
                    $"    x.AddScoped<{typeof(IDatabaseCommandProcessor<>).ReplaceGenericTypeName(entityFullName)}, {typeof(DatabaseCommandProcessor<,>).ReplaceGenericTypeName(typeArgs)}>();",
                    $"    x.AddScoped<{typeof(IDatabaseCommandEntityProvider<,>).ReplaceGenericTypeName(entityCommandProviderTypeArgs)}, {commandEntityProviderFullName}>();",
                    $"    x.AddSingleton<{typeof(IDatabaseCommandProvider<>).ReplaceGenericTypeName(entityFullName)}, {commandProviderFullName}>();",
                    $"    x.AddSingleton<{typeof(IDatabaseCommandProvider<>).ReplaceGenericTypeName(identityFullName)}, {identityCommandProviderFullName}>();",
                    $"    x.AddSingleton<{typeof(IQueryFieldInfoProvider)}, {queryFieldInfoProviderFullName}>();",
                    $"    x.AddSingleton<{typeof(IDatabaseEntityRetrieverProvider).FullName}, {databaseEntityRetrieverProviderFullName}>();",
                    $"    x.AddSingleton<{typeof(IDatabaseEntityRetrieverSettingsProvider).FullName}, {pagedDatabaseEntityRetrieverSettingsProviderFullName}>();",
                    $"    x.AddSingleton<{typeof(IPagedDatabaseEntityRetrieverSettingsProvider).FullName}, {pagedDatabaseEntityRetrieverSettingsProviderFullName}>();",
                    //TODO: Add IDatabaseEntityMapper, like x.AddSingleton<IDatabaseEntityMapper<Catalog>, CatalogEntityMapper>();
                    //TODO: Add repository, like x.AddScoped<ICatalogRepository, CatalogRepository>();
                    "});"
                )
            );

        return Task.FromResult(Result.Continue());
    }
}
