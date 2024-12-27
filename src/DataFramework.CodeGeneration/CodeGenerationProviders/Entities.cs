namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Entities(IPipelineService pipelineService) : DataFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => "DataFramework.Domain";

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetEntities(GetCoreModels(), "DataFramework.Domain");
}
