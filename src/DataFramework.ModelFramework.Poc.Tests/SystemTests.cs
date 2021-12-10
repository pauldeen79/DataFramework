using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Poc.Tests
{
    [ExcludeFromCodeCoverage]
    public class SystemTests
    {
#nullable enable
        [Fact]
        public void Can_Use_OfType_To_Filter_Out_Null_Values_In_Nullable_String_Array()
        {
            // Arrange
            var input = new string?[] { "A", "B", null, "C" };

            // Act
            var actual = input.OfType<string>();

            // Assert
            actual.Should().BeEquivalentTo(new[] { "A", "B", "C" });
        }
#nullable restore
    }
}
