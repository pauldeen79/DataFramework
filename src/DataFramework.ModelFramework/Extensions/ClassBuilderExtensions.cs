using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;


namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassBuilderExtensions
    {
        internal static ClassBuilder FillFrom(this ClassBuilder instance, IDataObjectInfo dataObjectInfo)
            => instance
                .WithPartial()
                .WithVisibility(dataObjectInfo.Metadata.GetValue(Entities.Visibility, () => dataObjectInfo.IsVisible.ToVisibility()))
                .AddMetadata(dataObjectInfo.Metadata.Convert());
    }
}
