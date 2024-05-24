namespace DataFramework.Pipelines.Extensions;

public static class DataObjectInfoExtensions
{
    internal static IEnumerable<FieldInfo> GetUpdateConcurrencyCheckFields(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
        => instance.Fields.Where(fieldInfo => IsUpdateConcurrencyCheckField(instance, fieldInfo, concurrencyCheckBehavior));

    internal static bool IsUpdateConcurrencyCheckField(this DataObjectInfo instance,
                                                       FieldInfo fieldInfo,
                                                       ConcurrencyCheckBehavior concurrencyCheckBehavior)
        => concurrencyCheckBehavior != ConcurrencyCheckBehavior.NoFields
            &&
            (
                concurrencyCheckBehavior == ConcurrencyCheckBehavior.AllFields
                || fieldInfo.UseForConcurrencyCheck
                ||
                (
                    !fieldInfo.IsComputed
                    && fieldInfo.IsPersistable
                    && (fieldInfo.IsIdentityField || fieldInfo.IsDatabaseIdentityField)
                )
            );
}
