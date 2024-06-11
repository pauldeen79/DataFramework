namespace DataFramework.Pipelines.Extensions;

public static class DataObjectInfoExtensions
{
    public static IEnumerable<FieldInfo> GetUpdateConcurrencyCheckFields(this DataObjectInfo instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
        => instance.Fields.Where(fieldInfo => fieldInfo.IsUpdateConcurrencyCheckField(concurrencyCheckBehavior));

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
