namespace DataFramework.Pipelines.IdentityClass.Components;

public class SetPartialComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
