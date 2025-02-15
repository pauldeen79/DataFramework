namespace DataFramework.Pipelines.IdentityClass.Components;

public class SetVisibilityComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.SourceModel.IsVisible
            ? Visibility.Public
            : Visibility.Internal);

        return Task.FromResult(Result.Continue());
    }
}
