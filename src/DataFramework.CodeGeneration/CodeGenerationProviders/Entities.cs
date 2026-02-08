namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Entities(ICommandService commandService) : DataFrameworkCSharpClassBase(commandService)
{
    public override string Path => "DataFramework.Domain";

    public override Task<Result<IEnumerable<TypeBase>>> GetModelAsync(CancellationToken token)
        => GetEntitiesAsync(GetCoreModelsAsync(), "DataFramework.Domain");
}
