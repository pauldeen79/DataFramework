using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static class FieldInfoExtensions
    {
        public static string CreatePropertyName(this IFieldInfo instance, IDataObjectInfo dataObjectInfo)
            => instance.Name == dataObjectInfo.Name
                ? string.Format(dataObjectInfo.Metadata.GetMetadataStringValue(Shared.PropertyNameDeconflictionFormatStringName, "{0}Property"), instance.Name)
                : instance.Name;

        public static bool IsRequired(this IFieldInfo instance)
            => instance.Metadata.GetMetadataValues<IAttribute>(Entities.EntitiesAttribute).Any(a => a.Name == "System.ComponentModel.DataAnnotations.Required");
    }
}
