namespace DataFramework.Pipelines.QueryBuilder;

public class PipelineBuilder : PipelineBuilder<QueryBuilderContext>
{
    public PipelineBuilder(IEnumerable<IQueryBuilderComponentBuilder> queryBuilderComponentBuilders)
    {
        AddComponents(queryBuilderComponentBuilders);
    }
}
