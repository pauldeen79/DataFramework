using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;

namespace DataFramework.ModelFramework.Extensions
{
    public static class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithEntityClassType(this DataObjectInfoBuilder instance, EntityClassType entityClassType)
        {
            instance.Metadata.RemoveAll(x => x.Name == Entities.EntityClassType);
            return instance.AddMetadata(new Metadata(Entities.EntityClassType, entityClassType));
        }

        public static DataObjectInfoBuilder AddAdditionalDataObjectInfos(this DataObjectInfoBuilder instance, params IDataObjectInfo[] dataObjectInfos)
            => instance.AddMetadata(dataObjectInfos.Select(dataObjectInfo => new Metadata(Shared.CustomDataObjectInfo, dataObjectInfo)));

        public static DataObjectInfoBuilder AddAdditionalDataObjectInfos(this DataObjectInfoBuilder instance, IEnumerable<IDataObjectInfo> dataObjectInfos)
            => instance.AddMetadata(dataObjectInfos.Select(dataObjectInfo => new Metadata(Shared.CustomDataObjectInfo, dataObjectInfo)));
    }
}
