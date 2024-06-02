namespace DataFramework.Pipelines.Extensions;

public static class DataObjectInfoExtensions
{
    public static IEnumerable<FieldInfo> GetUpdateConcurrencyCheckFields(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
        => instance.Fields.Where(fieldInfo => IsUpdateConcurrencyCheckField(instance, fieldInfo, concurrencyCheckBehavior));

    public static bool IsUpdateConcurrencyCheckField(this DataObjectInfo instance,
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

    public static string GetBuilderTypeName(this DataObjectInfo instance, PipelineSettings settings)
    {
        var ns = settings.DefaultBuilderNamespace
            .WhenNullOrEmpty(settings.DefaultEntityNamespace)
            .WhenNullOrEmpty(instance.TypeName.GetNamespaceWithDefault());

        var name = string.IsNullOrEmpty(instance.TypeName)
            ? instance.Name
            : instance.TypeName!.GetClassName().WhenNullOrEmpty(instance.Name);

        return string.IsNullOrEmpty(ns)
            ? $"{name}Builder"
            : $"{ns}.{name}Builder";
    }
}
