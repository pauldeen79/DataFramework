using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoBuilderExtensions
    {
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
