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

public abstract class ContextBase<TSourceModel, TResponse> : ContextBase<TSourceModel>
{
    protected ContextBase(TSourceModel sourceModel, PipelineSettings settings, IFormatProvider formatProvider) : base(sourceModel, settings, formatProvider)
    {
    }

    protected abstract IBuilder<TResponse> CreateResponseBuilder();

    private IBuilder<TResponse>? _responseBuilder;
    public IBuilder<TResponse> ResponseBuilder
    {
        get
        {
            if (_responseBuilder is null)
            {
                _responseBuilder = CreateResponseBuilder();
                if (_responseBuilder is null)
                {
                    throw new InvalidOperationException($"{nameof(CreateResponseBuilder)} returned a null reference, this is not allowed");
                }
            }

            return _responseBuilder;
        }
    }
}
