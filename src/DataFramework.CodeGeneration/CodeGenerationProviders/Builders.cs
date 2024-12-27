namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Builders(IPipelineService pipelineService) : DataFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => "DataFramework.Domain/Builders";

    public override Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => GetBuilders(GetCoreModels(), "DataFramework.Domain.Builders", "DataFramework.Domain");

    protected override bool CreateAsObservable => true;
}
