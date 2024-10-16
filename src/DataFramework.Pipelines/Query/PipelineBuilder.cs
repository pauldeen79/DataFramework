namespace DataFramework.Pipelines.Query;

public class PipelineBuilder : PipelineBuilder<QueryContext>
{
    public PipelineBuilder(IEnumerable<IQueryComponentBuilder> queryComponentBuilders)
    {
        AddComponents(queryComponentBuilders);
    }
}
