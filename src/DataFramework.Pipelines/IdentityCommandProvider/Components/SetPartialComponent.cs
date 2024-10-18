namespace DataFramework.Pipelines.IdentityCommandProvider.Components;

public class SetPartialComponentBuilder : IIdentityCommandProviderComponentBuilder
{
    public IPipelineComponent<IdentityCommandProviderContext> Build()
        => new SetPartialComponent();
}

public class SetPartialComponent : IPipelineComponent<IdentityCommandProviderContext>
{
    public Task<Result> Process(PipelineContext<IdentityCommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithPartial();

        return Task.FromResult(Result.Continue());
    }
}
