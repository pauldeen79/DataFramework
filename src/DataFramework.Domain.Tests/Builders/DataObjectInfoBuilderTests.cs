namespace DataFramework.Domain.Tests.Builders;

public class DataObjectInfoBuilderTests
{
    private static DataObjectInfoBuilder CreateSut() => new DataObjectInfoBuilder();

    [Fact]
    public void Can_Validate_Recursively()
    {
        // Arrange
        var sut = CreateSut().AddFields(new FieldInfoBuilder());

        // Act
        var validationResults = new List<ValidationResult>();
        var success = sut.TryValidate(validationResults);

        // Assert
        success.Should().BeFalse();
        validationResults.Should().HaveCount(2); //both the validation errors in DataObjectInfo and FieldInfo
    }
}
