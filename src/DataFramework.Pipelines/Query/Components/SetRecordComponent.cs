namespace DataFramework.Pipelines.Query.Components;

public class SetRecordComponentBuilder : IQueryComponentBuilder
{
    public IPipelineComponent<QueryContext> Build()
        => new SetRecordComponent();
}

public class SetRecordComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> Process(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithRecord();

        return Task.FromResult(Result.Continue());
    }
}
