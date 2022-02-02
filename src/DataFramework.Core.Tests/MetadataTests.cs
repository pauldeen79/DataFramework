namespace DataFramework.Core.Tests;

public class MetadataTests
{
    [Fact]
    public void Ctor_Throws_On_Empty_Name()
    {
        // Arrange
        var action = new Action(() => new MetadataBuilder().WithName(string.Empty).Build());

        // Act & Assert
        action.Should().Throw<ValidationException>().WithMessage("Name cannot be null or whitespace");
    }
}
