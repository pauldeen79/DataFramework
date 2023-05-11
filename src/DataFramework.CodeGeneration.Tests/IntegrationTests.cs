namespace DataFramework.CodeGeneration.Tests;

public class IntegrationTests
{
    private static readonly CodeGenerationSettings Settings = new CodeGenerationSettings
    (
        basePath: Path.Combine(Directory.GetCurrentDirectory(), @"../../../../"),
        generateMultipleFiles: true,
        dryRun: true
    );

    [Fact]
    public void CanGenerateAll()
    {
        // Arrange
        var multipleContentBuilder = new MultipleContentBuilder { BasePath = Settings.BasePath };

        // Act
        GenerateCode.For<Builders>(Settings, multipleContentBuilder);
        GenerateCode.For<Models>(Settings, multipleContentBuilder);
        GenerateCode.For<Records>(Settings, multipleContentBuilder);
        var actual = multipleContentBuilder.ToString();

        // Assert
        actual.NormalizeLineEndings().Should().NotBeNullOrEmpty().And.NotStartWith("Error:");
    }
}
