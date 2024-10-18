namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Builders : DataFrameworkCSharpClassBase
{
    public Builders(IPipelineService pipelineService) : base(pipelineService)
    {
    }

    public override string Path => "DataFramework.Domain/Builders";

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => await GetBuilders(await GetCoreModels(), "DataFramework.Domain.Builders", "DataFramework.Domain");

    protected override bool CreateAsObservable => true;
}
