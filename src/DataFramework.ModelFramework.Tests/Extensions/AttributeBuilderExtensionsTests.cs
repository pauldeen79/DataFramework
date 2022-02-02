namespace DataFramework.ModelFramework.Tests.Extensions;

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
