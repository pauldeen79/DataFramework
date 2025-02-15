namespace DataFramework.Pipelines.Class.Components;

public class SetRecordComponent : IPipelineComponent<ClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<ClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithRecord(context.Request.Settings.EntityClassType == EntityClassType.ImmutableRecord);

        return Task.FromResult(Result.Continue());
    }
}
