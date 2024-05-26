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

public abstract class ContextBase<TSourceModel> : ContextBase
{
    protected ContextBase(TSourceModel sourceModel, PipelineSettings settings, IFormatProvider formatProvider) : base(settings, formatProvider)
    {
        SourceModel = sourceModel.IsNotNull(nameof(sourceModel));
    }

    public TSourceModel SourceModel { get; }
}
