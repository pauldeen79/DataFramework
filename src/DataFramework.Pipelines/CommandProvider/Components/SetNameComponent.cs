namespace DataFramework.Pipelines.CommandProvider.Components;

public class SetNameComponentBuilder : ICommandProviderComponentBuilder
{
    public IPipelineComponent<CommandProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<CommandProviderContext>
{
    public Task<Result> Process(PipelineContext<CommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}CommandProvider")
            .WithNamespace(context.Request.Settings.CommandProviderNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
