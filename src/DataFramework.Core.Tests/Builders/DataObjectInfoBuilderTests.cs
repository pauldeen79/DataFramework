using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataFramework.Core.Builders;
using FluentAssertions;
using Xunit;

namespace DataFramework.Core.Tests.Builders
{
    [ExcludeFromCodeCoverage]
    public class DataObjectInfoBuilderTests
    {
        [Fact]
        public void Can_Build_Minimal_Entity()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder().WithName("Test");

            // Act
            var actual = sut.Build();

            // Assert
            actual.Name.Should().Be("Test");
        }

        [Fact]
        public void Can_Build_Entity_With_All_Properties()
        {
            // Arrange
            var sut = CreateFilledDataObjectInfoBuilder();

            // Act
            var actual = sut.Build();

            // Assert
            actual.Name.Should().Be(sut.Name);
            actual.AssemblyName.Should().Be(sut.AssemblyName);
            actual.Description.Should().Be(sut.Description);
            actual.DisplayName.Should().Be(sut.DisplayName);
            actual.TypeName.Should().Be(sut.TypeName);
            actual.IsQueryable.Should().Be(sut.IsQueryable);
            actual.IsReadOnly.Should().Be(sut.IsReadOnly);
            actual.IsVisible.Should().Be(sut.IsVisible);
            actual.Fields.Should().HaveCount(1);
            actual.Fields.First().CanGet.Should().Be(sut.Fields.First().CanGet);
            actual.Fields.First().CanSet.Should().Be(sut.Fields.First().CanSet);
            actual.Fields.First().DefaultValue.Should().Be(sut.Fields.First().DefaultValue);
            actual.Fields.First().Description.Should().Be(sut.Fields.First().Description);
            actual.Fields.First().DisplayName.Should().Be(sut.Fields.First().DisplayName);
            actual.Fields.First().IsComputed.Should().Be(sut.Fields.First().IsComputed);
            actual.Fields.First().IsIdentityField.Should().Be(sut.Fields.First().IsIdentityField);
            actual.Fields.First().IsPersistable.Should().Be(sut.Fields.First().IsPersistable);
            actual.Fields.First().IsReadOnly.Should().Be(sut.Fields.First().IsReadOnly);
            actual.Fields.First().IsVisible.Should().Be(sut.Fields.First().IsVisible);
            actual.Fields.First().Name.Should().Be(sut.Fields.First().Name);
            actual.Fields.First().TypeName.Should().Be(sut.Fields.First().TypeName);
            actual.Fields.First().UseForCheckOnOriginalValues.Should().Be(sut.Fields.First().UseForCheckOnOriginalValues);
            actual.Fields.First().Metadata.Should().HaveCount(1);
            actual.Fields.First().Metadata.First().Name.Should().Be(sut.Fields.First().Metadata.First().Name);
            actual.Fields.First().Metadata.First().Value.Should().Be(sut.Fields.First().Metadata.First().Value);
            actual.Metadata.Should().HaveCount(1);
            actual.Metadata.First().Name.Should().Be(sut.Metadata.First().Name);
            actual.Metadata.First().Value.Should().Be(sut.Metadata.First().Value);
        }

        [Fact]
        public void Can_Clear_Entity_With_All_Properties()
        {
            // Arrange
            var sut = CreateFilledDataObjectInfoBuilder();

            // Act
            var actual = sut.Clear();

            // Assert
            actual.WithName("TestEntity").Build().Should().BeEquivalentTo(new DataObjectInfoBuilder().WithName("TestEntity").Build());
        }

        [Fact]
        public void Can_Create_DataObjectInfoBuilder_From_Existing_Entity()
        {
            // Arrange
            var input = CreateFilledDataObjectInfoBuilder().Build();

            // Act
            var actual = new DataObjectInfoBuilder(input);

            // Assert
            actual.Build().Should().BeEquivalentTo(input);
        }

        [Fact]
        public void Can_Add_FieldInfoBuilder()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder().WithName("Test");

            // Act
            sut.AddFields(new FieldInfoBuilder());

            // Assert
            sut.Fields.Should().HaveCount(1);
            sut.Fields.First().Name.Should().BeEmpty();
        }

        [Fact]
        public void Can_Add_FieldInfoBuilder_Using_Enumerable()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder().WithName("Test");

            // Act
            sut.AddFields(new[] { new FieldInfoBuilder() }.AsEnumerable());

            // Assert
            sut.Fields.Should().HaveCount(1);
            sut.Fields.First().Name.Should().BeEmpty();
        }

        [Fact]
        public void Can_Add_MetadataBuilder()
        {
            // Arrange
            var sut = new DataObjectInfoBuilder().WithName("Test");

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
            var sut = new DataObjectInfoBuilder().WithName("Test");

            // Act
            sut.AddMetadata(new[] { new MetadataBuilder() }.AsEnumerable());

            // Assert
            sut.Metadata.Should().HaveCount(1);
            sut.Metadata.First().Name.Should().BeEmpty();
        }

        private static DataObjectInfoBuilder CreateFilledDataObjectInfoBuilder()
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithAssemblyName("MyAssembly")
                .WithDescription("My description")
                .WithDisplayName("TestEntityDisplay")
                .WithTypeName("MyAssembly.MyNamespace.TestEntity")
                .WithIsQueryable(false)
                .WithIsReadOnly(true)
                .WithIsVisible(false)
                .AddFields(new FieldInfoBuilder().WithName("Test").WithType(typeof(string)).AddMetadata("Name1", "Value1").Build())
                .AddMetadata("Name1", "Value1");
    }
}
