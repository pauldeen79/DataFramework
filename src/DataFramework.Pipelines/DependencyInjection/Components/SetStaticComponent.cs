namespace DataFramework.Pipelines.DependencyInjection.Components;

public class SetStaticComponentBuilder : IDependencyInjectionComponentBuilder
{
    public IPipelineComponent<DependencyInjectionContext> Build()
        => new SetStaticComponent();
}

public class SetStaticComponent : IPipelineComponent<DependencyInjectionContext>
{
    public Task<Result> Process(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithStatic();

        return Task.FromResult(Result.Continue());
    }
}
