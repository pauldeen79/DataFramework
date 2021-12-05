using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class AttributeBuilderExtensions
    {
        internal static AttributeBuilder ForCodeGenerator(this AttributeBuilder instance, string codeGeneratorName)
            => instance.WithName("System.CodeDom.Compiler.GeneratedCode")
                       .AddParameters
                       (
                           new AttributeParameterBuilder().WithValue(codeGeneratorName),
                           new AttributeParameterBuilder().WithValue(typeof(AttributeBuilderExtensions).Assembly.GetName().Version.ToString())
                       );
    }
}
