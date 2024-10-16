namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class SetPartialComponentBuilder : ICommandEntityProviderComponentBuilder
{
    public IPipelineComponent<CommandEntityProviderContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<CommandEntityProviderContext>
{
    public Task<Result> Process(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
