using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static EntityClassType GetEntityClassType(this IDataObjectInfo instance)
            => instance
                .Metadata
                .GetMetadataValue(Entities.EntityClassType, EntityClassType.Poco);

        /// <summary>
        /// Enriches data object info instance with additional data object info, present in metadata
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IEnumerable<IDataObjectInfo> WithAdditionalDataObjectInfos(this IDataObjectInfo instance)
        {
            yield return instance;
            foreach (var item in GetCustomMembersFromMetadata<IDataObjectInfo>(instance, Shared.CustomDataObjectInfoName))
            {
                yield return item;
            }
        }

        private static IEnumerable<T> GetCustomMembersFromMetadata<T>(IDataObjectInfo instance,
                                                                      string metadataName)
            where T : class =>
            instance
                .Metadata
                .Where(md => md.Name == metadataName)
                .Select(md => md.Value)
                .OfType<T>();
    }
}
