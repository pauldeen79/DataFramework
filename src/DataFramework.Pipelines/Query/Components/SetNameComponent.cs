namespace DataFramework.Pipelines.Query.Components;

public class SetNameComponentBuilder : IQueryComponentBuilder
{
    public IPipelineComponent<QueryContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> Process(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetQueryFullName(context.Request.Settings.QueryNamespace));

        return Task.FromResult(Result.Continue());
    }
}
