namespace DataFramework.Abstractions;

public interface IDataObjectInfo
{
    IReadOnlyCollection<IFieldInfo> Fields { get; }
    string? AssemblyName { get; }
    string? TypeName { get; }
    string Name { get; }
    string? Description { get; }
    string? DisplayName { get; }
    bool IsVisible { get; }
    bool IsReadOnly { get; }
    bool IsQueryable { get; }
    IReadOnlyCollection<IMetadata> Metadata { get; }
}
