namespace DataFramework.Pipelines.Entity.Components;

public class SetVisibilityComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.SourceModel.IsVisible
            ? Visibility.Public
            : Visibility.Internal);

        return Task.FromResult(Result.Continue());
    }
}
