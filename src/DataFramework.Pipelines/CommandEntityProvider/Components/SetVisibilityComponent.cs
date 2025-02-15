namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class SetVisibilityComponent : IPipelineComponent<CommandEntityProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.CommandEntityProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
