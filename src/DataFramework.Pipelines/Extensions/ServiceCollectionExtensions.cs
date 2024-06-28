namespace DataFramework.Pipelines.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataFrameworkPipelines(this IServiceCollection services)
        => services
            .AddEntityPipeline();

    private static IServiceCollection AddEntityPipeline(this IServiceCollection services)
        => services
            .AddScoped<IPipelineService, PipelineService>()
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
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.AddGeneratorAttributeComponentBuilder>()
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.SetNameComponentBuilder>()
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.SetPartialComponentBuilder>()
            .AddScoped<ICommandProviderComponentBuilder, CommandProvider.Components.SetVisibilityComponentBuilder>()
            ;
}
