namespace DataFramework.Pipelines;

public class PipelineService : IPipelineService
{
    private readonly IPipeline<EntityContext> _entityPipeline;

    public PipelineService(
        IPipeline<EntityContext> entityPipeline)
    {
        _entityPipeline = entityPipeline;
    }

    public async Task<Result<TypeBase>> Process(EntityContext context, CancellationToken cancellationToken)
    {
        context = context.IsNotNull(nameof(context));
        var result = await _entityPipeline.Process(context, cancellationToken).ConfigureAwait(false);
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
