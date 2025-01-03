﻿namespace DataFramework.Pipelines.Extensions;

public static class DataObjectInfoExtensions
{
    public static IEnumerable<FieldInfo> GetIdentityFields(this DataObjectInfo instance)
        => instance.Fields.Where(x => (x.IsIdentityField || x.IsDatabaseIdentityField) && !x.SkipFieldOnFind);

    public static IEnumerable<FieldInfo> GetUpdateConcurrencyCheckFields(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
        => instance.Fields.Where(fieldInfo => fieldInfo.IsUpdateConcurrencyCheckField(concurrencyCheckBehavior));

    public static IEnumerable<FieldInfo> GetOutputFields(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
        => instance
            .Fields
            .Where
            (
                fieldInfo =>
                    fieldInfo.IsPersistable
                    && fieldInfo.CanSet
                    && !string.IsNullOrEmpty(fieldInfo.GetSqlFieldType())
                    &&
                    (
                        fieldInfo.IsComputed
                        || fieldInfo.IsIdentityField
                        || fieldInfo.IsDatabaseIdentityField
                        || fieldInfo.UseForConcurrencyCheck
                        || concurrencyCheckBehavior == ConcurrencyCheckBehavior.AllFields
                    )
            );

    public static string GetEntityBuilderFullName(this DataObjectInfo instance, string entitiesNamespace, string buildersNamespace, bool isImmutable)
    {
        if (!isImmutable)
        {
            // A mutable (poco) entity can be used directly, no need for a builder
            return instance.GetEntityFullName(entitiesNamespace);
        }

        return $"{buildersNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}Builder";
    }

    public static string GetEntityFullName(this DataObjectInfo instance, string entitiesNamespace)
        => $"{entitiesNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}";

    public static string GetEntityIdentityFullName(this DataObjectInfo instance, string entityIdentityNamespace)
        => $"{entityIdentityNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}Identity";

    public static string GetEntityMapperFullName(this DataObjectInfo instance, string entityMapperNamespace)
        => $"{entityMapperNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}EntityMapper";

    public static string GetQueryFullName(this DataObjectInfo instance, string queryNamespace)
        => $"{queryNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}Query";

    public static string GetQueryBuilderFullName(this DataObjectInfo instance, string queryBuilderNamespace)
        => $"{queryBuilderNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}QueryBuilder";

    public static string GetQueryFieldInfoFullName(this DataObjectInfo instance, string queryFieldInfoNamespace)
        => $"{queryFieldInfoNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}QueryFieldInfo";

    public static string GetQueryFieldInfoProviderFullName(this DataObjectInfo instance, string queryFieldInfoProviderNamespace)
        => $"{queryFieldInfoProviderNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}QueryFieldInfoProvider";

    public static string GetCommandEntityProviderFullName(this DataObjectInfo instance, string commandEntityProviderNamespace)
        => $"{commandEntityProviderNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}CommandEntityProvider";

    public static string GetCommandProviderFullName(this DataObjectInfo instance, string commandProviderNamespace)
        => $"{commandProviderNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}CommandProvider";

    public static string GetIdentityCommandProviderFullName(this DataObjectInfo instance, string identityCommandProviderNamespace)
        => $"{identityCommandProviderNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}IdentityCommandProvider";

    public static string GetRepositoryFullName(this DataObjectInfo instance, string repositoryNamespace)
        => $"{repositoryNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}Repository";

    public static string GetRepositoryInterfaceFullName(this DataObjectInfo instance, string repositoryInterfaceNamespace)
        => $"{repositoryInterfaceNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}I{instance.Name}Repository";

    public static string GetDatabaseEntityRetrieverProviderFullName(this DataObjectInfo instance, string databaseEntityRetrieverProviderNamespace)
        => $"{databaseEntityRetrieverProviderNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}DatabaseEntityRetrieverProvider";

    public static string GetPagedDatabaseEntityRetrieverSettingsFullName(this DataObjectInfo instance, string pagedDatabaseEntityRetrieverSettingsNamespace)
        => $"{pagedDatabaseEntityRetrieverSettingsNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}DatabaseEntityRetrieverSettings";

    public static string GetPagedDatabaseEntityRetrieverSettingsProviderFullName(this DataObjectInfo instance, string pagedDatabaseEntityRetrieverSettingsProviderNamespace)
        => $"{pagedDatabaseEntityRetrieverSettingsProviderNamespace.WhenNullOrEmpty(() => instance.TypeName.GetNamespaceWithDefault()).GetNamespacePrefix()}{instance.Name}DatabaseEntityRetrieverSettingsProvider";

    public static string GetDatabaseTableName(this DataObjectInfo instance)
        => instance.DatabaseTableName.WhenNullOrEmpty(instance.Name);

    public static string GetDatabaseSchemaName(this DataObjectInfo instance)
        => instance.DatabaseSchemaName.WhenNullOrEmpty("dbo");

    public static IEnumerable<TableFieldBuilder> GetTableFields(this DataObjectInfo instance)
        => instance
            .Fields
            .Where(fi => fi.IsPersistable && fi.PropertyTypeName.IsSupportedByMap())
            .Select(fi =>
                new TableFieldBuilder()
                    .WithName(fi.GetDatabaseFieldName())
                    .WithType(fi.GetTypedSqlFieldType())
                    .WithIsRequired(fi.IsSqlRequired || fi.IsRequired)
                    .WithIsIdentity(fi.IsDatabaseIdentityField)
                    .WithNumericPrecision(fi.DatabaseNumericPrecision)
                    .WithNumericScale(fi.DatabaseNumericScale)
                    .WithStringLength(fi.GetSqlStringLength(FieldInfo.DefaultStringLength))
                    .WithStringCollation(fi.DatabaseStringCollation ?? string.Empty)
                    .WithIsStringMaxLength(fi.IsSqlStringMaxLength)
                    .AddCheckConstraints(CreateCheckConstraintExpressions(fi.DatabaseCheckConstraintExpression, fi, instance)));

    private static IEnumerable<CheckConstraintBuilder> CreateCheckConstraintExpressions(
        string? stringValue,
        FieldInfo fi,
        DataObjectInfo instance)
    {
        if (string.IsNullOrEmpty(stringValue))
        {
            yield break;
        }

        yield return new CheckConstraintBuilder()
            .WithName($"CHK_{instance.GetDatabaseTableName()}_{fi.GetDatabaseFieldName()}")
            .WithExpression(stringValue!);
    }

    public static IEnumerable<PrimaryKeyConstraintBuilder> GetTablePrimaryKeyConstraints(this DataObjectInfo instance)
        => instance.PrimaryKeyConstraints.Select(x => x.ToBuilder());

    public static IEnumerable<DefaultValueConstraintBuilder> GetTableDefaultValueConstraints(this DataObjectInfo instance)
        => instance
            .Fields
            .Where(fi => fi.DefaultValue is not null)
            .Select
                (fi => new DefaultValueConstraintBuilder()
                    .WithFieldName(fi.GetDatabaseFieldName())
                    .WithDefaultValue(GenerateDefaultValue(fi))
                    .WithName("DF_" + fi.GetDatabaseFieldName())
                );

    private static string GenerateDefaultValue(FieldInfo fi)
    {
        //HACK: Encode sql string when necessary, because the property is of type string, and there is no encoding or conversion in the Sql schema generator :(
        if (fi.GetSqlFieldType().IsDatabaseStringType())
        {
            return fi.DefaultValue.ToStringWithNullCheck().SqlEncode();
        }
        return fi.DefaultValue.ToStringWithNullCheck();
    }

    public static IEnumerable<ForeignKeyConstraintBuilder> GetTableForeignKeyConstraints(this DataObjectInfo instance)
        => instance.ForeignKeyConstraints.Select(x => x.ToBuilder());

    public static IEnumerable<IndexBuilder> GetTableIndexes(this DataObjectInfo instance)
        => instance.Indexes.Select(x => x.ToBuilder());

    public static IEnumerable<CheckConstraintBuilder> GetTableCheckConstraints(this DataObjectInfo instance)
        => instance.CheckConstraints.Select(x => x.ToBuilder());

    public static string CreateDatabaseInsertCommandText(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
    {
        var commandText = instance.CustomAddDatabaseCommandText;
        if (!string.IsNullOrEmpty(commandText))
        {
            return commandText;
        }

        return new InsertCommandBuilder()
            .Into($"[{instance.GetDatabaseTableName()}]")
            .AddFieldNames(instance.Fields.Where(x => x.UseOnInsert).Select(x => $"[{x.GetDatabaseFieldName()}]"))
            .AddFieldValues(instance.Fields.Where(x => x.UseOnInsert).Select(x => $"@{x.CreatePropertyName(instance)}"))
            .AddOutputFields(instance.GetOutputFields(concurrencyCheckBehavior).Select(x => $"INSERTED.[{x.GetDatabaseFieldName()}]"))
            .Build()
            .CommandText;
    }

    public static string CreateDatabaseUpdateCommandText(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
    {
        var commandText = instance.CustomUpdateDatabaseCommandText;
        if (!string.IsNullOrEmpty(commandText))
        {
            return commandText;
        }

        return new UpdateCommandBuilder()
            .WithTable($"[{instance.GetDatabaseTableName()}]")
            .Where(instance.GetUpdateConcurrencyWhereStatement(concurrencyCheckBehavior, x => x.UseOnUpdate))
            .AddFieldNames(instance.Fields.Where(x => x.UseOnUpdate).Select(x => $"[{x.GetDatabaseFieldName()}]"))
            .AddFieldValues(instance.Fields.Where(x => x.UseOnUpdate).Select(x => $"@{x.CreatePropertyName(instance)}"))
            .AddOutputFields(instance.GetOutputFields(concurrencyCheckBehavior).Select(x => $"INSERTED.[{x.GetDatabaseFieldName()}]"))
            .Build()
            .CommandText;
    }

    public static string CreateDatabaseDeleteCommandText(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
    {
        var commandText = instance.CustomUpdateDatabaseCommandText;
        if (!string.IsNullOrEmpty(commandText))
        {
            return commandText;
        }

        return new DeleteCommandBuilder()
            .From($"[{instance.GetDatabaseTableName()}]")
            .Where(instance.GetUpdateConcurrencyWhereStatement(concurrencyCheckBehavior, x => x.UseOnDelete))
            .AddOutputFields(instance.GetOutputFields(concurrencyCheckBehavior).Select(x => $"DELETED.[{x.GetDatabaseFieldName()}]"))
            .Build()
            .CommandText;
    }

    public static IEnumerable<StoredProcedureBuilder> GetStoredProcedures(
        this ContextBase instance,
        PipelineSettings settings,
        IFormatProvider formatProvider,
        IFormattableStringParser formattableStringParser)
    {
        if (settings.UseAddStoredProcedure && settings.CommandProviderEnableAdd)
        {
            yield return GetStoredProcedure(instance, settings.AddStoredProcedureName, DatabaseOperation.Insert, settings, formattableStringParser, formatProvider);
        }

        if (settings.UseUpdateStoredProcedure && settings.CommandProviderEnableUpdate)
        {
            yield return GetStoredProcedure(instance, settings.UpdateStoredProcedureName, DatabaseOperation.Update, settings, formattableStringParser, formatProvider);
        }

        if (settings.UseDeleteStoredProcedure && settings.CommandProviderEnableDelete)
        {
            yield return GetStoredProcedure(instance, settings.DeleteStoredProcedureName, DatabaseOperation.Delete, settings, formattableStringParser, formatProvider);
        }
    }

    private static StoredProcedureBuilder GetStoredProcedure(
        ContextBase instance,
        string procedureName,
        DatabaseOperation operation,
        PipelineSettings settings,
        IFormattableStringParser formattableStringParser,
        IFormatProvider formatProvider)
        => new StoredProcedureBuilder()
            .WithName(formattableStringParser.Parse(procedureName, formatProvider, instance).GetValueOrThrow())
            .AddParameters(GetStoredProcedureParameters(instance.SourceModel, operation, settings))
            .AddStatements(GetStoredProcedureStatements(instance.SourceModel, operation, settings));

    private static IEnumerable<StoredProcedureParameterBuilder> GetStoredProcedureParameters(DataObjectInfo instance, DatabaseOperation operation, PipelineSettings settings)
        => GetCommandTypeMetadataNameForCommandType(operation, CommandTypePart.Parameters, settings) switch
        {
            DatabaseOperation.Insert => instance.Fields.Where(x => x.UseOnInsert).Select(x => new StoredProcedureParameterBuilder().WithName(x.Name).WithType(x.GetTypedSqlFieldType(false)).WithIsStringMaxLength(x.IsMaxLengthString).WithNumericPrecision(x.DatabaseNumericPrecision).WithNumericScale(x.DatabaseNumericScale).WithStringCollation(x.DatabaseStringCollation ?? string.Empty).WithStringLength(x.StringMaxLength)),
            DatabaseOperation.Update => instance.Fields.Where(x => x.UseOnUpdate).Select(x => new StoredProcedureParameterBuilder().WithName(x.Name).WithType(x.GetTypedSqlFieldType(false)).WithIsStringMaxLength(x.IsMaxLengthString).WithNumericPrecision(x.DatabaseNumericPrecision).WithNumericScale(x.DatabaseNumericScale).WithStringCollation(x.DatabaseStringCollation ?? string.Empty).WithStringLength(x.StringMaxLength)),
            DatabaseOperation.Delete => instance.Fields.Where(x => x.UseOnDelete).Select(x => new StoredProcedureParameterBuilder().WithName(x.Name).WithType(x.GetTypedSqlFieldType(false)).WithIsStringMaxLength(x.IsMaxLengthString).WithNumericPrecision(x.DatabaseNumericPrecision).WithNumericScale(x.DatabaseNumericScale).WithStringCollation(x.DatabaseStringCollation ?? string.Empty).WithStringLength(x.StringMaxLength)),
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Unsupported command type: {operation}"),
        };

    private static IEnumerable<SqlStatementBaseBuilder> GetStoredProcedureStatements(DataObjectInfo instance, DatabaseOperation operation, PipelineSettings settings)
        => GetCommandTypeMetadataNameForCommandType(operation, CommandTypePart.Parameters, settings) switch
        {
            DatabaseOperation.Insert => CreateStoredProcedureStatements(settings.AddStoredProcedureStatements, () => instance.CreateDatabaseInsertCommandText(settings.ConcurrencyCheckBehavior)),
            DatabaseOperation.Update => CreateStoredProcedureStatements(settings.UpdateStoredProcedureStatements, () => instance.CreateDatabaseUpdateCommandText(settings.ConcurrencyCheckBehavior)),
            DatabaseOperation.Delete => CreateStoredProcedureStatements(settings.DeleteStoredProcedureStatements, () => instance.CreateDatabaseDeleteCommandText(settings.ConcurrencyCheckBehavior)),
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Unsupported command type: {operation}"),
        };

    private static IEnumerable<SqlStatementBaseBuilder> CreateStoredProcedureStatements(IEnumerable<SqlStatementBase> statements, Func<string> defaultStatementDelegate)
    {
        var any = false;
        foreach (var statement in statements)
        {
            yield return statement.ToBuilder();
            any = true;
        }

        // if there are no custom statements, then add the default statement
        if (!any)
        {
            yield return new StringSqlStatementBuilder().WithStatement(defaultStatementDelegate());
        }
    }

    private static DatabaseOperation GetCommandTypeMetadataNameForCommandType(DatabaseOperation operation, CommandTypePart commandTypePart, PipelineSettings settings)
        => operation switch
        {
            DatabaseOperation.Insert => commandTypePart == CommandTypePart.Text
                ? settings.DatabaseCommandTypeForInsertText
                : settings.DatabaseCommandTypeForInsertParameters,
            DatabaseOperation.Update => commandTypePart == CommandTypePart.Text
                ? settings.DatabaseCommandTypeForUpdateText
                : settings.DatabaseCommandTypeForUpdateParameters,
            DatabaseOperation.Delete => commandTypePart == CommandTypePart.Text
                ? settings.DatabaseCommandTypeForDeleteText
                : settings.DatabaseCommandTypeForDeleteParameters,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Unsupported command type: {operation}"),
        };

    private enum CommandTypePart { Parameters, Text }

    private static string GetUpdateConcurrencyWhereStatement(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior, Predicate<FieldInfo> predicate)
        => string.Join(" AND ", instance.GetUpdateConcurrencyCheckFields(concurrencyCheckBehavior)
            .Where(x => predicate(x) || x.IsIdentityField || x.IsDatabaseIdentityField)
            .Select(x => $"[{x.CreatePropertyName(instance)}] = @{x.CreatePropertyName(instance)}Original"));
}
