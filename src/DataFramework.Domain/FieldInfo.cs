﻿namespace DataFramework.Domain;

public partial class FieldInfo
{
    public const int DefaultStringLength = 32;

    public string CreatePropertyName(DataObjectInfo dataObjectInfo)
        => Name == dataObjectInfo.Name
            ? string.Format("{0}Property", Name).Sanitize()
            : Name.Sanitize();

    public string PropertyTypeName
        => TypeName ?? "System.Object";

    public bool IsRequired()
        => !IsNullable || IsIdentityField;

    /// <summary>
    /// Determines whether the specified field should be used on Insert in database
    /// </summary>
    /// <param name="instance"></param>
    /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
    public bool UseOnInsert
        => OverrideUseOnInsert ?? IsPersistable && !IsIdentityField && !IsIdentityFieldInDatabase && !IsComputed && PropertyTypeName.IsSupportedByMap();

    /// <summary>
    /// Determines whether the specified field should be used on Update in database
    /// </summary>
    /// <param name="instance"></param>
    /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
    public bool UseOnUpdate
        => OverrideUseOnUpdate ?? IsPersistable && !IsIdentityField && !IsIdentityFieldInDatabase && !IsComputed && PropertyTypeName.IsSupportedByMap();

    /// <summary>
    /// Determines whether the specified field should be used on Delete in database
    /// </summary>
    /// <param name="instance"></param>
    /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
    public bool UseOnDelete
        => OverrideUseOnDelete ?? IsPersistable && !IsIdentityField && !IsIdentityFieldInDatabase && !IsComputed && PropertyTypeName.IsSupportedByMap();

    /// <summary>
    /// Determines whether the specified field should always be used on Select in database
    /// </summary>
    /// <remarks>Metadata value overrides IsPersistable, both True and False</remarks>
    /// <param name="instance"></param>
    public bool UseOnSelect
        => OverrideUseOnSelect ?? IsPersistable && PropertyTypeName.IsSupportedByMap();

    //internal static ParameterBuilder ToParameterBuilder()
    //    => new ParameterBuilder().WithName(instance.Name.Sanitize().ToPascalCase())
    //                             .WithTypeName(instance.PropertyTypeName.FixTypeName())
    //                             .WithDefaultValue(instance.DefaultValue)
    //                             .WithIsNullable(instance.IsNullable);

    internal bool GetIsSqlRequired()
        => IsRequiredInDatabase ?? IsNullable || IsRequired();

    //internal static string GetSqlReaderMethodName()
    //{
    //    var metadataValue = instance.Metadata.GetStringValue(Database.SqlReaderMethodName);
    //    if (!string.IsNullOrEmpty(metadataValue))
    //    {
    //        return metadataValue;
    //    }

    //    var typeName = instance.PropertyTypeName;
    //    if (typeName.Length == 0)
    //    {
    //        //assume object
    //        return "GetValue";
    //    }

    //    return typeName.GetSqlReaderMethodName(instance.IsNullable);
    //}

    internal string GetSqlFieldType(bool includeSpecificProperties = false)
    {
        if (DatabaseFieldType is not null && !string.IsNullOrEmpty(DatabaseFieldType))
        {
            return includeSpecificProperties
                ? DatabaseFieldType
                : RemoveSpecificPropertiesFromSqlType(DatabaseFieldType);
        }

        var typeName = PropertyTypeName;
        if (string.IsNullOrEmpty(typeName))
        {
            return string.Empty;
        }

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
        if (type != null && _sqlTypeNameMappings.TryGetValue(type, out var sqlType))
        {
            return sqlType;
        }

        return string.Empty;
    }

    internal bool IsSqlStringMaxLength()
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
        var length = IsSqlStringMaxLength()
            ? "max"
            : GetSqlStringLength(defaultLength).ToString(CultureInfo.InvariantCulture);
        return $"varchar({length})";
    }

    internal int GetSqlStringLength(int defaultLength)
        => StringMaxLength ?? defaultLength;

    private static string RemoveSpecificPropertiesFromSqlType(string sqlType)
        => sqlType.IndexOf("(") > -1
            ? sqlType.Substring(0, sqlType.IndexOf("("))
            : sqlType;

    //private static int? AttributeParameterFirstValue(IAttribute attribute)
    //{
    //    if (attribute == null)
    //    {
    //        return null;
    //    }

    //    if (attribute.Parameters.Count == 0)
    //    {
    //        return null;
    //    }

    //    if (attribute.Parameters.First().Value is int i)
    //    {
    //        return i;
    //    }

    //    return null;
    //}

    private static Dictionary<Type, string> _sqlTypeNameMappings = new Dictionary<Type, string>
    {
        { typeof(int), "int" },
        { typeof(int?), "int" },
        { typeof(short), "smallint" },
        { typeof(short?), "smallint" },
        { typeof(long), "bigint" },
        { typeof(long?), "bigint" },
        { typeof(byte), "tinyint" },
        { typeof(byte?), "tinyint" },
        { typeof(bool), "bit" },
        { typeof(bool?), "bit" },
        { typeof(Guid), "uniqueidentifier" },
        { typeof(Guid?), "uniqueidentifier" },
        { typeof(double), "float" },
        { typeof(double?), "float" },
        { typeof(float), "real" },
        { typeof(float?), "real" },
        { typeof(DateTime), "datetime" },
        { typeof(DateTime?), "datetime" },
        { typeof(byte[]), "varbinary" },
    };
}