using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Data.Core.Builders;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        internal static string GetDatabaseFileGroupName(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Database.FileGroupName);

        internal static string GetDatabaseSchemaName(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Database.SchemaName, "dbo");

        internal static string GetTableName(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Database.TableName, instance.Name);

        internal static bool HasAddStoredProcedure(this IDataObjectInfo instance)
            => instance.Metadata.GetBooleanValue(Database.HasAddStoredProcedure);

        internal static bool HasUpdateStoredProcedure(this IDataObjectInfo instance)
            => instance.Metadata.GetBooleanValue(Database.HasUpdateStoredProcedure);

        internal static bool HasDeleteStoredProcedure(this IDataObjectInfo instance)
            => instance.Metadata.GetBooleanValue(Database.HasDeleteStoredProcedure);

        internal static string CreateDatabaseInsertCommandText(this IDataObjectInfo instance)
        {
            var commandText = instance.Metadata.GetStringValue(Database.AddCustomCommandText);
            if (!string.IsNullOrEmpty(commandText))
            {
                return commandText;
            }

            return new InsertCommandBuilder()
                .Into($"[{instance.GetTableName()}]")
                .AddFieldNames(instance.Fields.Where(x => x.UseOnInsert()).Select(x => $"[{x.GetDatabaseFieldName()}]"))
                .AddFieldValues(instance.Fields.Where(x => x.UseOnInsert()).Select(x => $"@{x.CreatePropertyName(instance)}"))
                .AddOutputFields(instance.GetOutputFields().Select(x => $"INSERTED.[{x.GetDatabaseFieldName()}]"))
                .Build()
                .CommandText;
        }

        internal static string CreateDatabaseUpdateCommandText(this IDataObjectInfo instance)
        {
            var commandText = instance.Metadata.GetStringValue(Database.UpdateCustomCommandText);
            if (!string.IsNullOrEmpty(commandText))
            {
                return commandText;
            }

            return new UpdateCommandBuilder()
                .WithTable($"[{instance.GetTableName()}]")
                .Where(instance.GetUpdateConcurrencyWhereStatement(x => x.UseOnUpdate()))
                .AddFieldNames(instance.Fields.Where(x => x.UseOnUpdate()).Select(x => $"[{x.GetDatabaseFieldName()}]"))
                .AddFieldValues(instance.Fields.Where(x => x.UseOnUpdate()).Select(x => $"@{x.CreatePropertyName(instance)}"))
                .AddOutputFields(instance.GetOutputFields().Select(x => $"INSERTED.[{x.GetDatabaseFieldName()}]"))
                .Build()
                .CommandText;
        }

        internal static string CreateDatabaseDeleteCommandText(this IDataObjectInfo instance)
        {
            var commandText = instance.Metadata.GetStringValue(Database.DeleteCustomCommandText);
            if (!string.IsNullOrEmpty(commandText))
            {
                return commandText;
            }

            return new DeleteCommandBuilder()
                .From($"[{instance.GetTableName()}]")
                .Where(instance.GetUpdateConcurrencyWhereStatement(x => x.UseOnDelete()))
                .AddOutputFields(instance.GetOutputFields().Select(x => $"DELETED.[{x.GetDatabaseFieldName()}]"))
                .Build()
                .CommandText;
        }

        private static IEnumerable<IFieldInfo> GetOutputFields(this IDataObjectInfo instance)
            => instance
                .Fields
                .Where
                (
                    fieldInfo =>
                        fieldInfo.IsPersistable
                        && fieldInfo.CanSet
                        && fieldInfo.TypeName?.IsSupportedByMap() == true
                        &&
                        (
                            fieldInfo.IsComputed
                            || fieldInfo.IsIdentityField
                            || fieldInfo.IsSqlIdentity()
                            || fieldInfo.UseForConcurrencyCheck
                            || instance.GetConcurrencyCheckBehavior() == ConcurrencyCheckBehavior.AllFields
                        )
                );

        private static string GetUpdateConcurrencyWhereStatement(this IDataObjectInfo instance, Predicate<IFieldInfo> predicate)
            => string.Join(" AND ", instance.GetUpdateConcurrencyCheckFields()
                                            .Where(x => predicate(x) || x.IsIdentityField || x.IsSqlIdentity())
                                            .Select(x => $"[{x.CreatePropertyName(instance)}] = @{x.CreatePropertyName(instance)}Original"));
    }
}
