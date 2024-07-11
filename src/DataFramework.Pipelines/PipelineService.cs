namespace DataFramework.Pipelines;

public static class PipelineService
{
    //TODO: Move this code to CrossCutting.Pipelines, in a helper class
    public static Result<TResult> ProcessResult<TResult>(Result result, object responseBuilder, Func<TResult> resultValueDelegate)
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

        return Result.FromExistingResult(result, resultValueDelegate.Invoke());
    }
}
