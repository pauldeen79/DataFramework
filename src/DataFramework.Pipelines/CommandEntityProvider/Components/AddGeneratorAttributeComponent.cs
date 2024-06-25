namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class AddGeneratorAttributeComponentBuilder : ICommandEntityProviderComponentBuilder
{
    public IPipelineComponent<CommandEntityProviderContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<CommandEntityProviderContext>
{
    public Task<Result> Process(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.ModelFramework.Generators.CommandEntityProviderGenerator", "1.0.0.0")
            );

        return Task.FromResult(Result.Continue());
    }
}
