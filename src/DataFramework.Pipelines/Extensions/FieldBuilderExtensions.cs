namespace DataFramework.Pipelines.Extensions;

internal static class FieldBuilderExtensions
{
    internal static FieldBuilder FillFrom(this FieldBuilder instance, FieldInfo fieldInfo, CultureInfo cultureInfo)
        => instance.WithName($"_{fieldInfo.Name.ToPascalCase(cultureInfo)}")
            .WithTypeName(fieldInfo.PropertyTypeName.FixTypeName())
            .WithIsNullable(fieldInfo.IsNullable);
}
