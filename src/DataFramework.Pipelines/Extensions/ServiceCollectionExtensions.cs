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
            .AddScoped<IEntityComponentBuilder, Entity.Components.AddAttributesComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.AddPropertiesComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.EquatableComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.ObservableComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.SetNameComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.SetPartialComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.SetRecordComponentBuilder>()
            .AddScoped<IEntityComponentBuilder, Entity.Components.SetVisibilityComponentBuilder>();
}
