namespace DataFramework.Pipelines.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataFrameworkPipelines(this IServiceCollection services)
        => services
            .AddEntityPipeline();

    private static IServiceCollection AddEntityPipeline(this IServiceCollection services)
        => services
            // Placeholder processors
            .AddScoped<IPlaceholder, DataObjectInfoProcessor>()
            // Class pipeline
            .AddScoped<IPipeline<ClassContext>, Pipeline<ClassContext>>()
            .AddScoped<IPipelineComponent<ClassContext>, Class.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<ClassContext>, Class.Components.AddPropertiesComponent>()
            .AddScoped<IPipelineComponent<ClassContext>, Class.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<ClassContext>, Class.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<ClassContext>, Class.Components.SetRecordComponent>()
            .AddScoped<IPipelineComponent<ClassContext>, Class.Components.SetVisibilityComponent>()
            // CommandEntityProvider pipeline
            .AddScoped<IPipeline<CommandEntityProviderContext>, Pipeline<CommandEntityProviderContext>>()
            .AddScoped<IPipelineComponent<CommandEntityProviderContext>, CommandEntityProvider.Components.AddEntityCommandProviderMembersComponent>()
            .AddScoped<IPipelineComponent<CommandEntityProviderContext>, CommandEntityProvider.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<CommandEntityProviderContext>, CommandEntityProvider.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<CommandEntityProviderContext>, CommandEntityProvider.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<CommandEntityProviderContext>, CommandEntityProvider.Components.SetVisibilityComponent>()
            // CommandProvider pipeline
            .AddScoped<IPipeline<CommandProviderContext>, Pipeline<CommandProviderContext>>()
            .AddScoped<IPipelineComponent<CommandProviderContext>, CommandProvider.Components.AddCommandProviderMembersComponent>()
            .AddScoped<IPipelineComponent<CommandProviderContext>, CommandProvider.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<CommandProviderContext>, CommandProvider.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<CommandProviderContext>, CommandProvider.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<CommandProviderContext>, CommandProvider.Components.SetVisibilityComponent>()
            // DatabaseEntityRetrieverProvider pipeline
            .AddScoped<IPipeline<DatabaseEntityRetrieverProviderContext>, Pipeline<DatabaseEntityRetrieverProviderContext>>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverProviderContext>, DatabaseEntityRetrieverProvider.Components.AddDatabaseEntityRetrieverProviderMembersComponent>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverProviderContext>, DatabaseEntityRetrieverProvider.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverProviderContext>, DatabaseEntityRetrieverProvider.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverProviderContext>, DatabaseEntityRetrieverProvider.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverProviderContext>, DatabaseEntityRetrieverProvider.Components.SetVisibilityComponent>()
            // DatabaseEntityRetrieverSettingsProvider pipeline
            .AddScoped<IPipeline<DatabaseEntityRetrieverSettingsProviderContext>, Pipeline<DatabaseEntityRetrieverSettingsProviderContext>>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>, DatabaseEntityRetrieverSettingsProvider.Components.AddDatabaseEntityRetrieverSettingsProviderMembersComponent>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>, DatabaseEntityRetrieverSettingsProvider.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>, DatabaseEntityRetrieverSettingsProvider.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>, DatabaseEntityRetrieverSettingsProvider.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>, DatabaseEntityRetrieverSettingsProvider.Components.SetVisibilityComponent>()
            // DatabaseSchema pipeline
            .AddScoped<IPipeline<DatabaseSchemaContext>, Pipeline<DatabaseSchemaContext>>()
            .AddScoped<IPipelineComponent<DatabaseSchemaContext>, DatabaseSchema.Components.AddTableComponent>()
            .AddScoped<IPipelineComponent<DatabaseSchemaContext>, DatabaseSchema.Components.AddInsertUpdateDeleteStoredProceduresComponent>()
            // Dependency Injection pipeline
            .AddScoped<IPipeline<DependencyInjectionContext>, Pipeline<DependencyInjectionContext>>()
            .AddScoped<IPipelineComponent<DependencyInjectionContext>, DependencyInjection.Components.AddDependencyInjectionMembersComponent>()
            .AddScoped<IPipelineComponent<DependencyInjectionContext>, DependencyInjection.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<DependencyInjectionContext>, DependencyInjection.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<DependencyInjectionContext>, DependencyInjection.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<DependencyInjectionContext>, DependencyInjection.Components.SetStaticComponent>()
            .AddScoped<IPipelineComponent<DependencyInjectionContext>, DependencyInjection.Components.SetVisibilityComponent>()
            // EntityMapper pipeline
            .AddScoped<IPipeline<EntityMapperContext>, Pipeline<EntityMapperContext>>()
            .AddScoped<IPipelineComponent<EntityMapperContext>, EntityMapper.Components.AddEntityMapperMembersComponent>()
            .AddScoped<IPipelineComponent<EntityMapperContext>, EntityMapper.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<EntityMapperContext>, EntityMapper.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<EntityMapperContext>, EntityMapper.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<EntityMapperContext>, EntityMapper.Components.SetVisibilityComponent>()
            // IdentityClass pipeline
            .AddScoped<IPipeline<IdentityClassContext>, Pipeline<IdentityClassContext>>()
            .AddScoped<IPipelineComponent<IdentityClassContext>, IdentityClass.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<IdentityClassContext>, IdentityClass.Components.AddPropertiesComponent>()
            .AddScoped<IPipelineComponent<IdentityClassContext>, IdentityClass.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<IdentityClassContext>, IdentityClass.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<IdentityClassContext>, IdentityClass.Components.SetRecordComponent>()
            .AddScoped<IPipelineComponent<IdentityClassContext>, IdentityClass.Components.SetVisibilityComponent>()
            // IdentityCommandProvider pipeline
            .AddScoped<IPipeline<IdentityCommandProviderContext>, Pipeline<IdentityCommandProviderContext>>()
            .AddScoped<IPipelineComponent<IdentityCommandProviderContext>, IdentityCommandProvider.Components.AddIdentityCommandProviderMembersComponent>()
            .AddScoped<IPipelineComponent<IdentityCommandProviderContext>, IdentityCommandProvider.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<IdentityCommandProviderContext>, IdentityCommandProvider.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<IdentityCommandProviderContext>, IdentityCommandProvider.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<IdentityCommandProviderContext>, IdentityCommandProvider.Components.SetVisibilityComponent>()
            // PagedEntityRetrieverSettings pipeline
            .AddScoped<IPipeline<PagedEntityRetrieverSettingsContext>, Pipeline<PagedEntityRetrieverSettingsContext>>()
            .AddScoped<IPipelineComponent<PagedEntityRetrieverSettingsContext>, PagedEntityRetrieverSettings.Components.AddPagedEntityRetrieverSettingsMembersComponent>()
            .AddScoped<IPipelineComponent<PagedEntityRetrieverSettingsContext>, PagedEntityRetrieverSettings.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<PagedEntityRetrieverSettingsContext>, PagedEntityRetrieverSettings.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<PagedEntityRetrieverSettingsContext>, PagedEntityRetrieverSettings.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<PagedEntityRetrieverSettingsContext>, PagedEntityRetrieverSettings.Components.SetVisibilityComponent>()
            // Query pipeline
            .AddScoped<IPipeline<QueryContext>, Pipeline<QueryContext>>()
            .AddScoped<IPipelineComponent<QueryContext>, Query.Components.AddQueryMembersComponent>()
            .AddScoped<IPipelineComponent<QueryContext>, Query.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<QueryContext>, Query.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<QueryContext>, Query.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<QueryContext>, Query.Components.SetRecordComponent>()
            .AddScoped<IPipelineComponent<QueryContext>, Query.Components.SetVisibilityComponent>()
            // QueryBuilder pipeline
            .AddScoped<IPipeline<QueryBuilderContext>, Pipeline<QueryBuilderContext>>()
            .AddScoped<IPipelineComponent<QueryBuilderContext>, QueryBuilder.Components.AddQueryBuilderMembersComponent>()
            .AddScoped<IPipelineComponent<QueryBuilderContext>, QueryBuilder.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<QueryBuilderContext>, QueryBuilder.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<QueryBuilderContext>, QueryBuilder.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<QueryBuilderContext>, QueryBuilder.Components.SetVisibilityComponent>()
            // QueryFieldInfo pipeline
            .AddScoped<IPipeline<QueryFieldInfoContext>, Pipeline<QueryFieldInfoContext>>()
            .AddScoped<IPipelineComponent<QueryFieldInfoContext>, QueryFieldInfo.Components.AddQueryFieldInfoMembersComponent>()
            .AddScoped<IPipelineComponent<QueryFieldInfoContext>, QueryFieldInfo.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<QueryFieldInfoContext>, QueryFieldInfo.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<QueryFieldInfoContext>, QueryFieldInfo.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<QueryFieldInfoContext>, QueryFieldInfo.Components.SetVisibilityComponent>()
            // QueryFieldInfoProvider pipeline
            .AddScoped<IPipeline<QueryFieldInfoProviderContext>, Pipeline<QueryFieldInfoProviderContext>>()
            .AddScoped<IPipelineComponent<QueryFieldInfoProviderContext>, QueryFieldInfoProvider.Components.AddQueryFieldInfoMembersComponent>()
            .AddScoped<IPipelineComponent<QueryFieldInfoProviderContext>, QueryFieldInfoProvider.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<QueryFieldInfoProviderContext>, QueryFieldInfoProvider.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<QueryFieldInfoProviderContext>, QueryFieldInfoProvider.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<QueryFieldInfoProviderContext>, QueryFieldInfoProvider.Components.SetVisibilityComponent>()
            // Repository pipeline
            .AddScoped<IPipeline<RepositoryContext>, Pipeline<RepositoryContext>>()
            .AddScoped<IPipelineComponent<RepositoryContext>, Repository.Components.AddRepositoryMembersComponent>()
            .AddScoped<IPipelineComponent<RepositoryContext>, Repository.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<RepositoryContext>, Repository.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<RepositoryContext>, Repository.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<RepositoryContext>, Repository.Components.SetVisibilityComponent>()
            // RepositoryInterface pipeline
            .AddScoped<IPipeline<RepositoryInterfaceContext>, Pipeline<RepositoryInterfaceContext>>()
            .AddScoped<IPipelineComponent<RepositoryInterfaceContext>, RepositoryInterface.Components.AddRepositoryInterfaceMembersComponent>()
            .AddScoped<IPipelineComponent<RepositoryInterfaceContext>, RepositoryInterface.Components.AddGeneratorAttributeComponent>()
            .AddScoped<IPipelineComponent<RepositoryInterfaceContext>, RepositoryInterface.Components.SetNameComponent>()
            .AddScoped<IPipelineComponent<RepositoryInterfaceContext>, RepositoryInterface.Components.SetPartialComponent>()
            .AddScoped<IPipelineComponent<RepositoryInterfaceContext>, RepositoryInterface.Components.SetVisibilityComponent>()
            ;
}
