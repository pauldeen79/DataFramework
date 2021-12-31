using System.Diagnostics.CodeAnalysis;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using ModelFramework.Generators.Objects;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;
using TextTemplateTransformationFramework.Runtime;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class AttributeBuilderExtensionsTests
    {
        [Fact]
        public void Can_Generate_Class_With_GeneratedCodeAttribute()
        {
            // Arrange
            var sut = new ClassBuilder().WithName("TestClass");

            // Act
            sut.AddAttributes(new AttributeBuilder().ForCodeGenerator("MyCodeGenerator"));
            var code = GenerateCode(sut.Build());

            // Assert
            code.Should().Contain(@"[System.CodeDom.Compiler.GeneratedCodeAttribute(@""MyCodeGenerator"", @""1.0.0.0"")]");
        }

        private static string GenerateCode(IClass input)
            => TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(), new[] { input });
    }
}
