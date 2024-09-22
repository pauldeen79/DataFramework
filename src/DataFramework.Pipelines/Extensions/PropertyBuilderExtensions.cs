namespace DataFramework.Pipelines.Extensions;

internal static class PropertyBuilderExtensions
{
    internal static PropertyBuilder Fill(this PropertyBuilder instance, FieldInfo field)
        => instance
            .WithTypeName(field.PropertyTypeName.FixTypeName())
            .WithIsNullable(field.IsNullable)
            .WithIsValueType(field.IsValueType)
            .WithVisibility(field.IsVisible.ToVisibility())
            .WithGetterVisibility(SubVisibility.InheritFromParent)
            .WithSetterVisibility(SubVisibility.InheritFromParent);

    internal static PropertyBuilder AddEntityCommandProviderMethod(
        this PropertyBuilder instance,
        DataObjectInfo dataObjectInfo,
        bool enabled,
        DatabaseOperation operation,
        string methodSuffix)
        => instance.Chain(() =>
        {
            if (enabled)
            {
                instance.AddGetterStringCodeStatements
                (
                    $"        case {typeof(DatabaseOperation).FullName}.{operation}:",
                    $"            return {operation.GetMethodNamePrefix()}{methodSuffix}(entity);"
                );
            }
        });
}
