namespace DataFramework.Pipelines.IdentityCommandProvider.Components;

public class AddGeneratorAttributeComponentBuilder : IIdentityCommandProviderComponentBuilder
{
    public IPipelineComponent<IdentityCommandProviderContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<IdentityCommandProviderContext>
{
    public Task<Result> Process(PipelineContext<IdentityCommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.IdentityCommandProviderGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
