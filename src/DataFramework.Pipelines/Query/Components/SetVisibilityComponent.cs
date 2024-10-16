namespace DataFramework.Pipelines.Query.Components;

public class SetVisibilityComponentBuilder : IQueryComponentBuilder
{
    public IPipelineComponent<QueryContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> Process(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.QueryVisibility);

        return Task.FromResult(Result.Continue());
    }
}
