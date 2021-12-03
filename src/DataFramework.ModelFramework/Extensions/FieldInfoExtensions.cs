using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;

namespace DataFramework.ModelFramework.Extensions
{
    public static class FieldInfoExtensions
    {
        public static string CreatePropertyName(this IFieldInfo instance, IDataObjectInfo dataObjectInfo)
            => instance.Name == dataObjectInfo.Name
                ? string.Format(dataObjectInfo.Metadata.GetMetadataStringValue(Shared.PropertyNameDeconflictionFormatStringName, "{0}Property"), instance.Name)
                : instance.Name;
    }
}
