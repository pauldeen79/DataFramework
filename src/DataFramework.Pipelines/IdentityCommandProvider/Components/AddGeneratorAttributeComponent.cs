namespace DataFramework.Pipelines.IdentityCommandProvider.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<IdentityCommandProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<IdentityCommandProviderContext> context, CancellationToken token)
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
