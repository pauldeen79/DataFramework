namespace DataFramework.Pipelines.Query.Components;

public class SetPartialComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
