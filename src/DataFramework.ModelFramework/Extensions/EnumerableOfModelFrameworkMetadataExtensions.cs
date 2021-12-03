using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
//using DataFramework.ModelFramework.Abstractions;
using ModelFramework.Common.Default;
using ModelFramework.Common.Extensions;

namespace DataFramework.ModelFramework.Extensions
{
    public static class EnumerableOfModelFrameworkMetadataExtensions
    {
        /// <summary>
        /// Converts the specified instance, and optionally adds default usings.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="defaultUsings">The default usings.</param>
        /// <returns></returns>
        internal static IEnumerable<global::ModelFramework.Common.Contracts.IMetadata> Convert
        (
            this IEnumerable<IMetadata> metadata,
            string[]? defaultUsings = null
        ) => metadata
                //.Select(md => md is INotRenderableAsAttribute
                //    ? (global::ModelFramework.Common.Contracts.IMetadata)new ModelMetadataNotRenderableAsAttribute(md.Name, md.Value)
                //    : new Metadata(md.Name, md.Value))
                .Select(md => new Metadata(md.Name, md.Value))
                .Concat(defaultUsings.DefaultWhenNull().Select(s => new Metadata(global::ModelFramework.Objects.MetadataNames.CustomUsing, s)));

    }
}
