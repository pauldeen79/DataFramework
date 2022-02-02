namespace DataFramework.ModelFramework.Extensions;

internal static class ClassMethodBuilderExtensions
{
    public static ClassMethodBuilder AddCommandProviderMethod(this ClassMethodBuilder instance,
                                                              IDataObjectInfo dataObjectInfo,
                                                              string preventMetadataName,
                                                              DatabaseOperation operation,
                                                              string commandType,
                                                              string commentText)
        => instance.Chain(builder =>
        {
            if (!dataObjectInfo.Metadata.GetBooleanValue(preventMetadataName))
            {
                instance.AddLiteralCodeStatements
                (
                    $"    case {typeof(DatabaseOperation).FullName}.{operation}:",
                    $"        return new {commandType}(\"{commentText}\", source, {typeof(DatabaseOperation).FullName}.{operation}, {operation.GetMethodNamePrefix()}Parameters);"
                );
            }
        });
}
