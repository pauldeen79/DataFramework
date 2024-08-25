namespace DataFramework.Pipelines.DependencyInjection.Components;

public class SetNameComponentBuilder : IDependencyInjectionComponentBuilder
{
    public IPipelineComponent<DependencyInjectionContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<DependencyInjectionContext>
{
    public Task<Result> Process(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}ServiceContextExtensions")
            .WithNamespace(context.Request.Settings.DependencyInjectionNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
