namespace DataFramework.Pipelines.Extensions;

public static class MethodBuilderExtensions
{
    public static MethodBuilder AddCommandProviderMethod(
        this MethodBuilder instance,
        DataObjectInfo dataObjectInfo,
        bool enabled,
        DatabaseOperation operation,
        string commandType,
        string commentText)
    => instance.Chain(builder =>
    {
        if (enabled)
        {
            instance.AddStringCodeStatements
            (
                $"    case {typeof(DatabaseOperation).FullName}.{operation}:",
                $"        return new {commandType}(\"{commentText}\", source, {typeof(DatabaseOperation).FullName}.{operation}, {operation.GetMethodNamePrefix()}Parameters);"
            );
        }
    });
}
