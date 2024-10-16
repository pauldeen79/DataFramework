namespace DataFramework.Domain.Extensions;

public static class StringExtensions
{
    public static bool IsSupportedByMap(this string instance)
    {
        if (string.IsNullOrEmpty(instance))
        {
            return false;

        }
        if (instance.IsRequiredEnum() || instance.IsOptionalEnum())
        {
            return true;
        }

        var type = Type.GetType(instance);
        return type is not null && TypeMappings.ReaderMethodNames.ContainsKey(type);
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
        if (type is null)
        {
            return "GetValue";
        }

        if (!TypeMappings.ReaderMethodNames.TryGetValue(type, out var name))
        {
            return "GetValue";
        }

        if (isNullable && !name.Contains("Nullable"))
        {
            return name.Replace("Get", "GetNullable");
        }

        return name;
    }
}
