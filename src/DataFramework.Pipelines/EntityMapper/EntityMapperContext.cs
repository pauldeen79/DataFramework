namespace DataFramework.Pipelines.EntityMapper;

public class EntityMapperContext : ContextBase<DataObjectInfo>
{
    public EntityMapperContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
