namespace DataFramework.Pipelines.QueryBuilder.Components;

public class SetPartialComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
