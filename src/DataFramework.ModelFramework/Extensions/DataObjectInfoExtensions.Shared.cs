using System;
using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        internal static EntityClassType GetEntityClassType(this IDataObjectInfo instance, EntityClassType defaultValue)
            => instance
                .Metadata
                .GetValue(Entities.EntityClassType, () => defaultValue);

        internal static RenderMetadataAsAttributesType GetRenderMetadataAsAttributesType(this IDataObjectInfo instance,
                                                                                         RenderMetadataAsAttributesType defaultValue)
            => instance
                .Metadata
                .GetValue(Entities.RenderMetadataAsAttributesType, () => defaultValue);

        internal static IEnumerable<IDataObjectInfo> WithAdditionalDataObjectInfos(this IDataObjectInfo instance)
        {
            yield return instance;
            foreach (var item in GetCustomMembersFromMetadata<IDataObjectInfo>(instance, Shared.CustomDataObjectInfo))
            {
                yield return item;
            }
        }

        internal static IEnumerable<IFieldInfo> GetUpdateConcurrencyCheckFields(this IDataObjectInfo dataObjectInfo)
        {
            var concurrencyCheckBehavior = dataObjectInfo.GetConcurrencyCheckBehavior();
            return dataObjectInfo
                .Fields
                .Where(fieldInfo => IsUpdateConcurrencyCheckField(dataObjectInfo, fieldInfo, concurrencyCheckBehavior));
        }

        internal static bool IsUpdateConcurrencyCheckField(this IDataObjectInfo dataObjectInfo,
                                                           IFieldInfo fieldInfo,
                                                           ConcurrencyCheckBehavior concurrencyCheckBehavior)
            => !dataObjectInfo.IsReadOnly
                && fieldInfo.IsPersistable
                && (fieldInfo.IsIdentityField || fieldInfo.UseForCheckOnOriginalValues || concurrencyCheckBehavior == ConcurrencyCheckBehavior.AllFields)
                && concurrencyCheckBehavior != ConcurrencyCheckBehavior.NoFields;

        internal static ConcurrencyCheckBehavior GetConcurrencyCheckBehavior(this IDataObjectInfo dataObjectInfo)
            => (ConcurrencyCheckBehavior)Enum.Parse(typeof(ConcurrencyCheckBehavior), dataObjectInfo.Metadata.Any(md => md.Name == Database.ConcurrencyCheckBehavior)
                ? dataObjectInfo.Metadata.First(md => md.Name == Database.ConcurrencyCheckBehavior).Value.ToStringWithNullCheck()
                : ConcurrencyCheckBehavior.NoFields.ToString());

        internal static string GetEntitiesNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Entities.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty);

        internal static string GetEntityBuildersNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Entities.BuildersNamespace)
                .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

        internal static string GetEntityIdentitiesNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Entities.IdentitiesNamespace)
                .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

        internal static string GetEntityFullName(this IDataObjectInfo instance)
        {
            var ns = instance.GetEntitiesNamespace();
            return string.IsNullOrEmpty(ns)
                ? instance.Name
                : $"{ns}.{instance.Name}";
        }

        private static IEnumerable<T> GetCustomMembersFromMetadata<T>(IDataObjectInfo instance,
                                                                      string metadataName)
            where T : class
            => instance
                .Metadata
                .Where(md => md.Name == metadataName)
                .Select(md => md.Value)
                .OfType<T>();

        private static IEnumerable<AttributeBuilder> GetClassAttributes(this IDataObjectInfo instance,
                                                                        RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                        string attributeName)
        {
            if (renderMetadataAsAttributes == RenderMetadataAsAttributesType.Validation)
            {
                return instance.Metadata.GetValues<IAttribute>(attributeName).Select(x => new AttributeBuilder(x));
            }

            return Enumerable.Empty<AttributeBuilder>();
        }
    }
}
