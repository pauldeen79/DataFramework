using System.Linq;
using CrossCutting.Common.Extensions;
using CrossCutting.Data.Abstractions;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Builders;
using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassPropertyBuilderExtensions
    {
        internal static ClassPropertyBuilder Fill(this ClassPropertyBuilder instance, IFieldInfo field)
            => instance
                .WithTypeName(field.GetPropertyTypeName())
                .WithIsNullable(field.IsNullable)
                .WithVisibility(field.Metadata.GetValue(Entities.Visibility, () => field.IsVisible.ToVisibility()))
                .WithGetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, () => field.IsVisible.ToVisibility()))
                .WithSetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, () => field.IsVisible.ToVisibility()))
                .AddMetadata(field.Metadata.Convert().Select(x => new MetadataBuilder(x)));

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
}
