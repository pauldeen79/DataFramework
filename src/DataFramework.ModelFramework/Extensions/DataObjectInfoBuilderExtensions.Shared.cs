﻿namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoBuilderExtensions
{
    public static DataObjectInfoBuilder AddAdditionalDataObjectInfos(this DataObjectInfoBuilder instance,
                                                                     params IDataObjectInfo[] dataObjectInfos)
        => instance.AddMetadata(dataObjectInfos.Select(dataObjectInfo => new MetadataBuilder().WithName(Shared.CustomDataObjectInfo)
                                                                                              .WithValue(dataObjectInfo)));

    private static DataObjectInfoBuilder ReplaceMetadata(this DataObjectInfoBuilder instance, string name, object? newValue)
        => instance.Chain(() =>
        {
            instance.Metadata.Replace(name, newValue);
        });
}
