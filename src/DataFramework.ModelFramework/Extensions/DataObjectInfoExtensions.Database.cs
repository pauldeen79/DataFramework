using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Data.Core.Builders;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        internal static IEnumerable<IDataObjectInfo> WithAdditionalDataObjectInfos(this IDataObjectInfo instance)
        {
            yield return instance;
            foreach (var item in GetCustomMembersFromMetadata<IDataObjectInfo>(instance, Shared.CustomDataObjectInfo))
            {
                yield return item;
            }
        }

        internal static IEnumerable<IFieldInfo> GetFindFields(this IDataObjectInfo instance)
            => instance.Fields.Where(x => (x.IsIdentityField || x.IsSqlIdentity()) && !x.SkipFieldOnFind());

        internal static IEnumerable<IFieldInfo> GetUpdateConcurrencyCheckFields(this IDataObjectInfo instance)
        {
            var concurrencyCheckBehavior = instance.GetConcurrencyCheckBehavior();
            return instance.Fields.Where(fieldInfo => IsUpdateConcurrencyCheckField(instance, fieldInfo, concurrencyCheckBehavior));
        }

        internal static bool IsUpdateConcurrencyCheckField(this IDataObjectInfo instance,
                                                           IFieldInfo fieldInfo,
                                                           ConcurrencyCheckBehavior concurrencyCheckBehavior)
            => concurrencyCheckBehavior != ConcurrencyCheckBehavior.NoFields
                && !instance.IsReadOnly
                && !fieldInfo.IsComputed
                && fieldInfo.IsPersistable
                && (fieldInfo.IsIdentityField
                    || fieldInfo.IsSqlIdentity()
                    || fieldInfo.UseForCheckOnOriginalValues
                    || concurrencyCheckBehavior == ConcurrencyCheckBehavior.AllFields);

        internal static ConcurrencyCheckBehavior GetConcurrencyCheckBehavior(this IDataObjectInfo instance)
            => (ConcurrencyCheckBehavior)Enum.Parse(typeof(ConcurrencyCheckBehavior), instance.Metadata.Any(md => md.Name == Database.ConcurrencyCheckBehavior)
                ? instance.Metadata.First(md => md.Name == Database.ConcurrencyCheckBehavior).Value.ToStringWithNullCheck()
                : ConcurrencyCheckBehavior.NoFields.ToString());

        internal static string GetTableName(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Database.TableName, instance.Name);

        internal static bool HasAddStoredProcedure(this IDataObjectInfo instance)
            => !string.IsNullOrEmpty(instance.Metadata.GetStringValue(Database.AddStoredProcedureName));

        internal static bool HasUpdateStoredProcedure(this IDataObjectInfo instance)
            => !string.IsNullOrEmpty(instance.Metadata.GetStringValue(Database.UpdateStoredProcedureName));

        internal static bool HasDeleteStoredProcedure(this IDataObjectInfo instance)
            => !string.IsNullOrEmpty(instance.Metadata.GetStringValue(Database.DeleteStoredProcedureName));

        internal static string CreateDatabaseInsertCommandText(this IDataObjectInfo instance)
        {
            var commandText = instance.Metadata.GetStringValue(Database.AddCustomCommandText);
            if (!string.IsNullOrEmpty(commandText))
            {
                return commandText;
            }

            return new InsertCommandBuilder()
                .Into($"[{instance.GetTableName()}]")
                .WithFieldNames(instance.Fields.Where(x => x.UseOnInsert()).Select(x => $"[{x.GetDatabaseFieldName()}]"))
                .WithFieldValues(instance.Fields.Where(x => x.UseOnInsert()).Select(x => $"@{x.Name.Sanitize()}"))
                .WithOutputFields(instance.GetInsertOutputFields().Select(x => $"INSERTED.[{x.GetDatabaseFieldName()}]"))
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
                .Where(instance.GetUpdateWhereStatement(x => x.UseOnUpdate()))
                .WithFieldNames(instance.Fields.Where(x => x.UseOnUpdate()).Select(x => $"[{x.GetDatabaseFieldName()}]"))
                .WithFieldValues(instance.Fields.Where(x => x.UseOnUpdate()).Select(x => $"@{x.Name.Sanitize()}"))
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
                .Where(instance.GetUpdateWhereStatement(x => x.UseOnDelete()))
                .Build()
                .CommandText;
        }

        private static IEnumerable<IFieldInfo> GetInsertOutputFields(this IDataObjectInfo instance)
            => instance
                .Fields
                .Where
                (
                    fieldInfo =>
                        fieldInfo.IsPersistable
                        &&
                        (
                            fieldInfo.IsComputed
                            || fieldInfo.IsIdentityField
                            || fieldInfo.IsSqlIdentity()
                            || fieldInfo.IsComputed
                            || fieldInfo.UseForCheckOnOriginalValues
                            || instance.GetConcurrencyCheckBehavior() == ConcurrencyCheckBehavior.AllFields
                        )
                        && fieldInfo.CanSet
                );

        internal static string GetFindWhereStatement(this IDataObjectInfo instance)
            => string.Join(" AND ", instance.GetFindFields().Select(x => $"[{x.Name}] = @{x.Name.Sanitize()}"));

        internal static string GetUpdateWhereStatement(this IDataObjectInfo instance, Predicate<IFieldInfo> predicate)
            => string.Join(" AND ", instance.GetUpdateConcurrencyCheckFields()
                                            .Where(x => predicate(x) || x.IsIdentityField || x.IsSqlIdentity())
                                            .Select(x => $"[{x.Name}] = @{x.Name.Sanitize()}Original"));
    }
}
