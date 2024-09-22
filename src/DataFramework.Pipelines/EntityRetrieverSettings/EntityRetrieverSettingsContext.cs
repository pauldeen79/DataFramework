namespace DataFramework.Pipelines.EntityRetrieverSettings;

public class EntityRetrieverSettingsContext : ContextBase
{
    public EntityRetrieverSettingsContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
