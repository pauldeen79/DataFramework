using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using ModelFramework.Objects.Builders;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class CollectionOfAttributeBuilderExtensionsTests
    {
        [Fact]
        public void AddConditionalModelClassAttribute_Does_Not_Add_Attribute_When_Condition_Is_False()
        {
            // Arrange
            var sut = new Collection<AttributeBuilder>();

            // Act
            sut.AddConditionalModelClassAttribute("Test", "value", false);

            // Assert
            sut.Should().BeEmpty();
        }

        [Fact]
        public void AddConditionalModelClassAttribute_Adds_Attribute_When_Condition_Is_True()
        {
            // Arrange
            var sut = new Collection<AttributeBuilder>();

            // Act
            sut.AddConditionalModelClassAttribute("Test", "value", true);

            // Assert
            sut.Should().ContainSingle();
        }

        [Fact]
        public void AddModelClassAttribute_Does_Not_Add_Attribute_When_Value_Is_Null()
        {
            // Arrange
            var sut = new Collection<AttributeBuilder>();

            // Act
            sut.AddModelClassAttribute("Test", null);

            // Assert
            sut.Should().BeEmpty();
        }

        [Fact]
        public void AddModelClassAttribute_Adds_Attribute_When_Value_Is_Not_Null()
        {
            // Arrange
            var sut = new Collection<AttributeBuilder>();

            // Act
            sut.AddModelClassAttribute("Test", "value");

            // Assert
            sut.Should().ContainSingle();
        }
    }
}
