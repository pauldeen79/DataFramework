namespace DataFramework.Pipelines.RepositoryInterface;

public class RepositoryInterfaceContext : ContextBase
{
    public RepositoryInterfaceContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public InterfaceBuilder Builder => _wrappedBuilder.Builder;

    private readonly InterfaceBuilderWrapper _wrappedBuilder = new();
}
