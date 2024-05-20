namespace DataFramework.Pipelines;

public abstract class ContextBase
{
    public PipelineSettings Settings { get; }
    public IFormatProvider FormatProvider { get; }

    protected ContextBase(PipelineSettings settings, IFormatProvider formatProvider)
    {
        Settings = settings.IsNotNull(nameof(settings));
        FormatProvider = formatProvider.IsNotNull(nameof(formatProvider));
    }
}
