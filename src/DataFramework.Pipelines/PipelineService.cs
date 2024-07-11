namespace DataFramework.Pipelines;

public class PipelineService : IPipelineService
{
    private readonly IPipeline<ClassContext> _classPipeline;
    private readonly IPipeline<CommandEntityProviderContext> _commandEntityProviderPipeline;
    private readonly IPipeline<CommandProviderContext> _commandProviderPipeline;
    private readonly IPipeline<DatabaseEntityRetrieverProviderContext> _databaseEntityRetrieverProviderPipeline;
    private readonly IPipeline<EntityMapperContext> _entityMapperPipeline;
    private readonly IPipeline<IdentityClassContext> _identityClassPipeline;
    private readonly IPipeline<IdentityCommandProviderContext> _identityCommandProviderPipeline;
    private readonly IPipeline<DatabaseSchemaContext> _databaseSchemaPipeline;

    public PipelineService(
        IPipeline<ClassContext> classPipeline,
        IPipeline<CommandEntityProviderContext> commandEntityProviderPipeline,
        IPipeline<CommandProviderContext> commandProviderPipeline,
        IPipeline<DatabaseEntityRetrieverProviderContext> databaseEntityRetrieverProviderPipeline,
        IPipeline<EntityMapperContext> entityMapperPipeline,
        IPipeline<IdentityClassContext> identityClassPipeline,
        IPipeline<IdentityCommandProviderContext> identityCommandProviderPipeline,
        IPipeline<DatabaseSchemaContext> databaseSchemaPipeline)
    {
        _classPipeline = classPipeline.IsNotNull(nameof(classPipeline));
        _commandEntityProviderPipeline = commandEntityProviderPipeline.IsNotNull(nameof(commandEntityProviderPipeline));
        _commandProviderPipeline = commandProviderPipeline.IsNotNull(nameof(commandProviderPipeline));
        _databaseEntityRetrieverProviderPipeline = databaseEntityRetrieverProviderPipeline.IsNotNull(nameof(databaseEntityRetrieverProviderPipeline));
        _entityMapperPipeline = entityMapperPipeline.IsNotNull(nameof(entityMapperPipeline));
        _identityClassPipeline = identityClassPipeline.IsNotNull(nameof(identityClassPipeline));
        _identityCommandProviderPipeline = identityCommandProviderPipeline.IsNotNull(nameof(identityCommandProviderPipeline));
        _databaseSchemaPipeline = databaseSchemaPipeline.IsNotNull(nameof(databaseSchemaPipeline));
    }

    public async Task<Result<TypeBase>> Process(ClassContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _classPipeline.Process(context, cancellationToken).ConfigureAwait(false);
        return ProcessResult(result, context.Builder, context.Builder.Build);
    }

    public async Task<Result<TypeBase>> Process(CommandEntityProviderContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _commandEntityProviderPipeline.Process(context, cancellationToken).ConfigureAwait(false);
        return ProcessResult(result, context.Builder, context.Builder.Build);
    }

    public async Task<Result<TypeBase>> Process(CommandProviderContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _commandProviderPipeline.Process(context, cancellationToken).ConfigureAwait(false);
        return ProcessResult(result, context.Builder, context.Builder.Build);
    }

    public async Task<Result<TypeBase>> Process(DatabaseEntityRetrieverProviderContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _databaseEntityRetrieverProviderPipeline.Process(context, cancellationToken).ConfigureAwait(false);
        return ProcessResult(result, context.Builder, context.Builder.Build);
    }

    public async Task<Result<TypeBase>> Process(EntityMapperContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _entityMapperPipeline.Process(context, cancellationToken).ConfigureAwait(false);
        return ProcessResult(result, context.Builder, context.Builder.Build);
    }

    public async Task<Result<TypeBase>> Process(IdentityClassContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _identityClassPipeline.Process(context, cancellationToken).ConfigureAwait(false);
        return ProcessResult(result, context.Builder, context.Builder.Build);
    }

    public async Task<Result<TypeBase>> Process(IdentityCommandProviderContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _identityCommandProviderPipeline.Process(context, cancellationToken).ConfigureAwait(false);
        return ProcessResult(result, context.Builder, context.Builder.Build);
    }

    public async Task<Result<IEnumerable<IDatabaseObject>>> Process(DatabaseSchemaContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _databaseSchemaPipeline.Process(context, cancellationToken).ConfigureAwait(false);
        return ProcessResult(result, context.Builders, () => context.Builders.Select(x => x.Build()));
    }

    private static Result<TResult> ProcessResult<TResult>(Result result, object responseBuilder, Func<TResult> dlg)
    {
        if (!result.IsSuccessful())
        {
            return Result.FromExistingResult<TResult>(result);
        }

        var validationResults = new List<ValidationResult>();
        var success = responseBuilder.TryValidate(validationResults);
        if (!success)
        {
            return Result.Invalid<TResult>("Pipeline response is not valid", validationResults.Select(x => new ValidationError(x.ErrorMessage ?? string.Empty, x.MemberNames)));
        }

        return Result.FromExistingResult(result, dlg.Invoke());
    }
}
