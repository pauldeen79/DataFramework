namespace DataFramework.Pipelines.Repository.Components;

public class AddGeneratorAttributeComponent : IPipelineComponent<RepositoryContext>
{
    public Task<Result> ProcessAsync(PipelineContext<RepositoryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.RepositoryGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
