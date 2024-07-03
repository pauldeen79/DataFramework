namespace DataFramework.Domain;

public partial class FieldInfo
{
    public const int DefaultStringLength = 32;

    public string CreatePropertyName(DataObjectInfo dataObjectInfo)
        => Name == dataObjectInfo.IsNotNull(nameof(dataObjectInfo)).Name
            ? $"{Name}Property".Sanitize()
            : Name.Sanitize();

    public string PropertyTypeName
        => TypeName.WhenNullOrEmpty(() => typeof(object).FullName!);

    public bool IsRequired
        => !IsNullable || IsIdentityField;

    /// <summary>
    /// Determines whether the specified field should be used on Insert in database
    /// </summary>
    /// <param name="instance"></param>
    /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
    public bool UseOnInsert
        => OverrideUseOnInsert ?? IsPersistable && !IsIdentityField && !IsDatabaseIdentityField && !IsComputed && PropertyTypeName.IsSupportedByMap();

    /// <summary>
    /// Determines whether the specified field should be used on Update in database
    /// </summary>
    /// <param name="instance"></param>
    /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
    public bool UseOnUpdate
        => OverrideUseOnUpdate ?? IsPersistable && !IsIdentityField && !IsDatabaseIdentityField && !IsComputed && PropertyTypeName.IsSupportedByMap();

    /// <summary>
    /// Determines whether the specified field should be used on Delete in database
    /// </summary>
    /// <param name="instance"></param>
    /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
    public bool UseOnDelete
        => OverrideUseOnDelete ?? IsPersistable && !IsIdentityField && !IsDatabaseIdentityField && !IsComputed && PropertyTypeName.IsSupportedByMap();

    /// <summary>
    /// Determines whether the specified field should always be used on Select in database
    /// </summary>
    /// <remarks>Metadata value overrides IsPersistable, both True and False</remarks>
    /// <param name="instance"></param>
    public bool UseOnSelect
        => OverrideUseOnSelect ?? IsPersistable && PropertyTypeName.IsSupportedByMap();

    public ParameterBuilder ToParameterBuilder(CultureInfo cultureInfo)
        => new ParameterBuilder().WithName(Name.Sanitize().ToPascalCase(cultureInfo))
                                 .WithTypeName(PropertyTypeName.FixTypeName())
                                 .WithDefaultValue(DefaultValue)
                                 .WithIsNullable(IsNullable);

    public bool IsSqlRequired
        => IsRequiredInDatabase ?? IsNullable || IsRequired;

    public string SqlReaderMethodName
    {
        get
        {
            if (!string.IsNullOrEmpty(DatabaseReaderMethodName))
            {
                return DatabaseReaderMethodName!;
            }

            return PropertyTypeName.GetSqlReaderMethodName(IsNullable);
        }
    }

    public string GetSqlFieldType(bool includeSpecificProperties = false)
    {
        if (DatabaseFieldType is not null && !string.IsNullOrEmpty(DatabaseFieldType))
        {
            return includeSpecificProperties
                ? DatabaseFieldType
                : RemoveSpecificPropertiesFromSqlType(DatabaseFieldType);
        }

        var typeName = PropertyTypeName;
        if (typeName == typeof(string).FullName || typeName == typeof(string).AssemblyQualifiedName)
        {
            return GetSqlVarcharType(includeSpecificProperties, DefaultStringLength);
        }

        if (typeName == typeof(decimal).FullName
            || typeName == typeof(decimal).AssemblyQualifiedName
            || typeName == typeof(decimal?).FullName
            || typeName == typeof(decimal?).AssemblyQualifiedName)
        {
            return GetSqlDecimalType(includeSpecificProperties);
        }

        if (typeName.IsRequiredEnum() || typeName.IsOptionalEnum())
        {
            return "int";
        }

        var type = Type.GetType(typeName);
        if (type != null && TypeMappings.SqlTypeNameMappings.TryGetValue(type, out var sqlType))
        {
            return sqlType;
        }

        return string.Empty;
    }

    public bool IsSqlStringMaxLength
        => IsMaxLengthString ?? false;

    private string GetSqlDecimalType(bool includeSpecificProperties)
        => includeSpecificProperties
            ? $"decimal({DatabaseNumericPrecision ?? 8},{DatabaseNumericScale ?? 0})"
            : "decimal";

    private string GetSqlVarcharType(bool includeSpecificProperties, int defaultLength)
    {
        if (!includeSpecificProperties)
        {
            return "varchar";
        }
        var length = IsSqlStringMaxLength
            ? "max"
            : GetSqlStringLength(defaultLength).ToString(CultureInfo.InvariantCulture);
        return $"varchar({length})";
    }

    public int GetSqlStringLength(int defaultLength)
        => StringMaxLength ?? defaultLength;

    private static string RemoveSpecificPropertiesFromSqlType(string sqlType)
        => sqlType.IndexOf("(") > -1
            ? sqlType.Substring(0, sqlType.IndexOf("("))
            : sqlType;
}
