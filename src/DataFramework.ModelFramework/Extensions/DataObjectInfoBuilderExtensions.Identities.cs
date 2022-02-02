namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoBuilderExtensions
{
    public static DataObjectInfoBuilder WithEntityIdentityNamespace(this DataObjectInfoBuilder instance, string? @namespace)
        => instance.ReplaceMetadata(Identities.Namespace, @namespace);

    public static DataObjectInfoBuilder AddEntityIdentityAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
        => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Identities.Attribute).WithValue(x)));

    public static DataObjectInfoBuilder AddEntityIdentityAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
        => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Identities.Attribute).WithValue(x.Build())));
}
