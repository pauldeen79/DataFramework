namespace DataFramework.Pipelines.Extensions;

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

        return string.IsNullOrEmpty(buildersNamespace)
            ? $"{instance.Name}Builder"
            : $"{buildersNamespace}.{instance.Name}Builder";
    }

    public static string GetEntityFullName(this DataObjectInfo instance, string entitiesNamespace)
        => string.IsNullOrEmpty(entitiesNamespace)
            ? instance.Name
            : $"{entitiesNamespace}.{instance.Name}";

    public static string GetEntityIdentityFullName(this DataObjectInfo instance, string entityIdentityNamespace)
        => string.IsNullOrEmpty(entityIdentityNamespace)
            ? $"{instance.Name}Identity"
            : $"{entityIdentityNamespace}.{instance.Name}Identity";

    public static string GetDatabaseTableName(this DataObjectInfo instance)
        => instance.DatabaseTableName.WhenNullOrEmpty(instance.Name);

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

    private static string GetUpdateConcurrencyWhereStatement(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior, Predicate<FieldInfo> predicate)
        => string.Join(" AND ", instance.GetUpdateConcurrencyCheckFields(concurrencyCheckBehavior)
            .Where(x => predicate(x) || x.IsIdentityField || x.IsDatabaseIdentityField)
            .Select(x => $"[{x.CreatePropertyName(instance)}] = @{x.CreatePropertyName(instance)}Original"));
}
