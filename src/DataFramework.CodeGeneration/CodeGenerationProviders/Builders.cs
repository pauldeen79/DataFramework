namespace DataFramework.CodeGeneration.CodeGenerationProviders;

public class Builders : DataFrameworkCSharpClassBase
{
    public Builders(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => "DataFramework.Domain/Builders";

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => await GetBuilders(await GetCoreModels(), "DataFramework.Domain.Builders", "DataFramework.Domain");

    protected override bool CreateAsObservable => true;
}
