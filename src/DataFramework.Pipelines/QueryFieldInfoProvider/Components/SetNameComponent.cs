namespace DataFramework.Pipelines.QueryFieldInfoProvider.Components;

public class SetNameComponentBuilder : IQueryFieldInfoProviderComponentBuilder
{
    public IPipelineComponent<QueryFieldInfoProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<QueryFieldInfoProviderContext>
{
    public Task<Result> Process(PipelineContext<QueryFieldInfoProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetQueryFieldInfoProviderFullName(context.Request.Settings.QueryFieldInfoProviderNamespace));

        return Task.FromResult(Result.Continue());
    }
}
