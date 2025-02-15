namespace DataFramework.Pipelines.PagedEntityRetrieverSettings.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<PagedEntityRetrieverSettingsContext>
{
    public Task<Result> ProcessAsync(PipelineContext<PagedEntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.PagedEntityRetrieverSettingsGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
