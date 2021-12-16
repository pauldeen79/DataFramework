using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        internal static string GetEntityRetrieverSettingsNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(EntityRetrieverSettings.Namespace)
                .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());
    }
}
