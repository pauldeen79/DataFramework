using System.Linq;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithEntityRetrieverSettingsNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(EntityRetrieverSettings.Namespace, @namespace);

        public static DataObjectInfoBuilder AddEntityRetrieverSettingsAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(EntityRetrieverSettings.Attribute).WithValue(x)));

        public static DataObjectInfoBuilder AddEntityRetrieverSettingsAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(EntityRetrieverSettings.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder WithEntityRetrieverSettingsVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(EntityRetrieverSettings.Visibility, visibility);

        public static DataObjectInfoBuilder WithTableName(this DataObjectInfoBuilder instance, string? tableName)
            => instance.ReplaceMetadata(EntityRetrieverSettings.TableName, tableName);

        public static DataObjectInfoBuilder AddEntityRetrieverSettingsFields(this DataObjectInfoBuilder instance, params string[] fields)
            => instance.AddMetadata(fields.Select(x => new MetadataBuilder().WithName(EntityRetrieverSettings.Field).WithValue(x)));

        public static DataObjectInfoBuilder WithDefaultOrderByFields(this DataObjectInfoBuilder instance, string? orderByFields)
            => instance.ReplaceMetadata(EntityRetrieverSettings.DefaultOrderByFields, orderByFields);

        public static DataObjectInfoBuilder WithDefaultWhereClause(this DataObjectInfoBuilder instance, string? whereClause)
            => instance.ReplaceMetadata(EntityRetrieverSettings.DefaultWhereClause, whereClause);

        public static DataObjectInfoBuilder WithOverridePageSize(this DataObjectInfoBuilder instance, int? overridePageSize)
            => instance.ReplaceMetadata(EntityRetrieverSettings.OverridePageSize, overridePageSize);
    }
}
