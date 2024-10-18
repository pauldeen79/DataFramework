namespace DataFramework.Pipelines.IdentityClass.Components;

public class SetVisibilityComponentBuilder : IIdentityClassComponentBuilder
{
    public IPipelineComponent<IdentityClassContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> Process(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.SourceModel.IsVisible
            ? Visibility.Public
            : Visibility.Internal);

        return Task.FromResult(Result.Continue());
    }
}
