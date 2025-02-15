namespace DataFramework.Pipelines.DependencyInjection.Components;

public class SetVisibilityComponent : IPipelineComponent<DependencyInjectionContext>
{
    public Task<Result> ProcessAsync(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.DependencyInjectionVisibility);

        return Task.FromResult(Result.Continue());
    }
}
