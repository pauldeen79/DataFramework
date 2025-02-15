namespace DataFramework.Pipelines.DependencyInjection.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<DependencyInjectionContext>
{
    public Task<Result> ProcessAsync(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.DependencyInjectionGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
