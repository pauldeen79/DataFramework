namespace DataFramework.Pipelines.IdentityClass.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.IdentityClassGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
