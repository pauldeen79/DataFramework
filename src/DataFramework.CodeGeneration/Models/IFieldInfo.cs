namespace DataFramework.CodeGeneration.Models;

public interface IFieldInfo
{
    [Required] string Name { get; }
    string? Description { get; }
    string? DisplayName { get; }
    string? TypeName { get; }
    bool IsNullable { get; }
    bool IsValueType { get; }
    [DefaultValue(true)] bool IsVisible { get; }
    [DefaultValue(true)] bool IsPersistable { get; }
    [DefaultValue(true)] bool CanGet { get; }
    [DefaultValue(true)] bool CanSet { get; }
    bool IsReadOnly { get; }
    bool IsIdentityField { get; }
    bool IsComputed { get; }
    bool IsRowVersion { get; }
    bool UseForConcurrencyCheck { get; }
    object? DefaultValue { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> GetterCodeStatements { get; }

    int? StringMaxLength { get; }
    bool? IsMaxLengthString { get; }
    string? DatabaseStringCollation { get; }
    byte? DatabaseNumericPrecision { get; }
    byte? DatabaseNumericScale { get; }
    bool SkipFieldOnFind { get; }
    string? DatabaseFieldName { get; }
    string? DatabaseFieldType { get; }
    string? DatabaseReaderMethodName { get; }
    string? DatabaseCheckConstraintExpression { get; }
    bool? OverrideUseOnInsert { get; }
    bool? OverrideUseOnUpdate { get; }
    bool? OverrideUseOnDelete { get; }
    bool? OverrideUseOnSelect { get; }
    bool? IsRequiredInDatabase { get; }
    bool IsDatabaseIdentityField { get; }
}
