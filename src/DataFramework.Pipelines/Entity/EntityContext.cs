namespace DataFramework.Pipelines.Entity;

public class EntityContext : ContextBase<DataObjectInfo, TypeBase>
{
    public EntityContext(DataObjectInfo sourceModel, PipelineSettings settings)
        : base(sourceModel, settings)
    {
    }

    protected override IBuilder<TypeBase> CreateResponseBuilder() => _wrappedBuilder;

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
