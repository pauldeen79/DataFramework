namespace DataFramework.ModelFramework.Tests.Extensions;

public class BooleanExtensionsTests
{
    [Theory]
    [InlineData(true, Visibility.Public)]
    [InlineData(false, Visibility.Internal)]
    public void ToVisibility_Returns_Correct_Result(bool input, Visibility expectedOutput)
    {
        // Act
        var actual = input.ToVisibility();

        // Assert
        actual.Should().Be(expectedOutput);
    }
}
