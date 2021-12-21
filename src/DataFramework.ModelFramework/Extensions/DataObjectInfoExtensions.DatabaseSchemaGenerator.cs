using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common.Extensions;
using CrossCutting.Data.Abstractions;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Database.Builders;
using ModelFramework.Database.Contracts;
using ModelFramework.Database.Default;
using ModelFramework.Database.Extensions;
using ModelFramework.Database.SqlStatements.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static IEnumerable<ISchema> ToSchemas(this IEnumerable<IDataObjectInfo> instance, GeneratorSettings settings)
            => instance.ToSchemaBuilders(settings).Select(x => x.Build());

        public static IEnumerable<SchemaBuilder> ToSchemaBuilders(this IEnumerable<IDataObjectInfo> instance, GeneratorSettings settings)
        {
            return instance
                .GroupBy(x => x.GetDatabaseSchemaName())
                .Select
                (
                    x => new SchemaBuilder()
                        .WithName(x.Key)
                        .AddTables(instance.Select(x => x.ToTableBuilder()))
                        .AddTables(instance.SelectMany(x => x.GetAdditionalTables()))
                        .AddStoredProcedures(instance.SelectMany(doi => doi.GetStoredProcedures()))
                        .AddViews(instance.SelectMany(GetViews))
                );
        }

        internal static TableBuilder ToTableBuilder(this IDataObjectInfo instance)
            => new TableBuilder()
                .WithName(instance.GetTableName())
                .WithFileGroupName(instance.GetDatabaseFileGroupName())
                .AddFields(GetTableFields(instance))
                .AddPrimaryKeyConstraints(GetTablePrimaryKeyConstraints(instance))
                .AddDefaultValueConstraints(GetTableDefaultValueConstraints(instance))
                .AddForeignKeyConstraints(GetTableForeignKeyConstraints(instance))
                .AddIndexes(GetTableIndexes(instance))
                .AddCheckConstraints(GetTableCheckConstraints(instance));

        internal static IEnumerable<TableFieldBuilder> GetTableFields(IDataObjectInfo instance)
            => instance
                .Fields
                .Where(fi => fi.IsPersistable && fi.GetPropertyTypeName().IsSupportedByMap())
                .Select(fi =>
                    new TableFieldBuilder()
                        .WithName(fi.GetDatabaseFieldName())
                        .WithType(fi.GetSqlFieldType())
                        .WithIsRequired(fi.IsSqlRequired() || fi.IsRequired())
                        .WithIsIdentity(fi.IsSqlIdentity())
                        .WithNumericPrecision(fi.GetSqlNumericPrecision())
                        .WithNumericScale(fi.GetSqlNumericScale())
                        .WithStringLength(fi.GetSqlStringLength(FieldInfoExtensions.DefaultStringLength))
                        .WithStringCollation(fi.GetSqlStringCollation())
                        .WithIsStringMaxLength(fi.IsSqlStringMaxLength())
                        .AddCheckConstraints(CreateCheckConstraintExpressions(fi.GetCheckConstraintExpression(), fi, instance)));

        private static IEnumerable<CheckConstraintBuilder> CreateCheckConstraintExpressions(string stringValue,
                                                                                                      IFieldInfo fi,
                                                                                                      IDataObjectInfo instance)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                yield break;
            }
            yield return new CheckConstraintBuilder()
                .WithName($"CHK_{instance.GetTableName()}_{fi.GetDatabaseFieldName()}")
                .WithExpression(stringValue);
        }

        private static IEnumerable<IPrimaryKeyConstraint> GetTablePrimaryKeyConstraints(IDataObjectInfo instance)
            => instance.Metadata.GetValues<IPrimaryKeyConstraint>(Database.PrimaryKeyConstraint);

        private static IEnumerable<IDefaultValueConstraint> GetTableDefaultValueConstraints(IDataObjectInfo instance)
            => instance
                .Fields
                .Where(fi => fi.DefaultValue != null)
                .Select
                    (fi => new DefaultValueConstraint
                        (
                            fi.GetDatabaseFieldName(),
                            GenerateDefaultValue(fi),
                            "DF_" + fi.GetDatabaseFieldName()
                        )
                    );

        private static string GenerateDefaultValue(IFieldInfo fi)
        {
            //HACK: Encode sql string when necessary, because the property is of type string, and there is no encoding or conversion in the Sql schema generator :(
            if (fi.GetSqlFieldType().IsDatabaseStringType())
            {
                return fi.DefaultValue.ToStringWithNullCheck().SqlEncode();
            }
            return fi.DefaultValue.ToStringWithNullCheck();
        }

        private static IEnumerable<IForeignKeyConstraint> GetTableForeignKeyConstraints(IDataObjectInfo instance)
            => instance.Metadata.GetValues<IForeignKeyConstraint>(Database.ForeignKeyConstraint);

        private static IEnumerable<IIndex> GetTableIndexes(IDataObjectInfo instance)
            => instance.Metadata.GetValues<IIndex>(Database.Index);

        private static IEnumerable<ICheckConstraint> GetTableCheckConstraints(IDataObjectInfo instance)
            => instance.Metadata.GetValues<ICheckConstraint>(Database.CheckConstraint);

        private static IEnumerable<IView> GetViews(this IDataObjectInfo instance)
            => instance.Metadata.GetValues<IView>(Database.View);

        private static IEnumerable<ITable> GetAdditionalTables(this IDataObjectInfo instance)
            => instance.Metadata.GetValues<ITable>(Database.AdditionalTable);

        private static IEnumerable<IStoredProcedure> GetAdditionalStoredProcedures(this IDataObjectInfo instance)
            => instance.Metadata.GetValues<IStoredProcedure>(Database.AdditionalStoredProcedure);

        private static IEnumerable<StoredProcedureBuilder> GetStoredProcedures(this IDataObjectInfo instance)
        {
            if (instance.HasAddStoredProcedure() && !instance.Metadata.Any(md => md.Name == CommandProviders.PreventAdd))
            {
                yield return GetStoredProcedure(instance, Database.AddStoredProcedureName, "Insert{0}", DatabaseOperation.Insert);
            }

            if (instance.HasUpdateStoredProcedure() && !instance.Metadata.Any(md => md.Name == CommandProviders.PreventUpdate))
            {
                yield return GetStoredProcedure(instance, Database.UpdateStoredProcedureName, "Update{0}", DatabaseOperation.Update);
            }

            if (instance.HasDeleteStoredProcedure() && !instance.Metadata.Any(md => md.Name == CommandProviders.PreventDelete))
            {
                yield return GetStoredProcedure(instance, Database.DeleteStoredProcedureName, "Delete{0}", DatabaseOperation.Delete);
            }

            foreach (var storedProcedure in GetAdditionalStoredProcedures(instance))
            {
                yield return new StoredProcedureBuilder(storedProcedure);
            }
        }

        private static StoredProcedureBuilder GetStoredProcedure(IDataObjectInfo instance, string metadataName, string procedureName, DatabaseOperation operation)
            => new StoredProcedureBuilder()
                .WithName(string.Format(instance.Metadata.GetStringValue(metadataName, procedureName), instance.GetTableName()))
                .AddParameters(GetStoredProcedureParameters(instance, operation))
                .AddStatements(GetStoredProcedureStatements(instance, operation));

        private static IEnumerable<StoredProcedureParameterBuilder> GetStoredProcedureParameters(IDataObjectInfo instance, DatabaseOperation operation)
        {
            var derrivedOperation = instance.Metadata.GetValue(GetCommandTypeMetadataNameForCommandType(operation, CommandTypePart.Parameters), () => operation);

            switch (derrivedOperation)
            {
                case DatabaseOperation.Insert:
                    return instance.Fields.Where(x => x.UseOnInsert()).Select(x => new StoredProcedureParameterBuilder().WithName(x.Name).WithType(x.GetSqlFieldType(true)));
                case DatabaseOperation.Update:
                    return instance.Fields.Where(x => x.UseOnUpdate()).Select(x => new StoredProcedureParameterBuilder().WithName(x.Name).WithType(x.GetSqlFieldType(true)));
                case DatabaseOperation.Delete:
                    return instance.Fields.Where(x => x.UseOnDelete()).Select(x => new StoredProcedureParameterBuilder().WithName(x.Name).WithType(x.GetSqlFieldType(true)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), "Unsupported command type: " + operation);
            }
        }

        private static IEnumerable<ISqlStatementBuilder> GetStoredProcedureStatements(IDataObjectInfo instance, DatabaseOperation operation)
        {
            var derrivedOperation = instance.Metadata.GetValue(GetCommandTypeMetadataNameForCommandType(operation, CommandTypePart.Parameters), () => operation);

            switch (derrivedOperation)
            {
                case DatabaseOperation.Insert:
                    return CreateStoredProcedureStatements(instance, Database.AddStoredProcedureStatement, () => instance.CreateDatabaseInsertCommandText());
                case DatabaseOperation.Update:
                    return CreateStoredProcedureStatements(instance, Database.UpdateStoredProcedureStatement, () => instance.CreateDatabaseUpdateCommandText());
                case DatabaseOperation.Delete:
                    return CreateStoredProcedureStatements(instance, Database.DeleteStoredProcedureStatement, () => instance.CreateDatabaseDeleteCommandText());
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), "Unsupported command type: " + operation);
            }
        }

        private static IEnumerable<ISqlStatementBuilder> CreateStoredProcedureStatements(IDataObjectInfo instance, string metadataName, Func<string> defaultStatementDelegate)
        {
            var statements = instance.Metadata.GetValues<ISqlStatement>(metadataName).ToArray();
            foreach (var statement in statements)
            {
                yield return statement.CreateBuilder();
            }
            if (!statements.Any())
            {
                yield return new LiteralSqlStatementBuilder().WithStatement(defaultStatementDelegate());
            }
        }

        private static string GetCommandTypeMetadataNameForCommandType(DatabaseOperation operation, CommandTypePart commandTypePart)
        {
            return operation switch
            {
                DatabaseOperation.Insert => commandTypePart == CommandTypePart.Text
                    ? Database.CommandTypeForInsertTextName
                    : Database.CommandTypeForInsertParametersName,
                DatabaseOperation.Update => commandTypePart == CommandTypePart.Text
                    ? Database.CommandTypeForUpdateTextName
                    : Database.CommandTypeForUpdateParametersName,
                DatabaseOperation.Delete => commandTypePart == CommandTypePart.Text
                    ? Database.CommandTypeForDeleteTextName
                    : Database.CommandTypeForDeleteParametersName,
                _ => throw new ArgumentOutOfRangeException(nameof(operation), "Unsupported command type: " + operation),
            };
        }

        private enum CommandTypePart { Parameters, Text }
    }
}
