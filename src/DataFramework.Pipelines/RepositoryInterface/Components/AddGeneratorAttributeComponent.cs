namespace DataFramework.Pipelines.RepositoryInterface.Components;

public class AddGeneratorAttributeComponentBuilder : IRepositoryInterfaceComponentBuilder
{
    public IPipelineComponent<RepositoryInterfaceContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<RepositoryInterfaceContext>
{
    public Task<Result> Process(PipelineContext<RepositoryInterfaceContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.RepositoryInterfaceGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}
