namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class PipelineBuilders(IPipelineService pipelineService) : DataFrameworkCSharpClassBase(pipelineService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken) => GetBuilders(GetPipelineModels(), "DataFramework.Pipelines.Builders", "DataFramework.Pipelines");

    public override string Path => "DataFramework.Pipelines/Builders";

    protected override bool CreateAsObservable => true;
}
