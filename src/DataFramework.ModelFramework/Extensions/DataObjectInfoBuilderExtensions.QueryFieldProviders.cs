using System.Linq;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithQueryFieldProviderNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(QueryFieldProviders.Namespace, @namespace);

        public static DataObjectInfoBuilder AddQueryFieldProviderAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.Attribute).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryFieldProviderAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder WithQueryFieldProviderVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(QueryFieldProviders.Visibility, visibility);
    }
}
