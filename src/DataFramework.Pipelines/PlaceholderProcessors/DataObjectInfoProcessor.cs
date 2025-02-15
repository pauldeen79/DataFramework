namespace DataFramework.Pipelines.PlaceholderProcessors;

public class DataObjectInfoProcessor : IPlaceholder
{
    public Result<GenericFormattableString> Evaluate(string value, PlaceholderSettings settings, object? context, IFormattableStringParser formattableStringParser)
    {
        if (context is not ContextBase contextBase)
        {
            return Result.Continue<GenericFormattableString>();
        }

        var dataObjectInfo = contextBase.SourceModel;
        var entitiesNamespace = contextBase.Settings.DefaultEntityNamespace;
        return value switch
        {
            "EntityName" => Result.Success<GenericFormattableString>(dataObjectInfo.Name),
            "EntityFullName" => Result.Success<GenericFormattableString>(dataObjectInfo.GetEntityFullName(entitiesNamespace)),
            "DatabaseTableName" => Result.Success<GenericFormattableString>(dataObjectInfo.GetDatabaseTableName()),
            _ => Result.Continue<GenericFormattableString>()
        };
    }

    public Result Validate(string value, PlaceholderSettings settings, object? context, IFormattableStringParser formattableStringParser)
        => Result.Success();
}
