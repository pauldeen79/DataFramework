namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class SetNameComponentBuilder : ICommandEntityProviderComponentBuilder
{
    public IPipelineComponent<CommandEntityProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<CommandEntityProviderContext>
{
    public Task<Result> Process(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}CommandEntityProvider")
            .WithNamespace(context.Request.Settings.CommandEntityProviderNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
