namespace DataFramework.Pipelines.Extensions;

internal static class PropertyBuilderExtensions
{
    internal static PropertyBuilder Fill(this PropertyBuilder instance, FieldInfo field)
        => instance
            .WithTypeName(field.PropertyTypeName.FixTypeName())
            .WithIsNullable(field.IsNullable)
            .WithVisibility(field.IsVisible.ToVisibility())
            .WithGetterVisibility(SubVisibility.InheritFromParent)
            .WithSetterVisibility(SubVisibility.InheritFromParent);
}
