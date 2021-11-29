using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataFramework.Core.Builders;
using FluentAssertions;
using Xunit;

namespace DataFramework.Core.Tests.Builders
{
    [ExcludeFromCodeCoverage]
    public class FieldInfoBuilderTests
    {
        [Fact]
        public void Can_Build_Minimal_Entity()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test");

            // Act
            var actual = sut.Build();

            // Assert
            actual.Name.Should().Be("Test");
        }

        [Fact]
        public void Can_Build_Entity_With_All_Properties()
        {
            // Arrange
            var sut = CreateFilledFieldInfoBuilder();

            // Act
            var actual = sut.Build();

            // Assert
            actual.Name.Should().Be(sut.Name);
            actual.CanGet.Should().Be(sut.CanGet);
            actual.CanSet.Should().Be(sut.CanSet);
            actual.DefaultValue.Should().Be(sut.DefaultValue);
            actual.Description.Should().Be(sut.Description);
            actual.DisplayName.Should().Be(sut.DisplayName);
            actual.IsComputed.Should().Be(sut.IsComputed);
            actual.IsIdentityField.Should().Be(sut.IsIdentityField);
            actual.IsPersistable.Should().Be(sut.IsPersistable);
            actual.IsReadOnly.Should().Be(sut.IsReadOnly);
            actual.IsVisible.Should().Be(sut.IsVisible);
            actual.TypeName.Should().Be(sut.TypeName);
            actual.UseForCheckOnOriginalValues.Should().Be(sut.UseForCheckOnOriginalValues);
            actual.Metadata.Should().HaveCount(1);
            actual.Metadata.First().Name.Should().Be(sut.Metadata.First().Name);
            actual.Metadata.First().Value.Should().Be(sut.Metadata.First().Value);
        }

        [Fact]
        public void Can_Clear_Entity_With_All_Properties()
        {
            // Arrange
            var sut = CreateFilledFieldInfoBuilder();

            // Act
            var actual = sut.Clear();

            // Assert
            actual.WithName("TestEntity").Build().Should().BeEquivalentTo(new FieldInfoBuilder().WithName("TestEntity").Build());
        }

        [Fact]
        public void Can_Create_FieldInfoBuilder_From_Existing_Entity()
        {
            // Arrange
            var input = CreateFilledFieldInfoBuilder().Build();

            // Act
            var actual = new FieldInfoBuilder(input);

            // Assert
            actual.Build().Should().BeEquivalentTo(input);
        }

        [Fact]
        public void Can_Add_MetadataBuilder()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test");

            // Act
            sut.AddMetadata(new MetadataBuilder());

            // Assert
            sut.Metadata.Should().HaveCount(1);
            sut.Metadata.First().Name.Should().BeEmpty();
        }

        [Fact]
        public void Can_Add_MetadataBuilder_Using_Enumerable()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test");

            // Act
            sut.AddMetadata(new[] { new MetadataBuilder() }.AsEnumerable());

            // Assert
            sut.Metadata.Should().HaveCount(1);
            sut.Metadata.First().Name.Should().BeEmpty();
        }

        private static FieldInfoBuilder CreateFilledFieldInfoBuilder()
            => new FieldInfoBuilder()
                .WithName("TestEntity")
                .WithCanGet(false)
                .WithCanSet(false)
                .WithDefaultValue("some default value")
                .WithDescription("Description")
                .WithDisplayName("Display name")
                .WithIsComputed(true)
                .WithIsIdentityField(true)
                .WithIsPersistable(false)
                .WithIsReadOnly(true)
                .WithIsVisible(false)
                .WithType(typeof(string))
                .WithUseForCheckOnOriginalValues(true)
                .AddMetadata("Name1", "Value1");
    }
}
