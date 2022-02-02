namespace DataFramework.ModelFramework.Extensions;

internal static class ClassBuilderExtensions
{
    internal static ClassBuilder FillFrom(this ClassBuilder instance, IDataObjectInfo dataObjectInfo)
        => instance
            .WithPartial()
            .AddMetadata(dataObjectInfo.Metadata.Convert().Select(x => new global::ModelFramework.Common.Builders.MetadataBuilder(x)));
}
