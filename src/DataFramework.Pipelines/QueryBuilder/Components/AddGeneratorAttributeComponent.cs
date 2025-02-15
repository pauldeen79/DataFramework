namespace DataFramework.Pipelines.QueryBuilder.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.QueryBuilderGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
