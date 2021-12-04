using DataFramework.Abstractions;
using ModelFramework.Objects.Builders;


namespace DataFramework.ModelFramework.Extensions
{
    public static class ClassBuilderExtensions
    {
        public static ClassBuilder WithSharedDataObjectInfoData(this ClassBuilder instance, IDataObjectInfo dataObjectInfo)
            => instance
                .WithPartial()
                .AddMetadata(dataObjectInfo.Metadata.Convert());
    }
}
