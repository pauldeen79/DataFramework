using CrossCutting.Common.Extensions;
using CrossCutting.Data.Abstractions;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Extensions;

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
                .AddMetadata(field.Metadata.Convert());

        internal static ClassPropertyBuilder AddGetterLiteralCodeStatements(this ClassPropertyBuilder instance, params string[] statements)
            => instance.AddGetterCodeStatements(statements.ToLiteralCodeStatementBuilders());

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
