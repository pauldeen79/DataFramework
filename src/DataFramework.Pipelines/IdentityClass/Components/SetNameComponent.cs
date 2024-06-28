namespace DataFramework.Pipelines.IdentityClass.Components;

public class SetNameComponentBuilder : IIdentityClassComponentBuilder
{
    public IPipelineComponent<IdentityClassContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> Process(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}Identity")
            .WithNamespace(context.Request.SourceModel.TypeName.GetNamespaceWithDefault(context.Request.Settings.DefaultIdentityNamespace));

        return Task.FromResult(Result.Continue());
    }
}
