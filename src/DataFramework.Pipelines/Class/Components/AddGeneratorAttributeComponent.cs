namespace DataFramework.Pipelines.Class.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<ClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<ClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.ClassGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
