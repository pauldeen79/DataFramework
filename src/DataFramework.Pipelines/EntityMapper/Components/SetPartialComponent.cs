namespace DataFramework.Pipelines.EntityMapper.Components;

public class SetPartialComponent : IPipelineComponent<EntityMapperContext>
{
    public Task<Result> ProcessAsync(PipelineContext<EntityMapperContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
