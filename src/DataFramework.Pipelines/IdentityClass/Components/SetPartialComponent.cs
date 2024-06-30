namespace DataFramework.Pipelines.IdentityClass.Components;

public class SetPartialComponentBuilder : IIdentityClassComponentBuilder
{
    public IPipelineComponent<IdentityClassContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> Process(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
