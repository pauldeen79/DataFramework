namespace DataFramework.CodeGeneration.Models;

public interface IDataObjectInfo
{
    [Required] string Name { get; }
    string? AssemblyName { get; }
    string? TypeName { get; }
    string? Description { get; }
    string? DisplayName { get; }
    [DefaultValue(true)] bool IsVisible { get; }
    [DefaultValue(true)] bool IsQueryable { get; }
    bool IsReadOnly { get; }
    [Required][ValidateObject] IReadOnlyCollection<IFieldInfo> Fields { get; }

    [Required(AllowEmptyStrings = true)] string DatabaseTableName { get; }
    [Required(AllowEmptyStrings = true)] string DatabaseSchemaName { get; }
    //TODO: Review if we want to store the 'CatalogName', which is the database name
    //[Required(AllowEmptyStrings = true)] string DatabaseCatalogName { get; }
    [Required(AllowEmptyStrings = true)] string DatabaseFileGroupName { get; }
    [Required(AllowEmptyStrings = true)] string CustomAddDatabaseCommandText { get; }
    [Required(AllowEmptyStrings = true)] string CustomUpdateDatabaseCommandText { get; }
    [Required(AllowEmptyStrings = true)] string CustomDeleteDatabaseCommandText { get; }
    [Required(AllowEmptyStrings = true)] string ViewDefinition { get; }

    [Required][ValidateObject] IReadOnlyCollection<PrimaryKeyConstraint> PrimaryKeyConstraints { get; }
    [Required][ValidateObject] IReadOnlyCollection<ForeignKeyConstraint> ForeignKeyConstraints { get; }
    [Required][ValidateObject] IReadOnlyCollection<DatabaseFramework.Domain.Index> Indexes { get; }
    [Required][ValidateObject] IReadOnlyCollection<CheckConstraint> CheckConstraints { get; }

    [Required][ValidateObject] IReadOnlyCollection<IEntityMapping> CustomEntityMappings { get; }
    string? DefaultOrderByFields { get; }
    string? DefaultWhereClause { get; }
    int? OverridePageSize { get; }
    [Required] IReadOnlyCollection<string> AdditionalQueryFields { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> QueryFieldNameStatements { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> QueryExpressionStatements { get; }
}
