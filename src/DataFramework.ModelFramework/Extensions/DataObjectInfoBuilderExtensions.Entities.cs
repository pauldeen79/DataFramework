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
        public static DataObjectInfoBuilder WithEntityClassType(this DataObjectInfoBuilder instance, EntityClassType entityClassType)
            => instance.ReplaceMetadata(Entities.EntityClassType, entityClassType);

        public static DataObjectInfoBuilder WithEntityNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Entities.Namespace, @namespace);

        public static DataObjectInfoBuilder AddEntityInterfaces(this DataObjectInfoBuilder instance, params string[] interfaces)
            => instance.AddMetadata(interfaces.Select(x => new MetadataBuilder().WithName(Entities.Interfaces).WithValue(x)));

        public static DataObjectInfoBuilder WithEntityVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(Entities.Visibility, visibility);

        public static DataObjectInfoBuilder AddEntityAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Entities.ClassAttribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder WithPropertyNameDeconflictionFormatString(this DataObjectInfoBuilder instance, string? propertyNameDeconflictionFormatString)
            => instance.ReplaceMetadata(Entities.PropertyNameDeconflictionFormatString, propertyNameDeconflictionFormatString);
    }
}
