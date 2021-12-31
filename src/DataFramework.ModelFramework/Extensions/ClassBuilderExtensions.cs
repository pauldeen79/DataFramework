using System.Linq;
using DataFramework.Abstractions;
using ModelFramework.Common.Builders;
using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassBuilderExtensions
    {
        internal static ClassBuilder FillFrom(this ClassBuilder instance, IDataObjectInfo dataObjectInfo)
            => instance
                .WithPartial()
                .AddMetadata(dataObjectInfo.Metadata.Convert().Select(x => new MetadataBuilder(x)));
    }
}
