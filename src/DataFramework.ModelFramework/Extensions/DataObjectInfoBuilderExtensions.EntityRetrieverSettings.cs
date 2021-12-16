using System.Collections.Generic;
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

        public static DataObjectInfoBuilder AddEntityRetrieverSettingsAttributes(this DataObjectInfoBuilder instance, IEnumerable<IAttribute> attributes)
            => instance.AddEntityRetrieverSettingsAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder AddEntityRetrieverSettingsAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(EntityRetrieverSettings.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddEntityRetrieverSettingsAttributes(this DataObjectInfoBuilder instance, IEnumerable<AttributeBuilder> attributes)
            => instance.AddEntityRetrieverSettingsAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder WithEntityRetrieverSettingsVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(EntityRetrieverSettings.Visibility, visibility);
    }
}
