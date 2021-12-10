using System.Diagnostics.CodeAnalysis;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class EnumerableOfMetadataExtensionsTests
    {
        [Fact]
        public void GetMetadataValue_With_Enum_Returns_DefaultValue_When_Not_Specified()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder().WithName("TestEntity").Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.Record);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetMetadataValue_With_Enum_Returns_DefaultValue_When_Specified_As_Empty(string value)
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", value))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.Record);
        }

        [Fact]
        public void GetMetadataValue_With_Enum_Returns_DefaultValue_When_Specified_Unknown_Value()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", "some unknown value"))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.Record);
        }
        
        [Fact]
        public void GetMetadataValue_With_Enum_Returns_Correct_Value_When_Specified_As_String()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", EntityClassType.ObservablePoco.ToString()))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.ObservablePoco);
        }

        [Fact]
        public void GetMetadataValue_With_Enum_Returns_Correct_Value_When_Specified_As_Enum()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", EntityClassType.ObservablePoco))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.ObservablePoco);
        }

        [Fact]
        public void GetMetadataValue_With_Enum_Returns_Correct_Value_When_Specified_As_Int()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", (int)EntityClassType.ObservablePoco))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.ObservablePoco);
        }

        [Fact]
        public void GetMetadataValue_With_NullableEnum_Returns_DefaultValue_When_Not_Specified()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder().WithName("TestEntity").Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => default(EntityClassType?));

            // Assert
            actual.Should().BeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetMetadataValue_With_NullableEnum_Returns_DefaultValue_When_Specified_As_Empty(string value)
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", value))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => default(EntityClassType?));

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void GetMetadataValue_With_NullableEnum_Returns_DefaultValue_When_Specified_Unknown_Value()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", "some unknown value"))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => default(EntityClassType?));

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void GetMetadataValue_With_NullableEnum_Returns_Correct_Value_When_Specified_As_String()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", EntityClassType.ObservablePoco.ToString()))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue<EntityClassType?>("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.ObservablePoco);
        }

        [Fact]
        public void GetMetadataValue_With_NullableEnum_Returns_Correct_Value_When_Specified_As_Enum()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", EntityClassType.ObservablePoco))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue<EntityClassType?>("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.ObservablePoco);
        }

        [Fact]
        public void GetMetadataValue_With_NullableEnum_Returns_Correct_Value_When_Specified_As_Int()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", (int)EntityClassType.ObservablePoco))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue<EntityClassType?>("MyName", () => EntityClassType.Record);

            // Assert
            actual.Should().Be(EntityClassType.ObservablePoco);
        }

        [Fact]
        public void GetMetadataValue_No_Enum_Converts_Value_From_Int_To_String()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", 1))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => default(string));

            // Assert
            actual.Should().Be("1");
        }

        [Fact]
        public void GetMetadataValue_No_Enum_Converts_Value_From_String_To_Int()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddMetadata(new Metadata("MyName", "1"))
                .Build();

            // Act
            var actual = sut.Metadata.GetValue("MyName", () => int.MinValue);

            // Assert
            actual.Should().Be(1);
        }
    }
}
