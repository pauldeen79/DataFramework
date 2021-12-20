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
        public static DataObjectInfoBuilder WithEntityMapperNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(EntityMappers.Namespace, @namespace);

        public static DataObjectInfoBuilder AddEntityMapperAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(EntityMappers.Attribute).WithValue(x)));

        public static DataObjectInfoBuilder AddEntityMapperAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(EntityMappers.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder WithEntityMapperVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(EntityMappers.Visibility, visibility);

        public static DataObjectInfoBuilder AddEntityMapperCustomMappings(this DataObjectInfoBuilder instance, params KeyValuePair<string, object>[] fields)
            => instance.AddMetadata(fields.Select(x => new MetadataBuilder().WithName(EntityMappers.CustomMapping).WithValue(x)));
    }
}
