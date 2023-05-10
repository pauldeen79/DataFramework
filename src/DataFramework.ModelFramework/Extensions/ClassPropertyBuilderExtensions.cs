namespace DataFramework.ModelFramework.Extensions;

internal static class ClassPropertyBuilderExtensions
{
    internal static ClassPropertyBuilder Fill(this ClassPropertyBuilder instance, IFieldInfo field)
        => instance
            .WithTypeName(field.GetPropertyTypeName().FixTypeName())
            .WithIsNullable(field.IsNullable)
            .WithVisibility(field.Metadata.GetValue(Entities.Visibility, () => field.IsVisible.ToVisibility()))
            .WithGetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterModifiers, () => field.IsVisible.ToVisibility()))
            .WithSetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertySetterModifiers, () => field.IsVisible.ToVisibility()))
            .AddMetadata(field.Metadata.Convert().Select(x => new global::ModelFramework.Common.Builders.MetadataBuilder(x)));

    public static ClassPropertyBuilder AddEntityCommandProviderMethod(this ClassPropertyBuilder instance,
                                                                      IDataObjectInfo dataObjectInfo,
                                                                      string preventMetadataName,
                                                                      DatabaseOperation operation,
                                                                      string methodSuffix)
        => instance.Chain(() =>
        {
            if (!dataObjectInfo.Metadata.GetBooleanValue(preventMetadataName))
            {
                instance.AddGetterLiteralCodeStatements
                (
                    $"        case {typeof(DatabaseOperation).FullName}.{operation}:",
                    $"            return {operation.GetMethodNamePrefix()}{methodSuffix}(entity);"
                );
            }
        });
}
