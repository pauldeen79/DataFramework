namespace DataFramework.Pipelines.QueryFieldInfoProvider;

public class QueryFieldInfoProviderContext : ContextBase
{
    public QueryFieldInfoProviderContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
