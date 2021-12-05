using System.Diagnostics.CodeAnalysis;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using DataFramework.ModelFramework.MetadataNames;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class FieldInfoExtensionsTests
    {
        [Fact]
        public void CreatePropertyName_Returns_Default_DeconflictionName_When_Name_Is_Equal_To_Entity_And_No_Custom_DeconflictionName_Is_Found()
        {
            // Arrange
            var fieldInfo = new FieldInfoBuilder().WithName("Name").Build();
            var dataObjectInfo = new DataObjectInfoBuilder().WithName("Name").Build();

            // Act
            var actual = fieldInfo.CreatePropertyName(dataObjectInfo);

            // Assert
            actual.Should().Be("NameProperty");
        }

        [Fact]
        public void CreatePropertyName_Returns_Custom_DeconflictionName_When_Name_Is_Equal_To_Entity_And_Custom_DeconflictionName_Is_Found()
        {
            // Arrange
            var fieldInfo = new FieldInfoBuilder().WithName("Name").Build();
            var dataObjectInfo = new DataObjectInfoBuilder().WithName("Name").AddMetadata(Shared.PropertyNameDeconflictionFormatString, "{0}Custom").Build();

            // Act
            var actual = fieldInfo.CreatePropertyName(dataObjectInfo);

            // Assert
            actual.Should().Be("NameCustom");
        }

        [Fact]
        public void CreatePropertyName_Returns_Original_Name_When_Name_Is_Not_Equal_To_Entity()
        {
            // Arrange
            var fieldInfo = new FieldInfoBuilder().WithName("Unchanged").Build();
            var dataObjectInfo = new DataObjectInfoBuilder().WithName("Entity").Build();

            // Act
            var actual = fieldInfo.CreatePropertyName(dataObjectInfo);

            // Assert
            actual.Should().Be("Unchanged");
        }
    }
}
