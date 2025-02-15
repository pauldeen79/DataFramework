namespace DataFramework.Pipelines.DependencyInjection.Components;

public class SetPartialComponent : IPipelineComponent<DependencyInjectionContext>
{
    public Task<Result> ProcessAsync(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
