namespace DataFramework.Pipelines.IdentityClass.Components;

public class SetRecordComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithRecord(context.Request.Settings.EntityClassType == EntityClassType.ImmutableRecord);

        return Task.FromResult(Result.Continue());
    }
}
