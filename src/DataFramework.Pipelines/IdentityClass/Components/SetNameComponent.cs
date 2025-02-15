namespace DataFramework.Pipelines.IdentityClass.Components;

public class SetNameComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace));

        return Task.FromResult(Result.Continue());
    }
}
