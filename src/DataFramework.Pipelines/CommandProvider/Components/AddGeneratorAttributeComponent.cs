namespace DataFramework.Pipelines.CommandProvider.Components;

public class AddGeneratorAttributeComponentBuilder : ICommandProviderComponentBuilder
{
    public IPipelineComponent<CommandProviderContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<CommandProviderContext>
{
    public Task<Result> Process(PipelineContext<CommandProviderContext> context, CancellationToken token)
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
