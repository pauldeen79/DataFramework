namespace DataFramework.Pipelines.CommandProvider.Components;

public class SetNameComponent : IPipelineComponent<CommandProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<CommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetCommandProviderFullName(context.Request.Settings.CommandProviderNamespace));

        return Task.FromResult(Result.Continue());
    }
}
