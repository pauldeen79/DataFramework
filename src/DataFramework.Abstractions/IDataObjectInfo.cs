using CrossCutting.Common;

namespace DataFramework.Abstractions
{
    public interface IDataObjectInfo
    {
        ValueCollection<IFieldInfo> Fields { get; }
        string? AssemblyName { get; }
        string? TypeName { get; }
        string Name { get; }
        string? Description { get; }
        string? DisplayName { get; }
        bool IsVisible { get; }
        bool IsReadOnly { get; }
        bool IsQueryable { get; }
        ValueCollection<IMetadata> Metadata { get; }
    }
}
