using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;

namespace DataFramework.ModelFramework.Extensions
{
    public static class EnumerableOfMetadataExtensions
    {
        public static string GetMetadataStringValue(this IEnumerable<IMetadata> metadata, string metadataName, string defaultValue = "")
            => metadata.GetMetadataValue<object?>(metadataName, defaultValue).ToStringWithDefault(defaultValue);

        public static T GetMetadataValue<T>(this IEnumerable<IMetadata> metadata, string metadataName, T defaultValue)
        {
            var metadataItem = metadata.FirstOrDefault(md => md.Name == metadataName);

            if (metadataItem == null)
            {
                return defaultValue;
            }

            return CreateMetadata(metadataItem, defaultValue);
        }

        public static IEnumerable<string> GetMetadataStringValues(this IEnumerable<IMetadata> metadata, string metadataName)
            => metadata.GetMetadataValues<object?>(metadataName).Select(x => x.ToStringWithDefault());

        public static IEnumerable<T> GetMetadataValues<T>(this IEnumerable<IMetadata> metadata, string metadataName)
            => metadata
                .Where(md => md.Name == metadataName)
                .Select(md => md.Value)
                .OfType<T>();

        private static T CreateMetadata<T>(IMetadata metadataItem, T defaultValue)
        {
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
