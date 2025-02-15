namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider.Components;

public class SetPartialComponent : IPipelineComponent<DatabaseEntityRetrieverProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<DatabaseEntityRetrieverProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
