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
}
