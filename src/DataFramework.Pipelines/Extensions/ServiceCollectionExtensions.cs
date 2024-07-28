namespace DataFramework.Pipelines.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataFrameworkPipelines(this IServiceCollection services)
        => services
            .AddEntityPipeline();

    private static IServiceCollection AddEntityPipeline(this IServiceCollection services)
        => services
            // Placeholder processors
            .AddScoped<IPlaceholderProcessor, DataObjectInfoProcessor>()
            // Class pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<ClassContext>>().Build())
            .AddScoped<IPipelineBuilder<ClassContext>, Class.PipelineBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.AddPropertiesComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetNameComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetPartialComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetRecordComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetVisibilityComponentBuilder>()
            // CommandEntityProvider pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<CommandEntityProviderContext>>().Build())
            .AddScoped<IPipelineBuilder<CommandEntityProviderContext>, CommandEntityProvider.PipelineBuilder>()
            .AddScoped<ICommandEntityProviderComponentBuilder, CommandEntityProvider.Components.AddEntityCommandProviderMembersComponentBuilder>()
            .AddScoped<ICommandEntityProviderComponentBuilder, CommandEntityProvider.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<ICommandEntityProviderComponentBuilder, CommandEntityProvider.Components.SetNameComponentBuilder>()
            .AddScoped<ICommandEntityProviderComponentBuilder, CommandEntityProvider.Components.SetPartialComponentBuilder>()
            .AddScoped<ICommandEntityProviderComponentBuilder, CommandEntityProvider.Components.SetVisibilityComponentBuilder>()
            // CommandProvider pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<CommandProviderContext>>().Build())
            .AddScoped<IPipelineBuilder<CommandProviderContext>, CommandProvider.PipelineBuilder>()
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.AddCommandProviderMembersComponentBuilder>()
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.SetNameComponentBuilder>()
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.SetPartialComponentBuilder>()
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.SetVisibilityComponentBuilder>()
            // DatabaseEntityRetrieverProvider pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<DatabaseEntityRetrieverProviderContext>>().Build())
            .AddScoped<IPipelineBuilder<DatabaseEntityRetrieverProviderContext>, DatabaseEntityRetrieverProvider.PipelineBuilder>()
            .AddScoped<IDatabaseEntityRetrieverProviderComponentBuilder, DatabaseEntityRetrieverProvider.Components.AddDatabaseEntityRetrieverProviderMembersComponentBuilder>()
            .AddScoped<IDatabaseEntityRetrieverProviderComponentBuilder, DatabaseEntityRetrieverProvider.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<IDatabaseEntityRetrieverProviderComponentBuilder, DatabaseEntityRetrieverProvider.Components.SetNameComponentBuilder>()
            .AddScoped<IDatabaseEntityRetrieverProviderComponentBuilder, DatabaseEntityRetrieverProvider.Components.SetPartialComponentBuilder>()
            .AddScoped<IDatabaseEntityRetrieverProviderComponentBuilder, DatabaseEntityRetrieverProvider.Components.SetVisibilityComponentBuilder>()
            // DatabaseSchema pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<DatabaseSchemaContext>>().Build())
            .AddScoped<IPipelineBuilder<DatabaseSchemaContext>, DatabaseSchema.PipelineBuilder>()
            .AddScoped<IDatabaseSchemaComponentBuilder, DatabaseSchema.Components.AddTableComponentBuilder>()
            .AddScoped<IDatabaseSchemaComponentBuilder, DatabaseSchema.Components.AddInsertUpdateDeleteStoredProceduresComponentBuilder>()
            // EntityMapper pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<EntityMapperContext>>().Build())
            .AddScoped<IPipelineBuilder<EntityMapperContext>, EntityMapper.PipelineBuilder>()
            .AddScoped<IEntityMapperComponentBuilder, EntityMapper.Components.AddEntityMapperMembersComponentBuilder>()
            .AddScoped<IEntityMapperComponentBuilder, EntityMapper.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<IEntityMapperComponentBuilder, EntityMapper.Components.SetNameComponentBuilder>()
            .AddScoped<IEntityMapperComponentBuilder, EntityMapper.Components.SetPartialComponentBuilder>()
            .AddScoped<IEntityMapperComponentBuilder, EntityMapper.Components.SetVisibilityComponentBuilder>()
            // IdentityClass pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<IdentityClassContext>>().Build())
            .AddScoped<IPipelineBuilder<IdentityClassContext>, IdentityClass.PipelineBuilder>()
            .AddScoped<IIdentityClassComponentBuilder, IdentityClass.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<IIdentityClassComponentBuilder, IdentityClass.Components.AddPropertiesComponentBuilder>()
            .AddScoped<IIdentityClassComponentBuilder, IdentityClass.Components.SetNameComponentBuilder>()
            .AddScoped<IIdentityClassComponentBuilder, IdentityClass.Components.SetPartialComponentBuilder>()
            .AddScoped<IIdentityClassComponentBuilder, IdentityClass.Components.SetRecordComponentBuilder>()
            .AddScoped<IIdentityClassComponentBuilder, IdentityClass.Components.SetVisibilityComponentBuilder>()
            // IdentityCommandProvider pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<IdentityCommandProviderContext>>().Build())
            .AddScoped<IPipelineBuilder<IdentityCommandProviderContext>, IdentityCommandProvider.PipelineBuilder>()
            .AddScoped<IIdentityCommandProviderComponentBuilder, IdentityCommandProvider.Components.AddIdentityCommandProviderMembersComponentBuilder>()
            .AddScoped<IIdentityCommandProviderComponentBuilder, IdentityCommandProvider.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<IIdentityCommandProviderComponentBuilder, IdentityCommandProvider.Components.SetNameComponentBuilder>()
            .AddScoped<IIdentityCommandProviderComponentBuilder, IdentityCommandProvider.Components.SetPartialComponentBuilder>()
            .AddScoped<IIdentityCommandProviderComponentBuilder, IdentityCommandProvider.Components.SetVisibilityComponentBuilder>()
            // PagedEntityRetrieverSettings pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<PagedEntityRetrieverSettingsContext>>().Build())
            .AddScoped<IPipelineBuilder<PagedEntityRetrieverSettingsContext>, PagedEntityRetrieverSettings.PipelineBuilder>()
            .AddScoped<IPagedEntityRetrieverSettingsComponentBuilder, PagedEntityRetrieverSettings.Components.AddPagedEntityRetrieverSettingsMembersComponentBuilder>()
            .AddScoped<IPagedEntityRetrieverSettingsComponentBuilder, PagedEntityRetrieverSettings.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<IPagedEntityRetrieverSettingsComponentBuilder, PagedEntityRetrieverSettings.Components.SetNameComponentBuilder>()
            .AddScoped<IPagedEntityRetrieverSettingsComponentBuilder, PagedEntityRetrieverSettings.Components.SetPartialComponentBuilder>()
            .AddScoped<IPagedEntityRetrieverSettingsComponentBuilder, PagedEntityRetrieverSettings.Components.SetVisibilityComponentBuilder>()
            ;
}
