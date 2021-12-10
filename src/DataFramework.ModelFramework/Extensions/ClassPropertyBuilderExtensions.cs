using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassPropertyBuilderExtensions
    {
        internal static ClassPropertyBuilder Fill(this ClassPropertyBuilder instance, IFieldInfo field)
            => instance
                .WithTypeName(field.Metadata.GetStringValue(Entities.PropertyType, field.TypeName ?? string.Empty))
                .WithStatic(field.Metadata.GetStringValue(Entities.Static).IsTrue())
                .WithVirtual(field.Metadata.GetStringValue(Entities.Virtual).IsTrue())
                .WithAbstract(field.Metadata.GetStringValue(Entities.Abstract).IsTrue())
                .WithProtected(field.Metadata.GetStringValue(Entities.Protected).IsTrue())
                .WithOverride(field.Metadata.GetStringValue(Entities.Override).IsTrue())
                .WithIsNullable(field.IsNullable)
                .WithVisibility(field.Metadata.GetValue(Entities.Visibility, () => field.IsVisible.ToVisibility()))
                .WithGetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, () => field.IsVisible.ToVisibility()))
                .WithSetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, () => field.IsVisible.ToVisibility()))
                .AddMetadata(field.Metadata.Convert());
    }
}
