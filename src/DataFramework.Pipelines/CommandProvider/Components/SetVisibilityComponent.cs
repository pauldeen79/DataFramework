namespace DataFramework.Pipelines.CommandProvider.Components;

public class SetVisibilityComponentBuilder : ICommandProviderComponentBuilder
{
    public IPipelineComponent<CommandProviderContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<CommandProviderContext>
{
    public Task<Result> Process(PipelineContext<CommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.CommandProviderVisibility);

        return Task.FromResult(Result.Continue());
    }
}
