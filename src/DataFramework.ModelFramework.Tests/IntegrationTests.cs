using System.Diagnostics.CodeAnalysis;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using ModelFramework.Generators.Objects;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.CodeStatements.Builders;
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
            var input = CreateDataObjectInfo(entityClassType).ToEntityClass(settings);

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
            var dataObjectInfo = CreateDataObjectInfo(entityClassType);
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
            var input = CreateDataObjectInfo(entityClassType).ToEntityIdentityClass(settings);

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
            var input = CreateDataObjectInfo(default(EntityClassType)).ToQueryClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Fact]
        public void Can_Generate_Repository()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(default(EntityClassType)).ToRepositoryClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Fact]
        public void Can_Generate_RepositoryInterface()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(default(EntityClassType)).ToRepositoryInterface(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        private static string GenerateCode(ITypeBase input, GeneratorSettings settings)
            => TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(),
                                                      new[] { input },
                                                      additionalParameters: new
                                                      {
                                                          EnableNullableContext = settings.EnableNullableContext,
                                                          CreateCodeGenerationHeader = settings.CreateCodeGenerationHeaders
                                                      });

        private static DataObjectInfo CreateDataObjectInfo(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithEntityNamespace("Entities")
                .WithEntityVisibility(Visibility.Internal)
                .AddEntityInterfaces("ITestEntity")
                .AddEntityAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .WithEntityBuilderNamespace("EntityBuilders")
                .AddEntityBuilderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .WithEntityIdentityNamespace("EntityIdentities")
                .WithQueryNamespace("Queries")
                .AddQueryAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .AddQueryInterfaces("IMyQuery")
                .AddQueryValidFieldNames("AdditionalValidFieldName")
                .WithDescription("Description goes here")
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsRequired().WithPropertyType(typeof(long)),
                    new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired(),
                    new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable(),
                    new FieldInfoBuilder().WithName("IsExistingEntity").WithType(typeof(bool)).WithIsComputed().AddComputedFieldStatements(new LiteralCodeStatementBuilder().WithStatement("return Id > 0;"))
                )
                .WithEntityClassType(entityClassType)
                .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
                .WithRepositoryNamespace("Repositories")
                .WithRepositoryInterfaceNamespace("Contracts.Repositories")
                .WithRepositoryVisibility(Visibility.Internal)
                .AddRepositoryAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .AddRepositoryInterfaces("IMyRepository")
                .WithCommandProviderNamespace("DatabaseCommandProviders")
                .AddCommandProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .Build();
    }
}
