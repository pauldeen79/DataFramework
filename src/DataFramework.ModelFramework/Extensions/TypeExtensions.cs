namespace DataFramework.ModelFramework.Extensions;

public static class TypeExtensions
{
    public static string CreateGenericTypeName(this Type instance, string genericTypeConstraintName)
        => instance.MakeGenericType(typeof(object)).FullName
            .FixTypeName()
            .Replace("<System.Object>", $"<{genericTypeConstraintName}>");

    public static string CreateGenericTypeName(this Type instance, string genericTypeConstraintName1, string genericTypeConstraintName2)
        => instance.MakeGenericType(typeof(object), typeof(string)).FullName
            .FixTypeName()
            .Replace("<System.Object", $"<{genericTypeConstraintName1}")
            .Replace("System.String>", $"{genericTypeConstraintName2}>");
}
