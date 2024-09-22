namespace DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProvider;

public class DatabaseEntityRetrieverSettingsProviderContext : ContextBase
{
    public DatabaseEntityRetrieverSettingsProviderContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
