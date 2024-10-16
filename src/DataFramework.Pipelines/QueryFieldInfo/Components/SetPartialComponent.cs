namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class SetPartialComponentBuilder : IQueryFieldInfoComponentBuilder
{
    public IPipelineComponent<QueryFieldInfoContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<QueryFieldInfoContext>
{
    public Task<Result> Process(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
