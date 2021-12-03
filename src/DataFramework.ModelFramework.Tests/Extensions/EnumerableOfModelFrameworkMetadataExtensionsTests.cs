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
            var input = new List<DataFramework.Abstractions.IMetadata>()
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

        [Fact]
        public void Convert_Adds_DefaultUsings_When_Supplied()
        {
            // Arrange
            var input = new List<DataFramework.Abstractions.IMetadata>()
            {
                new Core.Metadata("Name", "Value")
            };

            // Act
            var actual = input.Convert(defaultUsings: new[] { "First", "Second" });

            // Assert
            actual.Should().HaveCount(3);
            actual.First().Name.Should().Be(input.First().Name);
            actual.First().Value.Should().Be(input.First().Value);
            actual.ElementAt(1).Name.Should().Be(global::ModelFramework.Objects.MetadataNames.CustomUsing);
            actual.ElementAt(1).Value.Should().Be("First");
            actual.ElementAt(2).Name.Should().Be(global::ModelFramework.Objects.MetadataNames.CustomUsing);
            actual.ElementAt(2).Value.Should().Be("Second");
        }
    }
}
