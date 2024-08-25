namespace DataFramework.Pipelines.DependencyInjection;

public class DependencyInjectionContext : ContextBase<DataObjectInfo>
{
    public DependencyInjectionContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
