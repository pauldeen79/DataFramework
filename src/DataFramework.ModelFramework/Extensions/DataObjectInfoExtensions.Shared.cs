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

        internal static RenderMetadataAsAttributesTypes GetRenderMetadataAsAttributesType(this IDataObjectInfo instance,
                                                                                         RenderMetadataAsAttributesTypes defaultValue)
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
                .Where(fieldInfo => !fieldInfo.IsComputed
                    && fieldInfo.CanSet
                    && IsUpdateConcurrencyCheckField(dataObjectInfo, fieldInfo, concurrencyCheckBehavior));
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
            => instance.Metadata.GetStringValue(Builders.Namespace)
                .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

        internal static string GetEntityIdentitiesNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Identities.Namespace)
                .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

        internal static string GetQueriesNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Queries.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty);

        internal static string GetRepositoriesNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Repositories.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty);

        internal static string GetRepositoriesInterfaceNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(Repositories.InterfaceNamespace, instance.GetRepositoriesNamespace());

        internal static string GetCommandProvidersNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(CommandProviders.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty);

        internal static string GetQueryFieldProvidersNamespace(this IDataObjectInfo instance)
            => instance.Metadata.GetStringValue(QueryFieldProviders.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty);

        internal static string GetEntityFullName(this IDataObjectInfo instance)
        {
            var ns = instance.GetEntitiesNamespace();
            return string.IsNullOrEmpty(ns)
                ? instance.Name
                : $"{ns}.{instance.Name}";
        }

        internal static string GetEntityIdentityFullName(this IDataObjectInfo instance)
        {
            var ns = instance.GetEntityIdentitiesNamespace();
            return string.IsNullOrEmpty(ns)
                ? $"{instance.Name}Identity"
                : $"{ns}.{instance.Name}Identity";
        }

        internal static string GetEntityRetrieverFullName(this IDataObjectInfo instance)
        {
            var ns = instance.GetEntityRetrieverSettingsNamespace();
            return string.IsNullOrEmpty(ns)
                ? $"{instance.Name}PagedEntityRetrieverSettings"
                : $"{ns}.{instance.Name}PagedEntityRetrieverSettings";
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
                                                                        RenderMetadataAsAttributesTypes renderMetadataAsAttributes,
                                                                        string attributeName)
        {
            if (renderMetadataAsAttributes.HasFlag(RenderMetadataAsAttributesTypes.Custom))
            {
                return instance.Metadata.GetValues<IAttribute>(attributeName).Select(x => new AttributeBuilder(x));
            }

            return Enumerable.Empty<AttributeBuilder>();
        }
    }
}
