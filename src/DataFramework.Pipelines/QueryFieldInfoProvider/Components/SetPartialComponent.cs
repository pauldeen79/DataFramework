namespace DataFramework.Pipelines.QueryFieldInfoProvider.Components;

public class SetPartialComponentBuilder : IQueryFieldInfoProviderComponentBuilder
{
    public IPipelineComponent<QueryFieldInfoProviderContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<QueryFieldInfoProviderContext>
{
    public Task<Result> Process(PipelineContext<QueryFieldInfoProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
