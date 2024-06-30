namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class SetVisibilityComponentBuilder : ICommandEntityProviderComponentBuilder
{
    public IPipelineComponent<CommandEntityProviderContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<CommandEntityProviderContext>
{
    public Task<Result> Process(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.CommandEntityProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
