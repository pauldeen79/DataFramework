namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class SetNameComponent : IPipelineComponent<CommandEntityProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetCommandEntityProviderFullName(context.Request.Settings.CommandEntityProviderNamespace));

        return Task.FromResult(Result.Continue());
    }
}
