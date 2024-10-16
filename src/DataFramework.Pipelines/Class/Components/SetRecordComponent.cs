namespace DataFramework.Pipelines.Class.Components;

public class SetRecordComponentBuilder : IClassComponentBuilder
{
    public IPipelineComponent<ClassContext> Build()
        => new SetRecordComponent();
}

public class SetRecordComponent : IPipelineComponent<ClassContext>
{
    public Task<Result> Process(PipelineContext<ClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithRecord(context.Request.Settings.EntityClassType == EntityClassType.ImmutableRecord);

        return Task.FromResult(Result.Continue());
    }
}
