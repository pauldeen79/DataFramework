namespace DataFramework.Pipelines.CommandEntityProvider;

public class CommandEntityProviderContext : ContextBase<DataObjectInfo>
{
    public CommandEntityProviderContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
