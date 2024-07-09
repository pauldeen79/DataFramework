namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider.Components;

public class SetPartialComponentBuilder : IDatabaseEntityRetrieverProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverProviderContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<DatabaseEntityRetrieverProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
