﻿namespace DataFramework.Pipelines;

public class PipelineService : IPipelineService
{
    private readonly IPipeline<ClassContext> _classPipeline;
    private readonly IPipeline<CommandEntityProviderContext> _commandEntityProviderPipeline;
    private readonly IPipeline<CommandProviderContext> _commandProviderPipeline;

    public PipelineService(
        IPipeline<ClassContext> classPipeline,
        IPipeline<CommandEntityProviderContext> commandEntityProviderPipeline,
        IPipeline<CommandProviderContext> commandProviderPipeline)
    {
        _classPipeline = classPipeline.IsNotNull(nameof(classPipeline));
        _commandEntityProviderPipeline = commandEntityProviderPipeline.IsNotNull(nameof(commandEntityProviderPipeline));
        _commandProviderPipeline = commandProviderPipeline.IsNotNull(nameof(commandProviderPipeline));
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
