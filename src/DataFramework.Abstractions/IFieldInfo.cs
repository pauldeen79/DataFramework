namespace DataFramework.Abstractions;

public interface IFieldInfo
{
    string Name { get; }
    string? Description { get; }
    string? DisplayName { get; }
    string? TypeName { get; }
    bool IsNullable { get; }
    bool IsVisible { get; }
    bool IsReadOnly { get; }
    bool IsIdentityField { get; }
    bool IsComputed { get; }
    bool IsPersistable { get; }
    bool CanGet { get; }
    bool CanSet { get; }
    bool UseForConcurrencyCheck { get; }
    object? DefaultValue { get; }
    IReadOnlyCollection<IMetadata> Metadata { get; }
}
