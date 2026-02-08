namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class PipelineEntities(ICommandService commandService) : DataFrameworkCSharpClassBase(commandService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModelAsync(CancellationToken token)
        => GetEntitiesAsync(GetPipelineModelsAsync(), "DataFramework.Pipelines");

    public override string Path => "DataFramework.Pipelines";
}
