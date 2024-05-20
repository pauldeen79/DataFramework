namespace DataFramework.CodeGeneration.Models;

public interface IFieldInfo
{
    [Required] string Name { get; }
    string? Description { get; }
    string? DisplayName { get; }
    string? TypeName { get; }
    bool IsNullable { get; }
    [DefaultValue(true)] bool IsVisible { get; }
    [DefaultValue(true)] bool IsPersistable { get; }
    [DefaultValue(true)] bool CanGet { get; }
    [DefaultValue(true)] bool CanSet { get; }
    bool IsReadOnly { get; }
    bool IsIdentityField { get; }
    bool IsComputed { get; }
    bool UseForConcurrencyCheck { get; }
    object? DefaultValue { get; }
}
