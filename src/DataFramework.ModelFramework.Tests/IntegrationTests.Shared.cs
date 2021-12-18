using System.Diagnostics.CodeAnalysis;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using ModelFramework.Generators.Objects;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.CodeStatements.Builders;
using ModelFramework.Objects.Contracts;
using TextTemplateTransformationFramework.Runtime;

namespace DataFramework.ModelFramework.Tests
{
    [ExcludeFromCodeCoverage]
    public partial class IntegrationTests
    {
        private const string CreateResultEntitytatement = "// additional code goes here";

        private static string GenerateCode(ITypeBase input, GeneratorSettings settings)
            => TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(),
                                                      new[] { input },
                                                      additionalParameters: new
                                                      {
                                                          EnableNullableContext = settings.EnableNullableContext,
                                                          CreateCodeGenerationHeader = settings.CreateCodeGenerationHeaders,
                                                          EnvironmentVersion = "1.0.0"
                                                      });

        private static DataObjectInfo CreateDataObjectInfo(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithDescription("Description goes here")
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsRequired().WithPropertyType(typeof(long)),
                    new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired(),
                    new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable(),
                    new FieldInfoBuilder().WithName("IsExistingEntity").WithType(typeof(bool)).WithIsComputed().WithIsPersistable(false).AddComputedFieldStatements(new LiteralCodeStatementBuilder().WithStatement("return Id > 0;"))
                )
                .WithEntityClassType(entityClassType)
                .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)

                .WithEntityNamespace("Entities")
                .WithEntityVisibility(Visibility.Internal)
                .AddEntityInterfaces("ITestEntity")
                .AddEntityAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithEntityBuilderNamespace("EntityBuilders")
                .AddEntityBuilderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithEntityIdentityNamespace("EntityIdentities")
                .AddEntityIdentityAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithQueryNamespace("Queries")
                .AddQueryAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .AddQueryInterfaces("IMyQuery")
                .AddQueryValidFieldNames("AdditionalValidFieldName")
                .AddQueryValidFieldNameStatements(new LiteralCodeStatementBuilder().WithStatement(@"return expression.FieldName.StartsWith(""ExtraField"") || ValidFieldNames.Any(s => s.Equals(expression.FieldName, StringComparison.OrdinalIgnoreCase));"))

                .WithRepositoryNamespace("Repositories")
                .WithRepositoryInterfaceNamespace("Contracts.Repositories")
                .WithRepositoryVisibility(Visibility.Internal)
                .AddRepositoryAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .AddRepositoryInterfaces("IMyRepository")

                .WithCommandProviderNamespace("DatabaseCommandProviders")
                .WithCommandProviderVisibility(Visibility.Internal)
                .AddCommandProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithQueryFieldProviderNamespace("QueryFieldProviders")
                .WithQueryFieldProviderVisibility(Visibility.Internal)
                .AddQueryFieldProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithEntityRetrieverSettingsNamespace("EntityRetrieverSettings")
                .WithEntityRetrieverSettingsVisibility(Visibility.Internal)
                .AddEntityRetrieverSettingsAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithAddStoredProcedureName("InsertTestEntity")
                .WithUpdateStoredProcedureName("UpdateTestEntity")
                .WithDeleteStoredProcedureName("DeleteTestEntity")

                .WithCommandEntityProviderNamespace("CommandEntityProviders")
                .WithCommandEntityProviderVisibility(Visibility.Internal)
                .AddCommandEntityProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .AddAddResultEntityStatements(new LiteralCodeStatementBuilder().WithStatement(CreateResultEntitytatement).Build())
                .AddUpdateResultEntityStatements(new LiteralCodeStatementBuilder().WithStatement(CreateResultEntitytatement).Build())
                .AddDeleteResultEntityStatements(new LiteralCodeStatementBuilder().WithStatement(CreateResultEntitytatement).Build())

                .Build();

        private static DataObjectInfo CreateDataObjectInfoInsertOnly(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsRequired().WithPropertyType(typeof(long)),
                    new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired(),
                    new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable(),
                    new FieldInfoBuilder().WithName("IsExistingEntity").WithType(typeof(bool)).WithIsComputed().WithIsPersistable(false).AddComputedFieldStatements(new LiteralCodeStatementBuilder().WithStatement("return Id > 0;"))
                )
                .WithEntityClassType(entityClassType)
                .WithCommandProviderNamespace("DatabaseCommandProviders")
                .WithCommandProviderVisibility(Visibility.Internal)
                .AddCommandProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .WithPreventUpdate()
                .WithPreventDelete()
                .Build();

        private static DataObjectInfo CreateDataObjectInfoWithoutStoredProcedures(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsRequired().WithPropertyType(typeof(long)),
                    new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired(),
                    new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable(),
                    new FieldInfoBuilder().WithName("IsExistingEntity").WithType(typeof(bool)).WithIsComputed().WithIsPersistable(false).AddComputedFieldStatements(new LiteralCodeStatementBuilder().WithStatement("return Id > 0;"))
                )
                .WithEntityClassType(entityClassType)
                .WithCommandProviderNamespace("DatabaseCommandProviders")
                .WithCommandProviderVisibility(Visibility.Internal)
                .AddCommandProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .Build();
    }
}
