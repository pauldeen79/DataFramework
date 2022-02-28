namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoBuilderExtensions
{
    public static DataObjectInfoBuilder WithQueryFieldProviderNamespace(this DataObjectInfoBuilder instance, string? @namespace)
        => instance.ReplaceMetadata(QueryFieldInfos.Namespace, @namespace);

    public static DataObjectInfoBuilder AddQueryFieldProviderAttributes(this DataObjectInfoBuilder instance, params IAttribute[] attributes)
        => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.Attribute).WithValue(x)));

    public static DataObjectInfoBuilder AddQueryFieldProviderAttributes(this DataObjectInfoBuilder instance, params AttributeBuilder[] attributes)
        => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.Attribute).WithValue(x.Build())));

    public static DataObjectInfoBuilder WithQueryFieldProviderVisibility(this DataObjectInfoBuilder instance, Visibility? visibility)
        => instance.ReplaceMetadata(QueryFieldInfos.Visibility, visibility);

    public static DataObjectInfoBuilder AddQueryFieldProviderFields(this DataObjectInfoBuilder instance, params IClassField[] fields)
        => instance.AddMetadata(fields.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.Field).WithValue(x)));

    public static DataObjectInfoBuilder AddQueryFieldProviderFields(this DataObjectInfoBuilder instance, params ClassFieldBuilder[] fields)
        => instance.AddMetadata(fields.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.Field).WithValue(x.Build())));

    public static DataObjectInfoBuilder AddQueryFieldProviderConstructorParameters(this DataObjectInfoBuilder instance, params IParameter[] parameters)
        => instance.AddMetadata(parameters.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.ConstructorParameter).WithValue(x)));

    public static DataObjectInfoBuilder AddQueryFieldProviderConstructorParameters(this DataObjectInfoBuilder instance, params ParameterBuilder[] parameters)
        => instance.AddMetadata(parameters.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.ConstructorParameter).WithValue(x.Build())));

    public static DataObjectInfoBuilder AddQueryFieldProviderConstructorCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
        => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.ConstructorCodeStatement).WithValue(x)));

    public static DataObjectInfoBuilder AddQueryFieldProviderConstructorCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
        => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.ConstructorCodeStatement).WithValue(x.Build())));

    public static DataObjectInfoBuilder AddQueryFieldProviderConstructorCodeStatements(this DataObjectInfoBuilder instance, params string[] statements)
        => instance.AddQueryFieldProviderConstructorCodeStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<global::ModelFramework.Common.Contracts.IMetadata>())).ToArray());

    public static DataObjectInfoBuilder AddQueryFieldProviderGetAllFieldsCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
        => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.GetAllFieldsCodeStatement).WithValue(x)));

    public static DataObjectInfoBuilder AddQueryFieldProviderGetAllFieldsCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
        => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.GetAllFieldsCodeStatement).WithValue(x.Build())));

    public static DataObjectInfoBuilder AddQueryFieldProviderGetAllFieldsCodeStatements(this DataObjectInfoBuilder instance, params string[] statements)
        => instance.AddQueryFieldProviderGetAllFieldsCodeStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<global::ModelFramework.Common.Contracts.IMetadata>())).ToArray());

    public static DataObjectInfoBuilder AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatement[] statements)
        => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.GetDatabaseFieldNameCodeStatement).WithValue(x)));

    public static DataObjectInfoBuilder AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(this DataObjectInfoBuilder instance, params ICodeStatementBuilder[] statements)
        => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(QueryFieldInfos.GetDatabaseFieldNameCodeStatement).WithValue(x.Build())));

    public static DataObjectInfoBuilder AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(this DataObjectInfoBuilder instance, params string[] statements)
        => instance.AddQueryFieldProviderGetDatabaseFieldNameCodeStatements(statements.Select(x => new LiteralCodeStatement(x, Enumerable.Empty<global::ModelFramework.Common.Contracts.IMetadata>())).ToArray());
}
