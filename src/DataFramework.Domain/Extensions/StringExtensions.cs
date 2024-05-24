namespace DataFramework.Domain.Extensions;

internal static class StringExtensions
{
    internal static bool IsSupportedByMap(this string instance)
    {
        if (instance.IsRequiredEnum() || instance.IsOptionalEnum())
        {
            return true;
        }

        var type = Type.GetType(instance);
        return type != null && _readerMethodNames.ContainsKey(type);
    }

    internal static string GetSqlReaderMethodName(this string instance, bool isNullable)
    {
        if (instance.IsRequiredEnum() && !isNullable)
        {
            // assumption: enum is int32.
            return "GetInt32";
        }

        if (instance.IsOptionalEnum() || (instance.IsRequiredEnum() && isNullable))
        {
            // assumption: enum is int32.
            return "GetNullableInt32";
        }

        var type = Type.GetType(instance);
        if (type == null)
        {
            return "GetValue";
        }
        if (!_readerMethodNames.TryGetValue(type, out var name))
        {
            return "GetValue";
        }

        if (isNullable && !name.Contains("Nullable"))
        {
            return name.Replace("Get", "GetNullable");
        }

        return name;
    }

    private static readonly Dictionary<Type, string> _readerMethodNames = new Dictionary<Type, string>
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
}
