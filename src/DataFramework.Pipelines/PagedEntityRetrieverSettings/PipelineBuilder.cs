namespace DataFramework.Pipelines.PagedEntityRetrieverSettings;

public class PipelineBuilder : PipelineBuilder<PagedEntityRetrieverSettingsContext>
{
    public PipelineBuilder(IEnumerable<IPagedEntityRetrieverSettingsComponentBuilder> pagedEntityRetrieverSettingsComponentBuilders)
    {
        AddComponents(pagedEntityRetrieverSettingsComponentBuilders);
    }
}
