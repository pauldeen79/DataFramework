namespace DataFramework.Pipelines.QueryFieldInfoProvider.Components;

public class SetPartialComponent : IPipelineComponent<QueryFieldInfoProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryFieldInfoProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
