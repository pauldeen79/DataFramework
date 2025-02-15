namespace DataFramework.Pipelines.DependencyInjection.Components;

public class SetStaticComponent : IPipelineComponent<DependencyInjectionContext>
{
    public Task<Result> ProcessAsync(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithStatic();

        return Task.FromResult(Result.Continue());
    }
}
