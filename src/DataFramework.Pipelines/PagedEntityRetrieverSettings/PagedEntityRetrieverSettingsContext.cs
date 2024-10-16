namespace DataFramework.Pipelines.PagedEntityRetrieverSettings;

public class PagedEntityRetrieverSettingsContext : ContextBase
{
    public PagedEntityRetrieverSettingsContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
