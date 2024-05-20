namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class PipelineEntities : DataFrameworkCSharpClassBase
{
    public PipelineEntities(IPipelineService pipelineService) : base(pipelineService)
    {
    }

    public override async Task<IEnumerable<TypeBase>> GetModel() => await GetEntities(await GetPipelineModels().ConfigureAwait(false), "DataFramework.Pipelines").ConfigureAwait(false);

    public override string Path => "DataFramework.Pipelines";
}
