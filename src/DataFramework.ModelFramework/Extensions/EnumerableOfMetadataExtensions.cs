﻿namespace DataFramework.ModelFramework.Extensions;

public static class EnumerableOfMetadataExtensions
{
    public static string GetStringValue(this IEnumerable<IMetadata> metadata, string metadataName, string defaultValue = "")
        => metadata.GetStringValue(metadataName, () => defaultValue);

    public static string GetStringValue(this IEnumerable<IMetadata> metadata, string metadataName, Func<string> defaultValueDelegate)
        => metadata.GetValue<object?>(metadataName, defaultValueDelegate).ToStringWithDefault(defaultValueDelegate());

    public static bool GetBooleanValue(this IEnumerable<IMetadata> metadata, string metadataName, bool defaultValue = false)
        => metadata.GetBooleanValue(metadataName, () => defaultValue);

    public static bool GetBooleanValue(this IEnumerable<IMetadata> metadata, string metadataName, Func<bool> defaultValueDelegate)
        => metadata.GetValue<object?>(metadataName, () => defaultValueDelegate.Invoke()).ToStringWithDefault().IsTrue();

    public static T GetValue<T>(this IEnumerable<IMetadata> metadata, string metadataName, Func<T> defaultValueDelegate)
    {
        var metadataItem = metadata.FirstOrDefault(md => md.Name == metadataName);

        if (metadataItem == null)
        {
            return defaultValueDelegate();
        }

        return CreateMetadata(metadataItem, defaultValueDelegate);
    }

    public static IEnumerable<string> GetStringValues(this IEnumerable<IMetadata> metadata, string metadataName)
        => metadata.GetValues<object?>(metadataName).Select(x => x.ToStringWithDefault());

    public static IEnumerable<T> GetValues<T>(this IEnumerable<IMetadata> metadata, string metadataName)
        => metadata
            .Where(md => md.Name == metadataName)
            .Select(md => md.Value)
            .OfType<T>();

    private static T CreateMetadata<T>(IMetadata metadataItem, Func<T> defaultValueDelegate)
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
                    ? defaultValueDelegate()
                    : (T)Enum.Parse(typeof(T), val, true);
            }
            catch
            {
                return defaultValueDelegate();
            }
        }

        if (typeof(T).FullName.StartsWith("System.Nullable`1[[") && typeof(T).GetGenericArguments()[0].IsEnum)
        {
            try
            {
                var val = metadataItem.Value.ToStringWithNullCheck();
                return string.IsNullOrEmpty(val)
                    ? defaultValueDelegate()
                    : (T)Enum.Parse(typeof(T).GetGenericArguments()[0], val, true);
            }
            catch
            {
                return defaultValueDelegate();
            }
        }

        return (T)Convert.ChangeType(metadataItem.Value, typeof(T));
    }
}
