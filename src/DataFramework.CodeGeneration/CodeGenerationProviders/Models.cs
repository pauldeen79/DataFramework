namespace DataFramework.CodeGeneration.CodeGenerationProviders;

public class Models : DataFrameworkCSharpClassBase, ICodeGenerationProvider
{
    public override string Path => "DataFramework.Core/Models";
    public override string DefaultFileName => "Models.generated.cs";

    protected override string AddMethodNameFormatString => string.Empty; // we don't want Add methods for collection properties
    protected override string SetMethodNameFormatString => string.Empty; // we don't want With methods for non-collection properties
    protected override string BuilderNameFormatString => "{0}Model";
    protected override bool UseLazyInitialization => false; // we don't want lazy stuff in models, just getters and setters

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetDataFrameworkModelTypes(),
                                      "DataFramework.Core",
                                      "DataFramework.Core.Models");

    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        // method intentionally left empty
        // if we don't override this, then string will become stringbuilders etc.
    }
}
