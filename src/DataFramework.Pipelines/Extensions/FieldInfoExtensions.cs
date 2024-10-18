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

    public static IEnumerable<AttributeBuilder> GetEntityClassPropertyAttributes(this FieldInfo instance, string instanceName, bool addComponentModelAttributes)
    {
        if (addComponentModelAttributes && string.IsNullOrEmpty(instance.DisplayName) && instance.Name == instanceName)
        {
            //if the field name is equal to the DataObjectInstance name, then the property will be renamed to keep the C# compiler happy.
            //in this case, we would like to add a DisplayName attribute, so the property looks right in the UI. (PropertyGrid etc.)
            yield return new AttributeBuilder().AddNameAndParameter("System.ComponentModel.DataAnnotations.DisplayName", instance.Name);
        }
    }

    public static string GetDatabaseFieldName(this FieldInfo instance)
        => instance.DatabaseFieldName.WhenNullOrEmpty(instance.Name);
}
