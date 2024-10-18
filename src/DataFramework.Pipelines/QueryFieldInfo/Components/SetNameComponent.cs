namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class SetNameComponentBuilder : IQueryFieldInfoComponentBuilder
{
    public IPipelineComponent<QueryFieldInfoContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<QueryFieldInfoContext>
{
    public Task<Result> Process(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetQueryFieldInfoFullName(context.Request.Settings.QueryFieldInfoNamespace));

        return Task.FromResult(Result.Continue());
    }
}
