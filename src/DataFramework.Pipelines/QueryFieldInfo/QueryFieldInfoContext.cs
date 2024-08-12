namespace DataFramework.Pipelines.QueryFieldInfo;

public class QueryFieldInfoContext : ContextBase<DataObjectInfo>
{
    public QueryFieldInfoContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
