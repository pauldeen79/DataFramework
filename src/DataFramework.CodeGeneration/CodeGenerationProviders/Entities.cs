namespace DataFramework.CodeGeneration.CodeGenerationProviders;

public class Entities : DataFrameworkCSharpClassBase, ICodeGenerationProvider
{
    public override string Path => "DataFramework.Core";
    public override string DefaultFileName => "Entities.generated.cs";

    public override object CreateModel()
        => GetImmutableClasses(GetDataFrameworkModelTypes(), "DataFramework.Core");
}
