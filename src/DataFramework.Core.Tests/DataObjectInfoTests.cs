namespace DataFramework.Core.Tests;

public class DataObjectInfoTests
{
    [Fact]
    public void Ctor_Throws_On_Empty_Name()
    {
        // Arrange
        var action = new Action(() => new DataObjectInfoBuilder().WithName(string.Empty).Build());

        // Act & Assert
        action.Should().Throw<ValidationException>().WithMessage("Name cannot be null or whitespace");
    }
}
