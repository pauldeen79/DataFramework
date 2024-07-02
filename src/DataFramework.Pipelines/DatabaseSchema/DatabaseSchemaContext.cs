namespace DataFramework.Pipelines.DatabaseSchema;

public class DatabaseSchemaContext : ContextBase<DataObjectInfo>
{
    public DatabaseSchemaContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
        Builders = new Collection<IDatabaseObjectBuilder>();
    }

    public Collection<IDatabaseObjectBuilder> Builders { get; }
}
