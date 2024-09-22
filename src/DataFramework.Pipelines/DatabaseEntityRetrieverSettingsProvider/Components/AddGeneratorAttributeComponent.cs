namespace DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProvider.Components;

public class AddGeneratorAttributeComponentBuilder : IDatabaseEntityRetrieverSettingsProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<DatabaseEntityRetrieverSettingsProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverSettingsProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProviderGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
