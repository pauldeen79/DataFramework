namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<QueryFieldInfoContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.QueryFieldInfoGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
