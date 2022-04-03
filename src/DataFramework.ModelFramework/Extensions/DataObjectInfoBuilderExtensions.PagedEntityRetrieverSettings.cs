namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoBuilderExtensions
{
    public static DataObjectInfoBuilder WithPagedEntityRetrieverSettingsNamespace(this DataObjectInfoBuilder instance, string? @namespace)
        => instance.ReplaceMetadata(PagedEntityRetrieverSettings.Namespace, @namespace);

    public static DataObjectInfoBuilder AddPagedEntityRetrieverSettingsAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
        => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(PagedEntityRetrieverSettings.Attribute).WithValue(x)));

    public static DataObjectInfoBuilder AddPagedEntityRetrieverSettingsAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
        => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(PagedEntityRetrieverSettings.Attribute).WithValue(x.Build())));

    public static DataObjectInfoBuilder WithPagedEntityRetrieverSettingsVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
        => instance.ReplaceMetadata(PagedEntityRetrieverSettings.Visibility, visibility);

    public static DataObjectInfoBuilder WithTableName(this DataObjectInfoBuilder instance, string? tableName)
        => instance.ReplaceMetadata(PagedEntityRetrieverSettings.TableName, tableName);

    public static DataObjectInfoBuilder AddPagedEntityRetrieverSettingsFields(this DataObjectInfoBuilder instance, params string[] fields)
        => instance.AddMetadata(fields.Select(x => new MetadataBuilder().WithName(PagedEntityRetrieverSettings.Field).WithValue(x)));

    public static DataObjectInfoBuilder WithDefaultOrderByFields(this DataObjectInfoBuilder instance, string? orderByFields)
        => instance.ReplaceMetadata(PagedEntityRetrieverSettings.DefaultOrderByFields, orderByFields);

    public static DataObjectInfoBuilder WithDefaultWhereClause(this DataObjectInfoBuilder instance, string? whereClause)
        => instance.ReplaceMetadata(PagedEntityRetrieverSettings.DefaultWhereClause, whereClause);

    public static DataObjectInfoBuilder WithOverridePageSize(this DataObjectInfoBuilder instance, int? overridePageSize)
        => instance.ReplaceMetadata(PagedEntityRetrieverSettings.OverridePageSize, overridePageSize);
}
