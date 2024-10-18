namespace DataFramework.Pipelines.IdentityClass.Components;

public class SetRecordComponentBuilder : IIdentityClassComponentBuilder
{
    public IPipelineComponent<IdentityClassContext> Build()
        => new SetRecordComponent();
}

public class SetRecordComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> Process(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithRecord(context.Request.Settings.EntityClassType == EntityClassType.ImmutableRecord);

        return Task.FromResult(Result.Continue());
    }
}
