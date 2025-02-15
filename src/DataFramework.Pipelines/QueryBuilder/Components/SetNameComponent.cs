namespace DataFramework.Pipelines.QueryBuilder.Components;

public class SetNameComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetQueryBuilderFullName(context.Request.Settings.QueryBuilderNamespace));

        return Task.FromResult(Result.Continue());
    }
}
