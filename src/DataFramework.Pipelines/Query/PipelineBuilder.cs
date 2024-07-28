namespace DataFramework.Pipelines.Query;

public class PipelineBuilder : PipelineBuilder<QueryContext>
{
    public PipelineBuilder(IEnumerable<IQueryComponentBuilder> QueryComponentBuilders)
    {
        AddComponents(QueryComponentBuilders);
    }
}
