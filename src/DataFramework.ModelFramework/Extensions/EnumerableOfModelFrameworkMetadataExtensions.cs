namespace DataFramework.ModelFramework.Extensions;

internal static class EnumerableOfModelFrameworkMetadataExtensions
{
    internal static IEnumerable<global::ModelFramework.Common.Contracts.IMetadata> Convert(this IEnumerable<IMetadata> metadata)
        => metadata.Select(md => new global::ModelFramework.Common.Metadata(md.Value, md.Name));
}
