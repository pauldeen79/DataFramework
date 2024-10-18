namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class PipelineBuilders : DataFrameworkCSharpClassBase
{
    public PipelineBuilders(IPipelineService pipelineService) : base(pipelineService)
    {
    }

    public override async Task<IEnumerable<TypeBase>> GetModel() => await GetBuilders(await GetPipelineModels().ConfigureAwait(false), "DataFramework.Pipelines.Builders", "DataFramework.Pipelines").ConfigureAwait(false);

    public override string Path => "DataFramework.Pipelines/Builders";

    protected override bool CreateAsObservable => true;
}
