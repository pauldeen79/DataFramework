namespace DataFramework.Domain;

internal static class TypeMappings
{
    internal static readonly Dictionary<Type, string> ReaderMethodNames = new Dictionary<Type, string>
    {
        { typeof(string), "GetString" },
        { typeof(int), "GetInt32" },
        { typeof(int?), "GetNullableInt32" },
        { typeof(short), "GetInt16" },
        { typeof(short?), "GetNullableInt16" },
        { typeof(long), "GetInt64" },
        { typeof(long?), "GetNullableInt64" },
        { typeof(byte), "GetByte" },
        { typeof(byte?), "GetNullableByte" },
        { typeof(bool), "GetBoolean" },
        { typeof(bool?), "GetNullableBoolean" },
        { typeof(Guid), "GetGuid" },
        { typeof(Guid?), "GetNullableGuid" },
        { typeof(double), "GetDouble" },
        { typeof(double?), "GetNullableDouble" },
        { typeof(float), "GetFloat" },
        { typeof(float?), "GetNullableFloat" },
        { typeof(decimal), "GetDecimal" },
        { typeof(decimal?), "GetNullableDecimal" },
        { typeof(DateTime), "GetDateTime" },
        { typeof(DateTime?), "GetNullableDateTime" },
        { typeof(byte[]), "GetByteArray" },
    };

    internal static readonly Dictionary<Type, string> SqlTypeNameMappings = new Dictionary<Type, string>
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
