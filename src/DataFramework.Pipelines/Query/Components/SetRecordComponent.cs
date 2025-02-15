namespace DataFramework.Pipelines.Query.Components;

public class SetRecordComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithRecord();

        return Task.FromResult(Result.Continue());
    }
}
