using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
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
        [InlineData(EntityClassType.ImmutablePoco)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void GetEntityClassType_Returns_Correct_Value_When_Specified(EntityClassType value)
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithEntityClassType(value)
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
                .AddAdditionalDataObjectInfos
                (
                    new DataObjectInfoBuilder().WithName("FirstAdditional").Build(),
                    new DataObjectInfoBuilder().WithName("SecondAdditional").Build()
                )
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
