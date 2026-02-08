namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class PipelineBuilders(ICommandService commandService) : DataFrameworkCSharpClassBase(commandService)
{
    public override Task<Result<IEnumerable<TypeBase>>> GetModelAsync(CancellationToken token)
        => GetBuildersAsync(GetPipelineModelsAsync(), "DataFramework.Pipelines.Builders", "DataFramework.Pipelines");

    public override string Path => "DataFramework.Pipelines/Builders";

    protected override bool CreateAsObservable => true;
}
