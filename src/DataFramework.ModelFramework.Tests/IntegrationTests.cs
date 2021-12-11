using System.Diagnostics.CodeAnalysis;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using ModelFramework.Generators.Objects;
using ModelFramework.Objects.Contracts;
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
        public void Can_Generate_Entity(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfoBuilder(entityClassType).ToEntityClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(EntityClassType.ImmutableClass)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void Can_Generate_EntityBuilder(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var dataObjectInfo = CreateDataObjectInfoBuilder(entityClassType);
            var input = dataObjectInfo.ToEntityBuilderClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(EntityClassType.ImmutableClass)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void Can_Generate_EntityIdentity(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfoBuilder(entityClassType).ToEntityIdentityClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Fact]
        public void Can_Generate_Query()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfoBuilder(default(EntityClassType)).ToQueryClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        private static string GenerateCode(IClass input, GeneratorSettings settings)
            => TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(),
                                                      new[] { input },
                                                      additionalParameters: new
                                                      {
                                                          EnableNullableContext = settings.EnableNullableContext,
                                                          CreateCodeGenerationHeader = settings.CreateCodeGenerationHeaders
                                                      });

        private static DataObjectInfo CreateDataObjectInfoBuilder(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithEntitiesNamespace("Entities")
                .WithEntityBuildersNamespace("EntityBuilders")
                .WithEntityIdentitiesNamespace("EntityIdentities")
                .WithQueriesNamespace("Queries")
                .WithDescription("Description goes here")
                .AddFields(new FieldInfoBuilder().WithName("Id").WithType(typeof(long)).WithIsIdentityField().WithIsRequired())
                .AddFields(new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired())
                .AddFields(new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable())
                .WithEntityClassType(entityClassType)
                .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
                .Build();
    }
}
