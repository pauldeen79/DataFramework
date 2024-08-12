namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class SetVisibilityComponentBuilder : IQueryFieldInfoComponentBuilder
{
    public IPipelineComponent<QueryFieldInfoContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<QueryFieldInfoContext>
{
    public Task<Result> Process(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.QueryFieldInfoVisibility);

        return Task.FromResult(Result.Continue());
    }
}
