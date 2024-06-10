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
            .AddScoped<IClassComponentBuilder, Class.Components.AddPropertiesComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetNameComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetPartialComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetRecordComponentBuilder>()
            .AddScoped<IClassComponentBuilder, Class.Components.SetVisibilityComponentBuilder>()
            ;
}
