using System.Diagnostics.CodeAnalysis;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using ModelFramework.Generators.Objects;
using TextTemplateTransformationFramework.Runtime;
using Xunit;

namespace DataFramework.ModelFramework.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTests
    {
        [Theory]
        [InlineData(EntityClassType.ImmutableClass)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void Can_Generate_Entities(EntityClassType entityClassType)
        {
            // Arrange
            var input = CreateDataObjectInfoBuilder(entityClassType)
                .ToEntityClassBuilder()
                .Build();

            // Act
            var actual = TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(), new[] { input }, additionalParameters: new { EnableNullableContext = true });

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(EntityClassType.ImmutableClass)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void Can_Generate_EntityBuilders(EntityClassType entityClassType)
        {
            // Arrange
            var input = CreateDataObjectInfoBuilder(entityClassType)
                .ToEntityBuilderClassBuilder()
                .Build();

            // Act
            var actual = TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(), new[] { input }, additionalParameters: new { EnableNullableContext = true });

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(EntityClassType.ImmutableClass)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void Can_Generate_EntityIdentities(EntityClassType entityClassType)
        {
            // Arrange
            var input = CreateDataObjectInfoBuilder(entityClassType)
                .ToEntityIdentityClassBuilder()
                .Build();

            // Act
            var actual = TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(), new[] { input }, additionalParameters: new { EnableNullableContext = true });

            // Assert
            actual.Should().NotBeEmpty();
        }

        private static DataObjectInfo CreateDataObjectInfoBuilder(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithTypeName("MyNamespace.TestEntity")
                .WithDescription("Description goes here")
                .AddFields(new FieldInfoBuilder().WithName("Id").WithType(typeof(long)).WithIsIdentityField())
                .AddFields(new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired())
                .AddFields(new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable())
                .WithEntityClassType(entityClassType)
                .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
                .Build();
    }
}
