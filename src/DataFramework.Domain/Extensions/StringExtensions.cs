namespace DataFramework.Domain.Extensions;

internal static class StringExtensions
{
    internal static string FixGenericParameter(this string value, string typeName)
        => value.Replace("<T>", string.Format("<{0}>", typeName));

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

    internal static string ReplaceProperties(this string sqlType, string abstractSqlType)
        => sqlType.StartsWith(abstractSqlType, StringComparison.OrdinalIgnoreCase) && !sqlType.Equals(abstractSqlType, StringComparison.OrdinalIgnoreCase)
            ? abstractSqlType
            : sqlType;

    private static Dictionary<Type, string> _readerMethodNames = new Dictionary<Type, string>
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

    private static readonly string[] _keywords = new string[77]
{
    "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
    "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum",
    "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto",
    "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace",
    "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public",
    "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string",
    "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked",
    "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"
};

    public static string ToPascalCase(this string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
#pragma warning disable CA1308 // Normalize strings to uppercase
            return $"{value.Substring(0, 1).ToLowerInvariant()}{value.Substring(1)}";
#pragma warning restore CA1308 // Normalize strings to uppercase
        }

        return value;
    }

    public static string SqlEncode(this string value)
    {
        return "'" + value.Replace("'", "''") + "'";
    }

    public static string FixTypeName(this string? instance)
    {
        if (instance == null)
        {
            return string.Empty;
        }

        while (true)
        {
            int num = instance.IndexOf(", ");
            if (num == -1)
            {
                break;
            }

            int num2 = instance.IndexOf("]", num + 1);
            if (num2 == -1)
            {
                break;
            }

            instance = instance.Substring(0, num) + instance.Substring(num2 + 1);
        }

        while (true)
        {
            int num = instance.IndexOf("`");
            if (num == -1)
            {
                break;
            }

            instance = instance.Substring(0, num) + instance.Substring(num + 2);
        }

        int num3 = instance.IndexOf(", ");
        if (num3 > -1)
        {
            instance = instance.Substring(0, num3);
        }

        return FixAnonymousTypeName(instance.Replace("[[", "<").Replace(",[", ",").Replace(",]", ">")
            .Replace("]", ">")
            .Replace("[>", "[]")
            .Replace("System.Void", "void")
            .Replace("+", ".")
            .Replace("&", ""));
    }

    public static string GetCsharpFriendlyTypeName(this string instance)
    {
        return instance switch
        {
            "System.Char" => "char",
            "System.String" => "string",
            "System.Boolean" => "bool",
            "System.Object" => "object",
            "System.Decimal" => "decimal",
            "System.Double" => "double",
            "System.Single" => "float",
            "System.Byte" => "byte",
            "System.SByte" => "sbyte",
            "System.Int16" => "short",
            "System.UInt16" => "ushort",
            "System.Int32" => "int",
            "System.UInt32" => "uint",
            "System.Int64" => "long",
            "System.UInt64" => "ulong",
            _ => instance.ReplaceGenericArgument("System.Char", "char").ReplaceGenericArgument("System.String", "string").ReplaceGenericArgument("System.Boolean", "bool")
                .ReplaceGenericArgument("System.Object", "object")
                .ReplaceGenericArgument("System.Decimal", "decimal")
                .ReplaceGenericArgument("System.Double", "double")
                .ReplaceGenericArgument("System.Single", "float")
                .ReplaceGenericArgument("System.Byte", "byte")
                .ReplaceGenericArgument("System.SByte", "sbyte")
                .ReplaceGenericArgument("System.Int16", "short")
                .ReplaceGenericArgument("System.UInt16", "ushort")
                .ReplaceGenericArgument("System.Int32", "int")
                .ReplaceGenericArgument("System.UInt32", "uint")
                .ReplaceGenericArgument("System.Int64", "long")
                .ReplaceGenericArgument("System.UInt64", "ulong"),
        };
    }

    private static string ReplaceGenericArgument(this string instance, string find, string replace)
    {
        return instance.Replace("<" + find, "<" + replace).Replace(find + ">", replace + ">").Replace("," + find, "," + replace)
            .Replace(", " + find, ", " + replace)
            .Replace(find + "[]", replace + "[]");
    }

    public static string GetCsharpFriendlyName(this string instance)
    {
        if (!_keywords.Contains<string>(instance))
        {
            return instance;
        }

        return "@" + instance;
    }

    public static string GetGenericArguments(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        int num = value.IndexOf("<");
        if (num == -1)
        {
            return string.Empty;
        }

        int num2 = value.LastIndexOf(",");
        if (num2 == -1)
        {
            num2 = value.LastIndexOf(">");
        }

        if (num2 == -1)
        {
            return string.Empty;
        }

        return value.Substring(num + 1, num2 - num - 1);
    }

    public static string Sanitize(this string token)
    {
        token = Regex.Replace(token, "[\\W\\b]", "_", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(200.0));
        token = Regex.Replace(token, "^\\d", "_$0", RegexOptions.None, TimeSpan.FromMilliseconds(200.0));
        return token;
    }

    public static bool IsRequiredEnum(this string instance)
    {
        if (!string.IsNullOrEmpty(instance))
        {
            return Type.GetType(instance)?.IsEnum ?? false;
        }

        return false;
    }

    public static bool IsOptionalEnum(this string instance)
    {
        if (string.IsNullOrEmpty(instance))
        {
            return false;
        }

        Type type = Type.GetType(instance);
        if (type == null)
        {
            return false;
        }

        return Nullable.GetUnderlyingType(type)?.IsEnum ?? false;
    }

    public static string GetClassName(this string fullyQualifiedClassName)
    {
        int num = fullyQualifiedClassName.LastIndexOf(".");
        if (num != -1)
        {
            return fullyQualifiedClassName.Substring(num + 1);
        }

        return fullyQualifiedClassName;
    }

    public static string GetNamespaceWithDefault(this string? fullyQualifiedClassName, string defaultValue = "")
    {
        if (fullyQualifiedClassName == null || string.IsNullOrEmpty(fullyQualifiedClassName))
        {
            return defaultValue;
        }

        int num = fullyQualifiedClassName.LastIndexOf(".");
        if (num != -1)
        {
            return fullyQualifiedClassName.Substring(0, num);
        }

        return defaultValue;
    }

    public static string MakeGenericTypeName(this string instance, string genericTypeParameter)
    {
        if (!string.IsNullOrEmpty(genericTypeParameter))
        {
            return instance + "<" + genericTypeParameter + ">";
        }

        return instance;
    }

    public static string ReplaceSuffix(this string instance, string find, string replace, StringComparison stringComparison)
    {
        int num = instance.IndexOf(find, stringComparison);
        if (num == -1 || num < instance.Length - find.Length)
        {
            return instance;
        }

        return instance.Substring(0, instance.Length - find.Length) + replace;
    }

    public static bool IsStringTypeName(this string? instance)
    {
        return instance.FixTypeName() == typeof(string).FullName;
    }

    public static bool IsBooleanTypeName(this string? instance)
    {
        return instance.FixTypeName() == typeof(bool).FullName;
    }

    public static bool IsNullableBooleanTypeName(this string? instance)
    {
        return instance.FixTypeName() == typeof(bool?).FullName.FixTypeName();
    }

    public static bool IsObjectTypeName(this string? instance)
    {
        return instance.FixTypeName() == typeof(object).FullName;
    }

    private static string FixAnonymousTypeName(string instance)
    {
        bool num = instance.Contains("AnonymousType") && (instance.Contains("<>") || instance.Contains("VB$"));
        string text = instance.EndsWith("[]") ? "[]" : string.Empty;
        if (!num)
        {
            return instance;
        }

        return "AnonymousType" + text;
    }
}
