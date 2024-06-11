namespace DataFramework.Pipelines.Extensions;

public static class FieldInfoExtensions
{
    public static bool IsUpdateConcurrencyCheckField(this FieldInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
    => concurrencyCheckBehavior != ConcurrencyCheckBehavior.NoFields
        &&
        (
            concurrencyCheckBehavior == ConcurrencyCheckBehavior.AllFields
            || instance.UseForConcurrencyCheck
            ||
            (
                !instance.IsComputed
                && instance.IsPersistable
                && (instance.IsIdentityField || instance.IsDatabaseIdentityField)
            )
        );
}
