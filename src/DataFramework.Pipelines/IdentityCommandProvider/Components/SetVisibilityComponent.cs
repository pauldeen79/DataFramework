namespace DataFramework.Pipelines.IdentityCommandProvider.Components;

public class SetVisibilityComponent : IPipelineComponent<IdentityCommandProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<IdentityCommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.IdentityCommandProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
