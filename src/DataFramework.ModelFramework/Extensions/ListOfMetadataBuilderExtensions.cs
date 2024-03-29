﻿namespace DataFramework.ModelFramework.Extensions;

internal static class ListOfMetadataBuilderExtensions
{
    internal static List<MetadataBuilder> Replace(this List<MetadataBuilder> instance, string name, object? newValue)
        => instance.Chain(() =>
        {
            instance.RemoveAll(x => x.Name.ToString() == name);
            if (newValue != null)
            {
                instance.Add(new MetadataBuilder().WithName(name).WithValue(newValue));
            }
        });
}
