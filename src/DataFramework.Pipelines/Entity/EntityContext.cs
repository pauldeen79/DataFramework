namespace DataFramework.Pipelines.Entity;

public class EntityContext : ContextBase<DataObjectInfo, TypeBase>
{
    public EntityContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    protected override IBuilder<TypeBase> CreateResponseBuilder() => _wrappedBuilder;

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
