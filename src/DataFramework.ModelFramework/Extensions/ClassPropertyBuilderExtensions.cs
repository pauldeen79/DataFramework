using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    public static class ClassPropertyBuilderExtensions
    {
        public static ClassPropertyBuilder WithSharedFieldInfoData(this ClassPropertyBuilder instance, IFieldInfo field)
            => instance
                .WithTypeName(field.Metadata.GetMetadataStringValue(Entities.PropertyType, field.TypeName ?? string.Empty))
                .WithStatic(field.Metadata.GetMetadataStringValue(Entities.Static).IsTrue())
                .WithVirtual(field.Metadata.GetMetadataStringValue(Entities.Virtual).IsTrue())
                .WithAbstract(field.Metadata.GetMetadataStringValue(Entities.Abstract).IsTrue())
                .WithProtected(field.Metadata.GetMetadataStringValue(Entities.Protected).IsTrue())
                .WithOverride(field.Metadata.GetMetadataStringValue(Entities.Override).IsTrue())
                .WithIsNullable(field.IsNullable)
                .WithVisibility(field.Metadata.GetMetadataValue(Entities.Visibility, field.IsVisible.ToVisibility()))
                .WithGetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, field.IsVisible.ToVisibility()))
                .WithSetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, field.IsVisible.ToVisibility()))
                .AddMetadata(field.Metadata.Convert());
    }
}
