using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common.Extensions;
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

        public static IEnumerable<IDataObjectInfo> WithAdditionalDataObjectInfos(this IDataObjectInfo instance)
        {
            yield return instance;
            foreach (var item in GetCustomMembersFromMetadata<IDataObjectInfo>(instance, Shared.CustomDataObjectInfo))
            {
                yield return item;
            }
        }

        public static IEnumerable<IFieldInfo> GetUpdateConcurrencyCheckFields(this IDataObjectInfo dataObjectInfo)
        {
            var concurrencyCheckBehavior = dataObjectInfo.GetConcurrencyCheckBehavior();
            return dataObjectInfo
                .Fields
                .Where(fieldInfo => IsUpdateConcurrencyCheckField(dataObjectInfo, fieldInfo, concurrencyCheckBehavior));
        }

        public static bool IsUpdateConcurrencyCheckField(this IDataObjectInfo dataObjectInfo,
                                                         IFieldInfo fieldInfo,
                                                         ConcurrencyCheckBehavior concurrencyCheckBehavior)
            => !dataObjectInfo.IsReadOnly
                && fieldInfo.IsPersistable
                && (fieldInfo.IsIdentityField || fieldInfo.UseForCheckOnOriginalValues || concurrencyCheckBehavior == ConcurrencyCheckBehavior.AllFields)
                && concurrencyCheckBehavior != ConcurrencyCheckBehavior.NoFields;

        public static ConcurrencyCheckBehavior GetConcurrencyCheckBehavior(this IDataObjectInfo dataObjectInfo)
            => (ConcurrencyCheckBehavior)Enum.Parse(typeof(ConcurrencyCheckBehavior), dataObjectInfo.Metadata.Any(md => md.Name == DbCommand.ConcurrencyCheckBehaviorKey)
                ? dataObjectInfo.Metadata.First(md => md.Name == DbCommand.ConcurrencyCheckBehaviorKey).Value.ToStringWithNullCheck()
                : ConcurrencyCheckBehavior.NoFields.ToString());

        private static IEnumerable<T> GetCustomMembersFromMetadata<T>(IDataObjectInfo instance,
                                                                      string metadataName)
            where T : class
            => instance
                .Metadata
                .Where(md => md.Name == metadataName)
                .Select(md => md.Value)
                .OfType<T>();
    }
}
