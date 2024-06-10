namespace DataFramework.Pipelines.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataFrameworkPipelines(this IServiceCollection services)
        => services
            .AddEntityPipeline();

    private static IServiceCollection AddEntityPipeline(this IServiceCollection services)
        => services
            .AddScoped<IPipelineService, PipelineService>()
            // Entity pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<EntityContext>>().Build())
            .AddScoped<IPipelineBuilder<EntityContext>, Entity.PipelineBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.AddAttributesComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.AddConstructorComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.AddPropertiesComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.AddToBuilderMethodComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.EquatableComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.ObservableComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.SetNameComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.SetPartialComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.SetRecordComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.SetVisibilityComponentBuilder>()
            // Class pipeline
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<ClassContext>>().Build())
            .AddScoped<IPipelineBuilder<ClassContext>, Class.PipelineBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.AddPropertiesComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetNameComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetPartialComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetRecordComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetVisibilityComponentBuilder>()
            ;
}
