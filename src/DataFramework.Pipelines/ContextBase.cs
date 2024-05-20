namespace DataFramework.Pipelines;

public abstract class ContextBase
{
    public PipelineSettings Settings { get; }

    protected ContextBase(PipelineSettings settings)
    {
        Settings = settings.IsNotNull(nameof(settings));
    }
}

public abstract class ContextBase<TSourceModel> : ContextBase
{
    protected ContextBase(TSourceModel sourceModel, PipelineSettings settings) : base(settings)
    {
        SourceModel = sourceModel.IsNotNull(nameof(sourceModel));
    }

    public TSourceModel SourceModel { get; }
}

public abstract class ContextBase<TSourceModel, TResponse> : ContextBase<TSourceModel>
{
    protected ContextBase(TSourceModel sourceModel, PipelineSettings settings) : base(sourceModel, settings)
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
