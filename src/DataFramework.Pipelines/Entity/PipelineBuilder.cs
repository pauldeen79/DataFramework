namespace DataFramework.Pipelines.Entity;

public class PipelineBuilder : PipelineBuilder<DataFramework.Pipelines.Entity.EntityContext>
{
    public PipelineBuilder(IEnumerable<IEntityComponentBuilder> entityComponentBuilders)
    {
        AddComponents(entityComponentBuilders);
    }
}
