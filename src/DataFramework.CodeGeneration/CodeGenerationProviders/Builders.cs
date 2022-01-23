using ModelFramework.CodeGeneration.Tests.CodeGenerationProviders;
using TextTemplateTransformationFramework.Runtime.CodeGeneration;

namespace DataFramework.CodeGeneration.CodeGenerationProviders
{
    public class Builders : DataFrameworkCSharpClassBase, ICodeGenerationProvider
    {
        public override string Path => "DataFramework.Core\\Builders";

        public override string DefaultFileName => "Builders.generated.cs";

        public override bool RecurseOnDeleteGeneratedFiles => false;

        public override object CreateModel()
            => GetImmutableBuilderClasses(GetDataFrameworkModelTypes(),
                                          "DataFramework.Core",
                                          "DataFramework.Core.Builders");
    }
}
