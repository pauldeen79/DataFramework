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
        return type != null && TypeMappings.ReaderMethodNames.ContainsKey(type);
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
