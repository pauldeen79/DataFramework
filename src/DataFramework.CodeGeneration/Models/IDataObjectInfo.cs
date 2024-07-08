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
    [Required(AllowEmptyStrings = true)] string DatabaseFileGroupName { get; }
    [Required(AllowEmptyStrings = true)] string CustomAddDatabaseCommandText { get; }
    [Required(AllowEmptyStrings = true)] string CustomUpdateDatabaseCommandText { get; }
    [Required(AllowEmptyStrings = true)] string CustomDeleteDatabaseCommandText { get; }

    [Required][ValidateObject] IReadOnlyCollection<PrimaryKeyConstraint> PrimaryKeyConstraints { get; }
    [Required][ValidateObject] IReadOnlyCollection<ForeignKeyConstraint> ForeignKeyConstraints { get; }
    [Required][ValidateObject] IReadOnlyCollection<DatabaseFramework.Domain.Index> Indexes { get; }
    [Required][ValidateObject] IReadOnlyCollection<CheckConstraint> CheckConstraints { get; }

    [Required][ValidateObject] IReadOnlyCollection<IEntityMapping> CustomEntityMappings { get; }
}
