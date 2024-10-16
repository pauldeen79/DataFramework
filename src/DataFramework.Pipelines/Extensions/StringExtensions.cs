namespace DataFramework.Pipelines.Extensions;

public static class StringExtensions
{
    private static readonly string[] _databaseStringTypes = ["CHAR", "NCHAR", "NVARCHAR", "VARCHAR"];

    public static bool IsDatabaseStringType(this string instance)
        => Array.Exists(_databaseStringTypes, (string x) => x.Equals(instance, StringComparison.OrdinalIgnoreCase));
}
