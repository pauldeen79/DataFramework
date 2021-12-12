using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithEntityClassType(this DataObjectInfoBuilder instance, EntityClassType entityClassType)
            => instance.ReplaceMetadata(Entities.EntityClassType, entityClassType);

        public static DataObjectInfoBuilder WithConcurrencyCheckBehavior(this DataObjectInfoBuilder instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
            => instance.ReplaceMetadata(Database.ConcurrencyCheckBehavior, concurrencyCheckBehavior);

        public static DataObjectInfoBuilder WithEntityNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Entities.Namespace, @namespace);

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

        public static DataObjectInfoBuilder WithEntityIdentityNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Identities.Namespace, @namespace);

        public static DataObjectInfoBuilder WithQueryNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Queries.Namespace, @namespace);

        public static DataObjectInfoBuilder AddQueryAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Queries.Attribute).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryAttributes(this DataObjectInfoBuilder instance, IEnumerable<IAttribute> attributes)
            => instance.AddQueryAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder AddQueryAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Queries.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryAttributes(this DataObjectInfoBuilder instance, IEnumerable<AttributeBuilder> attributes)
            => instance.AddQueryAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder AddQueryValidExpressionStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(Queries.ValidExpressionStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryValidExpressionStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddQueryValidExpressionStatements(codeStatements.ToArray());

        public static DataObjectInfoBuilder AddQueryValidExpressionStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] codeStatementBuilders)
            => instance.AddMetadata(codeStatementBuilders.Select(x => new MetadataBuilder().WithName(Queries.ValidExpressionStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryValidExpressionStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatementBuilder> codeStatementBuilders)
            => instance.AddQueryValidExpressionStatements(codeStatementBuilders.ToArray());

        public static DataObjectInfoBuilder AddQueryValidFieldNameStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(Queries.ValidFieldNameStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryValidFieldNameStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddQueryValidFieldNameStatements(codeStatements.ToArray());

        public static DataObjectInfoBuilder AddQueryValidFieldNameStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] codeStatementBuilders)
            => instance.AddMetadata(codeStatementBuilders.Select(x => new MetadataBuilder().WithName(Queries.ValidFieldNameStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryValidFieldNameStatements(this DataObjectInfoBuilder instance, IEnumerable<ICodeStatementBuilder> codeStatementBuilders)
            => instance.AddQueryValidFieldNameStatements(codeStatementBuilders.ToArray());

        public static DataObjectInfoBuilder AddAdditionalDataObjectInfos(this DataObjectInfoBuilder instance, params IDataObjectInfo[] dataObjectInfos)
            => instance.AddMetadata(dataObjectInfos.Select(dataObjectInfo => new Metadata(Shared.CustomDataObjectInfo, dataObjectInfo)));

        public static DataObjectInfoBuilder AddAdditionalDataObjectInfos(this DataObjectInfoBuilder instance, IEnumerable<IDataObjectInfo> dataObjectInfos)
            => instance.AddMetadata(dataObjectInfos.Select(dataObjectInfo => new Metadata(Shared.CustomDataObjectInfo, dataObjectInfo)));

        public static DataObjectInfoBuilder AddEntityInterfaces(this DataObjectInfoBuilder instance, params string[] interfaces)
            => instance.AddMetadata(interfaces.Select(x => new MetadataBuilder().WithName(Entities.Interfaces).WithValue(x)));

        public static DataObjectInfoBuilder AddEntityInterfaces(this DataObjectInfoBuilder instance, IEnumerable<string> interfaces)
            => instance.AddEntityInterfaces(interfaces.ToArray());

        public static DataObjectInfoBuilder AddQueryInterfaces(this DataObjectInfoBuilder instance, params string[] interfaces)
            => instance.AddMetadata(interfaces.Select(x => new MetadataBuilder().WithName(Queries.AdditionalInterface).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryInterfaces(this DataObjectInfoBuilder instance, IEnumerable<string> interfaces)
            => instance.AddQueryInterfaces(interfaces.ToArray());

        public static DataObjectInfoBuilder AddQueryValidFieldNames(this DataObjectInfoBuilder instance, params string[] fieldNames)
            => instance.AddMetadata(fieldNames.Select(x => new MetadataBuilder().WithName(Queries.AdditionalValidFieldName).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryValidFieldNames(this DataObjectInfoBuilder instance, IEnumerable<string> fieldNames)
            => instance.AddQueryValidFieldNames(fieldNames.ToArray());

        public static DataObjectInfoBuilder WithQueryMaxLimit(this DataObjectInfoBuilder instance, int? maxLimit)
            => instance.ReplaceMetadata(Queries.MaxLimit, maxLimit);

        public static DataObjectInfoBuilder WithEntityVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
            => instance.ReplaceMetadata(Entities.Visibility, visibility);

        public static DataObjectInfoBuilder AddEntityAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Entities.ClassAttribute).WithValue(x)));

        public static DataObjectInfoBuilder AddEntityAttributes(this DataObjectInfoBuilder instance, IEnumerable<IAttribute> attributes)
            => instance.AddEntityAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder AddEntityAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Entities.ClassAttribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddEntityAttributes(this DataObjectInfoBuilder instance, IEnumerable<AttributeBuilder> attributes)
            => instance.AddEntityAttributes(attributes.ToArray());

        public static DataObjectInfoBuilder WithPreventAdd(this DataObjectInfoBuilder instance, bool? preventAdd = true)
            => instance.ReplaceMetadata(Entities.PreventAdd, preventAdd);

        public static DataObjectInfoBuilder WithPreventUpdate(this DataObjectInfoBuilder instance, bool? preventUpdate = true)
            => instance.ReplaceMetadata(Entities.PreventUpdate, preventUpdate);

        public static DataObjectInfoBuilder WithPreventDelete(this DataObjectInfoBuilder instance, bool? preventDelete = true)
            => instance.ReplaceMetadata(Entities.PreventDelete, preventDelete);

        private static DataObjectInfoBuilder ReplaceMetadata(this DataObjectInfoBuilder instance, string name, object? newValue)
            => instance.Chain(() =>
            {
                instance.Metadata.Replace(name, newValue);
            });
    }
}
