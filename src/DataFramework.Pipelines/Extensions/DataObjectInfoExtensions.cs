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
}
