namespace DataFramework.CodeGeneration.Models.Pipelines;

internal interface IPipelineSettings
{
    bool EnableNullableContext { get; }
    ConcurrencyCheckBehavior ConcurrencyCheckBehavior { get; }
    EntityClassType EntityClassType { get; }
    [Required(AllowEmptyStrings = true)] string DefaultEntityNamespace { get; }
    [Required(AllowEmptyStrings = true)] string DefaultIdentityNamespace { get; }
    [Required(AllowEmptyStrings = true)] string DefaultBuilderNamespace { get; }
    [DefaultValue(true)] bool AddComponentModelAttributes { get; }

    // CommandEntityProvider settings
    Visibility CommandEntityProviderVisibility { get; }
    [Required(AllowEmptyStrings = true)] string CommandEntityProviderNamespace { get; }
    [DefaultValue(true)] bool CommandProviderEnableAdd { get; }
    [DefaultValue(true)] bool CommandProviderEnableUpdate { get; }
    [DefaultValue(true)] bool CommandProviderEnableDelete { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderAddResultEntityStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderAddAfterReadStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderUpdateResultEntityStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderUpdateAfterReadStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderDeleteResultEntityStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderDeleteAfterReadStatements { get; }

    // CommandProvider settings
    Visibility CommandProviderVisibility { get; }
    [Required(AllowEmptyStrings = true)] string CommandProviderNamespace { get; }
    bool UseAddStoredProcedure { get; }
    bool UseUpdateStoredProcedure { get; }
    bool UseDeleteStoredProcedure { get; }

    // DatabaseEntityRetrieverProvider settings
    Visibility DatabaseEntityRetrieverProviderVisibility { get; }
    [Required(AllowEmptyStrings = true)] string DatabaseEntityRetrieverProviderNamespace { get; }

    // Database settings
    [Required] string AddStoredProcedureName { get; }
    [Required] string UpdateStoredProcedureName { get; }
    [Required] string DeleteStoredProcedureName { get; }
    [Required][ValidateObject] IReadOnlyCollection<SqlStatementBase> AddStoredProcedureStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<SqlStatementBase> UpdateStoredProcedureStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<SqlStatementBase> DeleteStoredProcedureStatements { get; }
    [DefaultValue(DatabaseOperation.Insert)] DatabaseOperation DatabaseCommandTypeForInsertText { get; }
    [DefaultValue(DatabaseOperation.Insert)] DatabaseOperation DatabaseCommandTypeForInsertParameters { get; }
    [DefaultValue(DatabaseOperation.Update)] DatabaseOperation DatabaseCommandTypeForUpdateText { get; }
    [DefaultValue(DatabaseOperation.Update)] DatabaseOperation DatabaseCommandTypeForUpdateParameters { get; }
    [DefaultValue(DatabaseOperation.Delete)] DatabaseOperation DatabaseCommandTypeForDeleteText { get; }
    [DefaultValue(DatabaseOperation.Delete)] DatabaseOperation DatabaseCommandTypeForDeleteParameters { get; }

    // EntityMapper settings
    Visibility EntityMapperVisibility { get; }
    [Required(AllowEmptyStrings = true)] string EntityMapperNamespace { get; }

    // EntityRetriever settings
    Visibility EntityRetrieverVisibility { get; }
    [Required(AllowEmptyStrings = true)] string EntityRetrieverNamespace { get; }

    // IdentityCommandProvider settings
    Visibility IdentityCommandProviderVisibility { get; }
    [Required(AllowEmptyStrings = true)] string IdentityCommandProviderNamespace { get; }

    // PagedEntityRetrieverSettings settings
    Visibility PagedEntityRetrieverSettingsVisibility { get; }
    [Required(AllowEmptyStrings = true)] string PagedEntityRetrieverSettingsNamespace { get; }

    // Query settings
    Visibility QueryVisibility { get; }
    [Required(AllowEmptyStrings = true)] string QueryNamespace { get; }
    int? QueryMaxLimit { get; }
    bool CreateQueryAsRecord { get; }

    // QueryFieldInfo settings
    Visibility QueryFieldInfoVisibility { get; }
    [Required(AllowEmptyStrings = true)] string QueryFieldInfoNamespace { get; }
    [Required][ValidateObject] IReadOnlyCollection<Field> QueryFieldInfoFields { get; }
    [Required][ValidateObject] IReadOnlyCollection<Parameter> QueryFieldInfoConstructorParameters { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> QueryFieldInfoConstructorCodeStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> QueryFieldInfoGetAllFieldsCodeStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> QueryFieldInfoGetDatabaseFieldNameCodeStatements { get; }

    // Repository settings
    Visibility RepositoryVisibility { get; }
    [Required(AllowEmptyStrings = true)] string RepositoryNamespace { get; }
    [Required(AllowEmptyStrings = true)] string RepositoryInterfaceNamespace { get; }
}
