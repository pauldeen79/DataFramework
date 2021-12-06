using System.Diagnostics.CodeAnalysis;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using ModelFramework.Generators.Objects;
using ModelFramework.Objects.Contracts;
using ModelFramework.Objects.Extensions;
using ModelFramework.Objects.Settings;
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
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfoBuilder(entityClassType)
                .ToEntityClass(settings);

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
        public void Can_Generate_EntityBuilders(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfoBuilder(entityClassType)
                .ToEntityBuilderClass(settings);

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
        public void Can_Generate_EntityBuilders_Using_ModelFramework(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfoBuilder(entityClassType)
                .ToEntityClass(settings)
                .ToImmutableBuilderClass(new ImmutableBuilderClassSettings(addCopyConstructor: true,
                                                                           poco: entityClassType.HasPropertySetter(),
                                                                           addNullChecks: settings.EnableNullableContext));

            if (entityClassType == EntityClassType.ObservablePoco)
            {
                input = input.ToObservableClass();
            }

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
        public void Can_Generate_EntityIdentities(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfoBuilder(entityClassType)
                .ToEntityIdentityClass(settings);

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
