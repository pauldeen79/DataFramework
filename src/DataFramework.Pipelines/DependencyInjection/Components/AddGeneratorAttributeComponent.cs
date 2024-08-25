namespace DataFramework.Pipelines.DependencyInjection.Components;

public class AddGeneratorAttributeComponentBuilder : IDependencyInjectionComponentBuilder
{
    public IPipelineComponent<DependencyInjectionContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<DependencyInjectionContext>
{
    public Task<Result> Process(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
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
