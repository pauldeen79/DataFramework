namespace DataFramework.CodeGeneration.CodeGenerationProviders;

public class Records : DataFrameworkCSharpClassBase, ICodeGenerationProvider
{
    public override string Path => "DataFramework.Core";
    public override string DefaultFileName => "Entities.generated.cs";

    public override object CreateModel()
        => GetImmutableClasses(GetDataFrameworkModelTypes(), "DataFramework.Core");
}
