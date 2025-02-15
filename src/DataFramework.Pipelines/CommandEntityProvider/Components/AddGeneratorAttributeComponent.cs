namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<CommandEntityProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.CommandEntityProviderGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
