namespace DataFramework.Core.Tests.Builders;

public class DataObjectInfoBuilderTests
{
    private static DataObjectInfoBuilder CreateSut() => new DataObjectInfoBuilder();

    [Fact(Skip = "Can only pass after fixing problem in ClassFramework, and then replace ModelFramework code generation in DataFramework with ClassFramework")]
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
