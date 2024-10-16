namespace DataFramework.Pipelines.Query.Components;

public class AddGeneratorAttributeComponentBuilder : IQueryComponentBuilder
{
    public IPipelineComponent<QueryContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> Process(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.QueryGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
