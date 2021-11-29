using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;

namespace DataFramework.ModelFramework.Extensions
{
    public static class CollectionOfMetadataExtensions
    {
        public static T GetMetadataValue<T>(this IEnumerable<IMetadata> metadata, string metadataName, T defaultValue)
        {
            var metadataItem = metadata.FirstOrDefault(md => md.Name == metadataName);

            if (metadataItem == null)
            {
                return defaultValue;
            }

            if (metadataItem.Value is T t)
            {
                return t;
            }

            if (typeof(T).IsEnum)
            {
                try
                {
                    var val = metadataItem.Value.ToStringWithNullCheck();
                    return string.IsNullOrEmpty(val)
                        ? defaultValue
                        : (T)Enum.Parse(typeof(T), val, true);
                }
                catch
                {
                    return defaultValue;
                }
            }

            if (typeof(T).FullName.StartsWith("System.Nullable`1[[") && typeof(T).GetGenericArguments()[0].IsEnum)
            {
                try
                {
                    var val = metadataItem.Value.ToStringWithNullCheck();
                    return string.IsNullOrEmpty(val)
                        ? defaultValue
                        : (T)Enum.Parse(typeof(T).GetGenericArguments()[0], val, true);
                }
                catch
                {
                    return defaultValue;
                }
            }

            return (T)Convert.ChangeType(metadataItem.Value, typeof(T));
        }
    }
}
