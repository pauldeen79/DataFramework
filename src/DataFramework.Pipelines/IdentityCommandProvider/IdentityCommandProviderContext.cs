namespace DataFramework.Pipelines.IdentityCommandProvider;

public class IdentityCommandProviderContext : ContextBase
{
    public IdentityCommandProviderContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
