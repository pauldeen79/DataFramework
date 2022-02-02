namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoBuilderExtensions
{
    public static DataObjectInfoBuilder WithPreventAdd(this DataObjectInfoBuilder instance, bool? preventAdd = true)
        => instance.ReplaceMetadata(CommandProviders.PreventAdd, preventAdd);

    public static DataObjectInfoBuilder WithPreventUpdate(this DataObjectInfoBuilder instance, bool? preventUpdate = true)
        => instance.ReplaceMetadata(CommandProviders.PreventUpdate, preventUpdate);

    public static DataObjectInfoBuilder WithPreventDelete(this DataObjectInfoBuilder instance, bool? preventDelete = true)
        => instance.ReplaceMetadata(CommandProviders.PreventDelete, preventDelete);

    public static DataObjectInfoBuilder WithCommandProviderNamespace(this DataObjectInfoBuilder instance, string? @namespace)
        => instance.ReplaceMetadata(CommandProviders.Namespace, @namespace);

    public static DataObjectInfoBuilder AddCommandProviderAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
        => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(CommandProviders.Attribute).WithValue(x.Build())));

    public static DataObjectInfoBuilder AddCommandProviderAttributes(this DataObjectInfoBuilder instance, IEnumerable<AttributeBuilder> attributes)
        => instance.AddCommandProviderAttributes(attributes.ToArray());

    public static DataObjectInfoBuilder WithCommandProviderVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
        => instance.ReplaceMetadata(CommandProviders.Visibility, visibility);
}
