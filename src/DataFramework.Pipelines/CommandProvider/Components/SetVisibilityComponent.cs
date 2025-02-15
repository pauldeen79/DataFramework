namespace DataFramework.Pipelines.CommandProvider.Components;

public class SetVisibilityComponent : IPipelineComponent<CommandProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<CommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.CommandProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
