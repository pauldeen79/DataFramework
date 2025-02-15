namespace DataFramework.Pipelines.IdentityCommandProvider.Components;

public class SetPartialComponent : IPipelineComponent<IdentityCommandProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<IdentityCommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
