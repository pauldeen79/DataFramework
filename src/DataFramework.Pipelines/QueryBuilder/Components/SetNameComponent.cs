namespace DataFramework.Pipelines.QueryBuilder.Components;

public class SetNameComponentBuilder : IQueryBuilderComponentBuilder
{
    public IPipelineComponent<QueryBuilderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> Process(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetQueryBuilderFullName(context.Request.Settings.QueryBuilderNamespace));

        return Task.FromResult(Result.Continue());
    }
}
