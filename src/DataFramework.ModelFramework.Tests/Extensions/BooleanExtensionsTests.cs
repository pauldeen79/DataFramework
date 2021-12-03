using System.Diagnostics.CodeAnalysis;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using ModelFramework.Objects.Contracts;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
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
}
