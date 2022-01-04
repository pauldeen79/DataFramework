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
        public static DataObjectInfoBuilder WithQueryNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Queries.Namespace, @namespace);

        public static DataObjectInfoBuilder AddQueryAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Queries.Attribute).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Queries.Attribute).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryValidExpressionStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Queries.ValidExpressionStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryValidExpressionStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Queries.ValidExpressionStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryValidExpressionStatements(this DataObjectInfoBuilder instance, params string[] statements)
            => instance.AddQueryValidExpressionStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<IMetadata>())).ToArray());

        public static DataObjectInfoBuilder AddQueryValidFieldNameStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Queries.ValidFieldNameStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryValidFieldNameStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Queries.ValidFieldNameStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryValidFieldNameStatements(this DataObjectInfoBuilder instance, params string[] statements)
            => instance.AddQueryValidFieldNameStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<IMetadata>())).ToArray());

        public static DataObjectInfoBuilder AddQueryInterfaces(this DataObjectInfoBuilder instance, params string[] interfaces)
            => instance.AddMetadata(interfaces.Select(x => new MetadataBuilder().WithName(Queries.Interface).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryValidFieldNames(this DataObjectInfoBuilder instance, params string[] fieldNames)
            => instance.AddMetadata(fieldNames.Select(x => new MetadataBuilder().WithName(Queries.ValidFieldName).WithValue(x)));

        public static DataObjectInfoBuilder WithQueryMaxLimit(this DataObjectInfoBuilder instance, int? maxLimit)
            => instance.ReplaceMetadata(Queries.MaxLimit, maxLimit);
    }
}
