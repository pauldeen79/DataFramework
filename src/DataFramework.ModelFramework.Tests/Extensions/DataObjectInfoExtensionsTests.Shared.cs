using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using DataFramework.ModelFramework.MetadataNames;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class DataObjectInfoExtensionsTests
    {
        [Fact]
        public void GetEntityClassType_Returns_Poco_When_Not_Specified()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder().WithName("TestEntity").Build();

            // Act
            var actual = sut.GetEntityClassType();

            // Assert
            actual.Should().Be(EntityClassType.Poco);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetEntityClassType_Returns_Poco_When_Specified_As_Empty(string value)
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata(Entities.EntityClassType, value))
                .Build();

            // Act
            var actual = sut.GetEntityClassType();

            // Assert
            actual.Should().Be(EntityClassType.Poco);
        }

        [Fact]
        public void GetEntityClassType_Returns_Poco_When_Specified_Unknown_Value()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata(Entities.EntityClassType, "some unknown value"))
                .Build();

            // Act
            var actual = sut.GetEntityClassType();

            // Assert
            actual.Should().Be(EntityClassType.Poco);
        }

        [Theory]
        [InlineData(EntityClassType.ImmutablePoco)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void GetEntityClassType_Returns_Correct_Value_When_Specified_As_String(EntityClassType value)
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata(Entities.EntityClassType, value.ToString()))
                .Build();

            // Act
            var actual = sut.GetEntityClassType();

            // Assert
            actual.Should().Be(value);
        }

        [Fact]
        public void WithAdditionalDataObjectInfos_Returns_Unchanged_Enumerable_When_Not_Found()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .Build();

            // Act
            var actual = sut.WithAdditionalDataObjectInfos();

            // Assert
            actual.Should().BeEquivalentTo(new[] { sut });
        }

        [Fact]
        public void WithAdditionalDataObjectInfos_Returns_Correct_Enumerable_When_Found()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata(Shared.CustomDataObjectInfoName, new DataObjectInfoBuilder().WithName("FirstAdditional").Build()))
                .AddMetadata(new Metadata(Shared.CustomDataObjectInfoName, new DataObjectInfoBuilder().WithName("SecondAdditional").Build()))
                .Build();

            // Act
            var actual = sut.WithAdditionalDataObjectInfos();

            // Assert
            actual.Should().HaveCount(3);
            actual.First().Should().Be(sut);
            actual.ElementAt(1).Name.Should().Be("FirstAdditional");
            actual.ElementAt(2).Name.Should().Be("SecondAdditional");
        }
    }
}
