namespace DataFramework.Pipelines.EntityRetrieverSettings;

public class PipelineBuilder : PipelineBuilder<EntityRetrieverSettingsContext>
{
    public PipelineBuilder(IEnumerable<IEntityRetrieverSettingsComponentBuilder> EntityRetrieverSettingsComponentBuilders)
    {
        AddComponents(EntityRetrieverSettingsComponentBuilders);
    }
}
