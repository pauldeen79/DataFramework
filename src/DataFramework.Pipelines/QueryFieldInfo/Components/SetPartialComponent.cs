namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class SetPartialComponent : IPipelineComponent<QueryFieldInfoContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
