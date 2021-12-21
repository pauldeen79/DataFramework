using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using ModelFramework.Database.Builders;
using ModelFramework.Database.Contracts;
using ModelFramework.Generators.Database;
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

        private static string GenerateCode(IEnumerable<ISchema> input, GeneratorSettings settings)
            => TemplateRenderHelper.GetTemplateOutput(new SqlServerDatabaseSchemaGenerator(),
                                                      input,
                                                      additionalParameters: new
                                                      {
                                                          CreateCodeGenerationHeader = settings.CreateCodeGenerationHeaders
                                                      });

        private static DataObjectInfo CreateDataObjectInfo(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithDescription("Description goes here")
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithSqlIdentity().WithIsRequired(),
                    new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired(),
                    new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable(),
                    new FieldInfoBuilder().WithName("IsExistingEntity").WithType(typeof(bool)).WithIsComputed().WithIsPersistable(false).AddComputedFieldStatements(new LiteralCodeStatementBuilder().WithStatement("return Id > 0;").Build())
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

                .WithCommandEntityProviderNamespace("CommandEntityProviders")
                .WithCommandEntityProviderVisibility(Visibility.Internal)
                .AddCommandEntityProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .AddAddResultEntityStatements(new LiteralCodeStatementBuilder().WithStatement(CreateResultEntitytatement).Build())
                .AddUpdateResultEntityStatements(new LiteralCodeStatementBuilder().WithStatement(CreateResultEntitytatement).Build())
                .AddDeleteResultEntityStatements(new LiteralCodeStatementBuilder().WithStatement(CreateResultEntitytatement).Build())

                .WithEntityMapperNamespace("EntityMappers")
                .WithEntityMapperVisibility(Visibility.Internal)
                .AddEntityMapperAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .AddEntityMapperCustomMappings(new KeyValuePair<string, object>("IsExistingEntity", true))

                .WithHasAddStoredProcedure()
                .WithHasUpdateStoredProcedure()
                .WithHasDeleteStoredProcedure()
                .AddPrimaryKeyConstraints(new PrimaryKeyConstraintBuilder().WithName("PK_TestEntity").AddFields(new PrimaryKeyConstraintFieldBuilder().WithName("Id")))

                .Build();

        private static DataObjectInfo CreateDataObjectInfoInsertOnly(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithSqlIdentity().WithIsRequired().WithPropertyType(typeof(int)),
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

        private static DataObjectInfo CreateDataObjectInfoWithCustomQueryFieldProviderStuff()
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsRequired().WithPropertyTypeName(typeof(long).FullName),
                    new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired(),
                    new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable(),
                    new FieldInfoBuilder().WithName("IsExistingEntity").WithType(typeof(bool)).WithIsComputed().WithIsPersistable(false).AddComputedFieldStatements(new LiteralCodeStatementBuilder().WithStatement("return Id > 0;")),
                    new FieldInfoBuilder().WithName("ExtraField").WithType(typeof(string)).WithIsNullable().WithSkipFieldInQueryFieldProvider()
                )
                .AddQueryFieldProviderFields(new ClassFieldBuilder().WithName("_extraFields").WithReadOnly().WithTypeName("IEnumerable<ExtraField>"))
                .AddQueryFieldProviderConstructorParameters(new ParameterBuilder().WithName("extraFields").WithTypeName("IEnumerable<ExtraField>"))
                .AddQueryFieldProviderConstructorCodeStatements(new LiteralCodeStatementBuilder().WithStatement("_extraFields = extraFields;"))
                .AddQueryFieldProviderGetAllFieldsCodeStatements(new LiteralCodeStatementBuilder().WithStatement("yield return @\"CustomExtraField\";"))
                .AddQueryFieldProviderGetAllFieldsCodeStatements(new LiteralCodeStatementBuilder().WithStatement("yield return @\"AllFields\";"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("var extraField = _extraFields.FirstOrDefault(x => x.Name == queryFieldName);"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("if (extraField != null)"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("{"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("    return string.Format(\"ExtraField{0}\", extraField.FieldNumber);"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("}"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("if (queryFieldName == \"AllFields\")"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("{"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("    return \"[Name] + ' ' + COALESCE([Description], '')\";"))
                .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(new LiteralCodeStatementBuilder().WithStatement("}"))
                .Build();
    }
}
