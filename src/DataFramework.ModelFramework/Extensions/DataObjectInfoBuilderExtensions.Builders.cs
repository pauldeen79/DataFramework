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
        public static DataObjectInfoBuilder WithEntityBuilderNamespace(this DataObjectInfoBuilder instance, string? @namespace)
        => instance.ReplaceMetadata(Builders.Namespace, @namespace);

        public static DataObjectInfoBuilder AddEntityBuilderAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
        => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Builders.Attribute).WithValue(x)));

        public static DataObjectInfoBuilder AddEntityBuilderAttributes(this DataObjectInfoBuilder instance, IEnumerable<IAttribute> attributes)
            => instance.AddEntityBuilderAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder AddEntityBuilderAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Builders.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddEntityBuilderAttributes(this DataObjectInfoBuilder instance, IEnumerable<AttributeBuilder> attributes)
            => instance.AddEntityBuilderAttributes(attributes.ToArray());
    }
}
