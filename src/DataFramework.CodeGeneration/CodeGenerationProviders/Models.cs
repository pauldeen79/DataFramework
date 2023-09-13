namespace DataFramework.CodeGeneration.CodeGenerationProviders;

public class Models : DataFrameworkCSharpClassBase, ICodeGenerationProvider
{
    public override string Path => "DataFramework.Core/Models";
    public override string DefaultFileName => "Models.generated.cs";

    protected override string AddMethodNameFormatString => string.Empty; // we don't want Add methods for collection properties
    protected override string SetMethodNameFormatString => string.Empty; // we don't want With methods for non-collection properties
    protected override string BuilderNameFormatString => "{0}Model";
    protected override string BuilderBuildMethodName => "ToEntity";
    protected override string BuilderBuildTypedMethodName => "ToTypedEntity";
    protected override string BuilderName => "Model";
    protected override string BuildersName => "Models";
    protected override bool UseLazyInitialization => false; // we don't want lazy stuff in models, just getters and setters

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetDataFrameworkModelTypes(),
                                      "DataFramework.Core",
                                      "DataFramework.Core.Models");

    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        // note that this should maybe not be necessary, but DataFramework still uses this override in the DataFrameworkCSharpClassBase class...
        if (typeName.StartsWith("DataFramework.Abstractions.I", StringComparison.InvariantCulture))
        {
            property.ConvertSinglePropertyToBuilderOnBuilder
            (
                typeName.Replace("Abstractions.I", "Core.Models.") + "Model",
                customBuilderMethodParameterExpression: property.IsNullable || AddNullChecks
                    ? "{0}?.ToEntity()"
                    : "{0}{2}.ToEntity()"
            );
        }
        else if (typeName.Contains("Collection<DataFramework."))
        {
            property.ConvertCollectionPropertyToBuilderOnBuilder
            (
                false,
                typeof(ReadOnlyValueCollection<>).WithoutGenerics(),
                typeName.Replace("Abstractions.I", "Core.Models.").ReplaceSuffix(">", "Model>", StringComparison.InvariantCulture),
                null,
                customBuilderMethodParameterExpression: "{0}.Select(x => x.ToEntity())"
            );
        }
        else if (typeName.Contains($"Collection<{typeof(string).FullName}"))
        {
            property.AddMetadata(MetadataNames.CustomBuilderMethodParameterExpression, $"new {typeof(List<string>).FullName.FixTypeName()}({{0}})");
        }
    }
}
