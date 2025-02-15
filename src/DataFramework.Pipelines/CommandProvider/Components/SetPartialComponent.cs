namespace DataFramework.Pipelines.CommandProvider.Components;

public class SetPartialComponent : IPipelineComponent<CommandProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<CommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
