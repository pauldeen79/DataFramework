namespace DataFramework.Pipelines.IdentityCommandProvider.Components;

public class SetNameComponentBuilder : IIdentityCommandProviderComponentBuilder
{
    public IPipelineComponent<IdentityCommandProviderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<IdentityCommandProviderContext>
{
    public Task<Result> Process(PipelineContext<IdentityCommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder.WithFullName(context.Request.SourceModel.GetIdentityCommandProviderFullName(context.Request.Settings.IdentityCommandProviderNamespace));

        return Task.FromResult(Result.Continue());
    }
}
