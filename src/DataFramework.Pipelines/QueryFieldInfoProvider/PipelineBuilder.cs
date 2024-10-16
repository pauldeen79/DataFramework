namespace DataFramework.Pipelines.QueryFieldInfoProvider;

public class PipelineBuilder : PipelineBuilder<QueryFieldInfoProviderContext>
{
    public PipelineBuilder(IEnumerable<IQueryFieldInfoProviderComponentBuilder> queryFieldInfoProviderComponentBuilders)
    {
        AddComponents(queryFieldInfoProviderComponentBuilders);
    }
}
