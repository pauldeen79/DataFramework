namespace DataFramework.CodeGeneration.Tests;

public class IntegrationTests
{
    private static readonly CodeGenerationSettings Settings = new CodeGenerationSettings
    (
        basePath: Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\"),
        generateMultipleFiles: false,
        dryRun: true
    );

    [Fact]
    public void CanGenerateAll()
    {
        Verify(GenerateCode.For<Builders>(Settings));
        Verify(GenerateCode.For<Records>(Settings));
    }

    private void Verify(GenerateCode generatedCode)
    {
        if (Settings.DryRun)
        {
            var actual = generatedCode.GenerationEnvironment.ToString();

            // Assert
            actual.NormalizeLineEndings().Should().NotBeNullOrEmpty().And.NotStartWith("Error:");
        }
    }
}
