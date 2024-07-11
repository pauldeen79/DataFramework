namespace DataFramework.Pipelines.IdentityCommandProvider.Components;

public class SetNameComponentBuilder : IIdentityCommandProviderComponentBuilder
{
    public IPipelineComponent<IdentityCommandProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<IdentityCommandProviderContext>
{
    public Task<Result> Process(PipelineContext<IdentityCommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}IdentityCommandProvider")
            .WithNamespace(context.Request.Settings.IdentityCommandProviderNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
