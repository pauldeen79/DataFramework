using System.Linq;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithRepositoryNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Repositories.Namespace, @namespace);

        public static DataObjectInfoBuilder WithRepositoryInterfaceNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Repositories.InterfaceNamespace, @namespace);

        public static DataObjectInfoBuilder AddRepositoryAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Repositories.Attribute).WithValue(x)));

        public static DataObjectInfoBuilder AddRepositoryAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Repositories.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddRepositoryInterfaces(this DataObjectInfoBuilder instance, params string[] interfaces)
            => instance.AddMetadata(interfaces.Select(x => new MetadataBuilder().WithName(Repositories.Interfaces).WithValue(x)));

        public static DataObjectInfoBuilder WithRepositoryVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(Repositories.Visibility, visibility);

        public static DataObjectInfoBuilder AddRepositoryMethods(this DataObjectInfoBuilder instance, params IClassMethod[] methods)
            => instance.AddMetadata(methods.Select(x => new MetadataBuilder().WithName(Repositories.Method).WithValue(x)));

        public static DataObjectInfoBuilder AddRepositoryMethods(this DataObjectInfoBuilder instance, params ClassMethodBuilder[] methods)
            => instance.AddMetadata(methods.Select(x => new MetadataBuilder().WithName(Repositories.Method).WithValue(x.Build())));
    }
}
