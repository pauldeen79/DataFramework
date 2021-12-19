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

        public static DataObjectInfoBuilder AddQueryFieldProviderFields(this DataObjectInfoBuilder instance, params IClassField[] fields)
            => instance.AddMetadata(fields.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.Field).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryFieldProviderFields(this DataObjectInfoBuilder instance, params ClassFieldBuilder[] fields)
            => instance.AddMetadata(fields.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.Field).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryFieldProviderConstructorParameters(this DataObjectInfoBuilder instance, params IParameter[] parameters)
            => instance.AddMetadata(parameters.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.ConstructorParameter).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryFieldProviderConstructorParameters(this DataObjectInfoBuilder instance, params ParameterBuilder[] parameters)
            => instance.AddMetadata(parameters.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.ConstructorParameter).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryFieldProviderConstructorCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.ConstructorCodeStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryFieldProviderConstructorCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.ConstructorCodeStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryFieldProviderGetAllFieldsCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.GetAllFieldsCodeStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryFieldProviderGetAllFieldsCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.GetAllFieldsCodeStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.GetDatabaseFieldNameCodeStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.GetDatabaseFieldNameCodeStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryFieldProviderGetSelectFieldsCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.GetSelectFieldsCodeStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryFieldProviderGetSelectFieldsCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.GetSelectFieldsCodeStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddQueryFieldProviderValidateExpressionCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.ValidateExpressionStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddQueryFieldProviderValidateExpressionCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldProviders.ValidateExpressionStatement).WithValue(x.Build())));
    }
}
