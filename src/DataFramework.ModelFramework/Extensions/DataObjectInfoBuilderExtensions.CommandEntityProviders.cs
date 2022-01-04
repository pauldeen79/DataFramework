using System.Linq;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Contracts;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.CodeStatements;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithCommandEntityProviderNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(CommandEntityProviders.Namespace, @namespace);

        public static DataObjectInfoBuilder AddCommandEntityProviderAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder WithCommandEntityProviderVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(CommandEntityProviders.Visibility, visibility);

        public static DataObjectInfoBuilder AddAddResultEntityStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.AddResultEntityStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddAddResultEntityStatements(this DataObjectInfoBuilder instance, params string[] statements)
            => instance.AddAddResultEntityStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<IMetadata>())).ToArray());

        public static DataObjectInfoBuilder AddUpdateResultEntityStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.UpdateResultEntityStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddUpdateResultEntityStatements(this DataObjectInfoBuilder instance, params string[] statements)
            => instance.AddUpdateResultEntityStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<IMetadata>())).ToArray());

        public static DataObjectInfoBuilder AddDeleteResultEntityStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.DeleteResultEntityStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddDeleteResultEntityStatements(this DataObjectInfoBuilder instance, params string[] statements)
            => instance.AddDeleteResultEntityStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<IMetadata>())).ToArray());

        public static DataObjectInfoBuilder AddAddAfterReadStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.AddAfterReadStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddAddAfterReadStatements(this DataObjectInfoBuilder instance, params string[] statements)
            => instance.AddAddAfterReadStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<IMetadata>())).ToArray());

        public static DataObjectInfoBuilder AddUpdateAfterReadStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.UpdateAfterReadStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddUpdateAfterReadStatements(this DataObjectInfoBuilder instance, params string[] statements)
            => instance.AddUpdateAfterReadStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<IMetadata>())).ToArray());

        public static DataObjectInfoBuilder AddDeleteAfterReadStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(CommandEntityProviders.DeleteAfterReadStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddDeleteAfterReadStatements(this DataObjectInfoBuilder instance, params string[] statements)
            => instance.AddDeleteAfterReadStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<IMetadata>())).ToArray());
    }
}
