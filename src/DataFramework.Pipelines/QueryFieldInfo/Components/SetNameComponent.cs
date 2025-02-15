namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class SetNameComponent : IPipelineComponent<QueryFieldInfoContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetQueryFieldInfoFullName(context.Request.Settings.QueryFieldInfoNamespace));

        return Task.FromResult(Result.Continue());
    }
}
