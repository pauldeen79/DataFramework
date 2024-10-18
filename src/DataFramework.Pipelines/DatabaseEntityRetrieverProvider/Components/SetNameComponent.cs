namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider.Components;

public class SetNameComponentBuilder : IDatabaseEntityRetrieverProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<DatabaseEntityRetrieverProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetDatabaseEntityRetrieverProviderFullName(context.Request.Settings.DatabaseEntityRetrieverProviderNamespace));

        return Task.FromResult(Result.Continue());
    }
}
