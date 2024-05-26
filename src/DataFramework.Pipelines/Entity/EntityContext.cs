namespace DataFramework.Pipelines.Entity;

public class EntityContext : ContextBase<DataObjectInfo>
{
    public EntityContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
