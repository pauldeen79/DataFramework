using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassPropertyBuilderExtensions
    {
        internal static ClassPropertyBuilder Fill(this ClassPropertyBuilder instance, IFieldInfo field)
            => instance
                .WithTypeName(field.Metadata.GetStringValue(Entities.PropertyType, field.TypeName ?? "System.Object"))
                .WithIsNullable(field.IsNullable)
                .WithVisibility(field.Metadata.GetValue(Entities.Visibility, () => field.IsVisible.ToVisibility()))
                .WithGetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, () => field.IsVisible.ToVisibility()))
                .WithSetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, () => field.IsVisible.ToVisibility()))
                .AddMetadata(field.Metadata.Convert());
    }
}
