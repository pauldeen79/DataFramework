namespace DataFramework.Pipelines.Query.Components;

public class SetNameComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetQueryFullName(context.Request.Settings.QueryNamespace));

        return Task.FromResult(Result.Continue());
    }
}
