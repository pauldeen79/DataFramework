namespace DataFramework.Pipelines.CommandProvider.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<CommandProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<CommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.CommandProviderGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
