namespace DataFramework.Pipelines.DependencyInjection;

public class PipelineBuilder : PipelineBuilder<DependencyInjectionContext>
{
    public PipelineBuilder(IEnumerable<IDependencyInjectionComponentBuilder> DependencyInjectionComponentBuilders)
    {
        AddComponents(DependencyInjectionComponentBuilders);
    }
}
