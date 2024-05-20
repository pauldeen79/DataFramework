namespace DataFramework.Pipelines.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataFrameworkPipelines(this IServiceCollection services)
        => services
            .AddEntityPipeline();

    private static IServiceCollection AddEntityPipeline(this IServiceCollection services)
        => services
            .AddScoped(services => services.GetRequiredService<IPipelineBuilder<EntityContext>>().Build())
            .AddScoped<IPipelineBuilder<EntityContext>, Entity.PipelineBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.ObservableComponentBuilder>();
}
