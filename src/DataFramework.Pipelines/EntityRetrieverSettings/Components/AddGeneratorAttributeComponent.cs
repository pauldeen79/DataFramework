namespace DataFramework.Pipelines.EntityRetrieverSettings.Components;

public class AddGeneratorAttributeComponentBuilder : IEntityRetrieverSettingsComponentBuilder
{
    public IPipelineComponent<EntityRetrieverSettingsContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<EntityRetrieverSettingsContext>
{
    public Task<Result> Process(PipelineContext<EntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.EntityRetrieverSettingsGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
