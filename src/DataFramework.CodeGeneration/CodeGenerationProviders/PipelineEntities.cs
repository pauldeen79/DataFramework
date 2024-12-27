namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class PipelineEntities(IPipelineService pipelineService) : DataFrameworkCSharpClassBase(pipelineService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken) => GetEntities(GetPipelineModels(), "DataFramework.Pipelines");

    public override string Path => "DataFramework.Pipelines";
}
