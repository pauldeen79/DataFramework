namespace DataFramework.Pipelines.Class;

public class PipelineBuilder : PipelineBuilder<ClassContext>
{
    public PipelineBuilder(IEnumerable<IClassComponentBuilder> entityComponentBuilders)
    {
        AddComponents(entityComponentBuilders);
    }
}
