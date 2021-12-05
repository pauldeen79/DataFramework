using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using ModelFramework.Common.Default;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class EnumerableOfModelFrameworkMetadataExtensions
    {
        internal static IEnumerable<global::ModelFramework.Common.Contracts.IMetadata> Convert(this IEnumerable<IMetadata> metadata)
            => metadata.Select(md => new Metadata(md.Name, md.Value));
    }
}
