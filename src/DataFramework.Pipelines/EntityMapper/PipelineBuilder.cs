namespace DataFramework.Pipelines.EntityMapper;

public class PipelineBuilder : PipelineBuilder<EntityMapperContext>
{
    public PipelineBuilder(IEnumerable<IEntityMapperComponentBuilder> entityMapperComponentBuilders)
    {
        AddComponents(entityMapperComponentBuilders);
    }
}
