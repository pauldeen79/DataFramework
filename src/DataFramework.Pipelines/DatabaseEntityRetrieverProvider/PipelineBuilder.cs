namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider;

public class PipelineBuilder : PipelineBuilder<DatabaseEntityRetrieverProviderContext>
{
    public PipelineBuilder(IEnumerable<IDatabaseEntityRetrieverProviderComponentBuilder> databaseEntityRetrieverProviderComponentBuilders)
    {
        AddComponents(databaseEntityRetrieverProviderComponentBuilders);
    }
}
