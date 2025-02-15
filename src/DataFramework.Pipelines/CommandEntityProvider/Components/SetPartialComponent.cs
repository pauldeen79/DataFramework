namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class SetPartialComponent : IPipelineComponent<CommandEntityProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
