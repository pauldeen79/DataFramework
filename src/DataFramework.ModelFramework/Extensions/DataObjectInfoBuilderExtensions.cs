using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;

namespace DataFramework.ModelFramework.Extensions
{
    public static class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithEntityClassType(this DataObjectInfoBuilder instance, EntityClassType entityClassType)
            => instance.ReplaceMetadata(Entities.EntityClassType, entityClassType);

        public static DataObjectInfoBuilder WithConcurrencyCheckBehavior(this DataObjectInfoBuilder instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
            => instance.ReplaceMetadata(Database.ConcurrencyCheckBehavior, concurrencyCheckBehavior);

        public static DataObjectInfoBuilder WithEntitiesNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Entities.Namespace, @namespace);

        public static DataObjectInfoBuilder WithEntityBuildersNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Builders.Namespace, @namespace);

        public static DataObjectInfoBuilder WithEntityIdentitiesNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Identities.Namespace, @namespace);

        public static DataObjectInfoBuilder WithQueriesNamespace(this DataObjectInfoBuilder instance, string? @namespace)
            => instance.ReplaceMetadata(Queries.Namespace, @namespace);

        public static DataObjectInfoBuilder AddAdditionalDataObjectInfos(this DataObjectInfoBuilder instance, params IDataObjectInfo[] dataObjectInfos)
            => instance.AddMetadata(dataObjectInfos.Select(dataObjectInfo => new Metadata(Shared.CustomDataObjectInfo, dataObjectInfo)));

        public static DataObjectInfoBuilder AddAdditionalDataObjectInfos(this DataObjectInfoBuilder instance, IEnumerable<IDataObjectInfo> dataObjectInfos)
            => instance.AddMetadata(dataObjectInfos.Select(dataObjectInfo => new Metadata(Shared.CustomDataObjectInfo, dataObjectInfo)));

        private static DataObjectInfoBuilder ReplaceMetadata(this DataObjectInfoBuilder instance, string name, object? newValue)
            => instance.Chain(() =>
            {
                instance.Metadata.Replace(name, newValue);
            });
    }
}
