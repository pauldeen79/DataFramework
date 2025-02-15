namespace DataFramework.Pipelines.QueryFieldInfoProvider.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<QueryFieldInfoProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryFieldInfoProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.QueryFieldInfoProviderGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
