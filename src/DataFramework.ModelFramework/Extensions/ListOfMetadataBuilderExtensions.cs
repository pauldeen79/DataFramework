using System.Collections.Generic;
using CrossCutting.Common.Extensions;
using DataFramework.Core.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ListOfMetadataBuilderExtensions
    {
        internal static List<MetadataBuilder> Replace(this List<MetadataBuilder> instance, string name, object? newValue)
            => instance.Chain(() =>
            {
                instance.RemoveAll(x => x.Name == name);
                if (newValue != null)
                {
                    instance.Add(new MetadataBuilder().WithName(name).WithValue(newValue));
                }
            });
    }
}
