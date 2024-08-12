namespace DataFramework.Pipelines.QueryFieldInfo;

public class PipelineBuilder : PipelineBuilder<QueryFieldInfoContext>
{
    public PipelineBuilder(IEnumerable<IQueryFieldInfoComponentBuilder> queryFieldInfoComponentBuilders)
    {
        AddComponents(queryFieldInfoComponentBuilders);
    }
}
