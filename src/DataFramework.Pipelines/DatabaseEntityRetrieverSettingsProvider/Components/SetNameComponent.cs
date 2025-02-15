namespace DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProvider.Components;

public class SetNameComponent : IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<DatabaseEntityRetrieverSettingsProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetPagedDatabaseEntityRetrieverSettingsProviderFullName(context.Request.Settings.DatabaseEntityRetrieverSettingsProviderNamespace));

        return Task.FromResult(Result.Continue());
    }
}
