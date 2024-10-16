namespace DataFramework.Pipelines.EntityMapper.Components;

public class SetVisibilityComponentBuilder : IEntityMapperComponentBuilder
{
    public IPipelineComponent<EntityMapperContext> Build()
        => new SetVisibilityComponent();
}

public class SetVisibilityComponent : IPipelineComponent<EntityMapperContext>
{
    public Task<Result> Process(PipelineContext<EntityMapperContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithVisibility(context.Request.Settings.EntityMapperVisibility);

        return Task.FromResult(Result.Continue());
    }
}
