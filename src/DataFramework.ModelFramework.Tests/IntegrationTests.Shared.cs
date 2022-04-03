namespace DataFramework.ModelFramework.Tests;

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

    private static IDataObjectInfo CreateDataObjectInfo(EntityClassType entityClassType)
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
            .AddQueryValidFieldNameStatements(@"    return expression.FieldName.StartsWith(""ExtraField"") || ValidFieldNames.Any(s => s.Equals(expression.FieldName, StringComparison.OrdinalIgnoreCase));")

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

            .WithPagedEntityRetrieverSettingsNamespace("PagedEntityRetrieverSettings")
            .WithPagedEntityRetrieverSettingsVisibility(Visibility.Internal)
            .AddPagedEntityRetrieverSettingsAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

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

    private static IDataObjectInfo CreateDataObjectInfoInsertOnly(EntityClassType entityClassType)
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

    private static IDataObjectInfo CreateDataObjectInfoWithoutStoredProcedures(EntityClassType entityClassType)
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

    private static IDataObjectInfo CreateDataObjectInfoWithCustomQueryFieldProviderStuff()
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
            .AddQueryFieldProviderConstructorCodeStatements("_extraFields = extraFields;")
            .AddQueryFieldProviderGetAllFieldsCodeStatements
            (
                "yield return @\"CustomExtraField\";",
                "yield return @\"AllFields\";"
            )
            .AddQueryFieldProviderGetDatabaseFieldNameCodeStatements
            (
                "var extraField = _extraFields.FirstOrDefault(x => x.Name == queryFieldName);",
                "if (extraField != null)",
                "{",
                "    return string.Format(\"ExtraField{0}\", extraField.FieldNumber);",
                "}",
                "if (queryFieldName == \"AllFields\")",
                "{",
                "    return \"[Name] + ' ' + COALESCE([Description], '')\";",
                "}"
            )
            .Build();

    private static IDataObjectInfo CreateDataObjectInfoWithCollectionField()
        => new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddFields
            (
                new FieldInfoBuilder().WithName("Id")
                                      .WithType(typeof(int))
                                      .WithIsIdentityField()
                                      .WithIsRequired()
                                      .WithPropertyTypeName(typeof(long).FullName),
                new FieldInfoBuilder().WithName("Tags")
                                      .WithType(typeof(List<string>))
            )
            .Build();
}
