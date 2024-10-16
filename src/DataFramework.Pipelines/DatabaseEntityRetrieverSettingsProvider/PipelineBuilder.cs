namespace DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProvider;

public class PipelineBuilder : PipelineBuilder<DatabaseEntityRetrieverSettingsProviderContext>
{
    public PipelineBuilder(IEnumerable<IDatabaseEntityRetrieverSettingsProviderComponentBuilder> DatabaseEntityRetrieverSettingsProviderComponentBuilders)
    {
        AddComponents(DatabaseEntityRetrieverSettingsProviderComponentBuilders);
    }
}
