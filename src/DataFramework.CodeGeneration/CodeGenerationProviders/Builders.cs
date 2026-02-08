namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Builders(ICommandService commandService) : DataFrameworkCSharpClassBase(commandService)
{
    public override string Path => "DataFramework.Domain/Builders";

    public override Task<Result<IEnumerable<TypeBase>>> GetModelAsync(CancellationToken token)
        => GetBuildersAsync(GetCoreModelsAsync(), "DataFramework.Domain.Builders", "DataFramework.Domain");

    protected override bool CreateAsObservable => true;
}
