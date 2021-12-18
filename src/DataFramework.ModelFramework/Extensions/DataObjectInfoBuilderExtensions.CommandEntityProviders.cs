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
        public static DataObjectInfoBuilder WithCommandEntityProviderNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(CommandEntityProviders.Namespace, @namespace);

        public static DataObjectInfoBuilder AddCommandEntityProviderAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.Attribute).WithValue(x)));

        public static DataObjectInfoBuilder AddCommandEntityProviderAttributes(this DataObjectInfoBuilder instance, IEnumerable<IAttribute> attributes)
            => instance.AddCommandEntityProviderAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder AddCommandEntityProviderAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddCommandEntityProviderAttributes(this DataObjectInfoBuilder instance, IEnumerable<AttributeBuilder> attributes)
            => instance.AddCommandEntityProviderAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder WithCommandEntityProviderVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(CommandEntityProviders.Visibility, visibility);

        public static DataObjectInfoBuilder AddAddResultEntityStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.AddResultEntityStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddAddResultEntityStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddAddResultEntityStatements(codeStatements.ToArray());

        public static DataObjectInfoBuilder AddUpdateResultEntityStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.UpdateResultEntityStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddUpdateResultEntityStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddUpdateResultEntityStatements(codeStatements.ToArray());

        public static DataObjectInfoBuilder AddDeleteResultEntityStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.DeleteResultEntityStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddDeleteResultEntityStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddDeleteResultEntityStatements(codeStatements.ToArray());

        public static DataObjectInfoBuilder AddAddAfterReadStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.AddAfterReadStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddAddAfterReadStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddAddAfterReadStatements(codeStatements.ToArray());

        public static DataObjectInfoBuilder AddUpdateAfterReadStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.UpdateAfterReadStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddUpdateAfterReadStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddUpdateAfterReadStatements(codeStatements.ToArray());

        public static DataObjectInfoBuilder AddDeleteAfterReadStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.DeleteAfterReadStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddDeleteAfterReadStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddDeleteAfterReadStatements(codeStatements.ToArray());
    }
}
