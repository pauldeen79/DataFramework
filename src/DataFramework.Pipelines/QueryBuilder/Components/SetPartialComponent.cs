namespace DataFramework.Pipelines.QueryBuilder.Components;

public class SetPartialComponentBuilder : IQueryBuilderComponentBuilder
{
    public IPipelineComponent<QueryBuilderContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> Process(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
