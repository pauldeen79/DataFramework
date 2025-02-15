namespace DataFramework.Pipelines.Class.Components;

public class SetNameComponent : IPipelineComponent<ClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<ClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace));

        return Task.FromResult(Result.Continue());
    }
}
