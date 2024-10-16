namespace DataFramework.Pipelines.EntityMapper.Components;

public class SetPartialComponentBuilder : IEntityMapperComponentBuilder
{
    public IPipelineComponent<EntityMapperContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<EntityMapperContext>
{
    public Task<Result> Process(PipelineContext<EntityMapperContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
