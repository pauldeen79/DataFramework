namespace DataFramework.Pipelines.EntityMapper.Components;

public class SetNameComponent : IPipelineComponent<EntityMapperContext>
{
    public Task<Result> ProcessAsync(PipelineContext<EntityMapperContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetEntityMapperFullName(context.Request.Settings.EntityMapperNamespace));

        return Task.FromResult(Result.Continue());
    }
}
