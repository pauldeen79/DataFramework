using DataFramework.Abstractions;
using ModelFramework.Objects.Builders;


namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassBuilderExtensions
    {
        internal static ClassBuilder WithSharedDataObjectInfoData(this ClassBuilder instance, IDataObjectInfo dataObjectInfo)
            => instance
                .WithPartial()
                .AddMetadata(dataObjectInfo.Metadata.Convert());
    }
}
