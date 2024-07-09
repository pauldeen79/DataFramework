namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider.Components;

public class AddGeneratorAttributeComponentBuilder : IDatabaseEntityRetrieverProviderComponentBuilder
{
    public IPipelineComponent<DatabaseEntityRetrieverProviderContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<DatabaseEntityRetrieverProviderContext>
{
    public Task<Result> Process(PipelineContext<DatabaseEntityRetrieverProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.DatabaseEntityRetrieverProviderGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
