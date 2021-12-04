using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class EnumerableOfModelFrameworkMetadataExtensionsTests
    {
        [Fact]
        public void Convert_Converts_DataFramework_Metadata_To_ModelFramework_Metadata()
        {
            // Arrange
            var input = new List<Abstractions.IMetadata>()
            {
                new Core.Metadata("Name", "Value")
            };

            // Act
            var actual = input.Convert();

            // Assert
            actual.Should().ContainSingle();
            actual.First().Name.Should().Be(input.First().Name);
            actual.First().Value.Should().Be(input.First().Value);
        }
    }
}
