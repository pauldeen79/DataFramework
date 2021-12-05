using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class FieldInfoExtensions
    {
        internal static string CreatePropertyName(this IFieldInfo instance, IDataObjectInfo dataObjectInfo)
            => instance.Name == dataObjectInfo.Name
                ? string.Format(dataObjectInfo.Metadata.GetMetadataStringValue(Shared.PropertyNameDeconflictionFormatStringName, "{0}Property"), instance.Name).Sanitize()
                : instance.Name.Sanitize();

        internal static bool IsRequired(this IFieldInfo instance)
            => instance.Metadata.GetMetadataValues<IAttribute>(Entities.EntitiesAttribute).Any(a => a.Name == "System.ComponentModel.DataAnnotations.Required");

        internal static ParameterBuilder ToParameterBuilder(this IFieldInfo instance)
            => new ParameterBuilder().WithName(instance.Name.Sanitize().ToPascalCase())
                                     .WithTypeName(instance.TypeName)
                                     .WithDefaultValue(instance.DefaultValue)
                                     .WithIsNullable(instance.IsNullable);
    }
}
