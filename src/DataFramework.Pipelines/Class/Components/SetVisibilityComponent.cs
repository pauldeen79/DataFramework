namespace DataFramework.Pipelines.Class.Components;

public class SetVisibilityComponentBuilder : IClassComponentBuilder
{
    public IPipelineComponent<ClassContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<ClassContext>
{
    public Task<Result> Process(PipelineContext<ClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.SourceModel.IsVisible
            ? Visibility.Public
            : Visibility.Internal);

        return Task.FromResult(Result.Continue());
    }
}
