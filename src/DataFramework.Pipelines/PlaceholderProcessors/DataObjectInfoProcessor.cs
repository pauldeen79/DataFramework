namespace DataFramework.Pipelines.PlaceholderProcessors;

public class DataObjectInfoProcessor : IPlaceholderProcessor
{
    public int Order => 30;

    public Result<FormattableStringParserResult> Process(string value, IFormatProvider formatProvider, object? context, IFormattableStringParser formattableStringParser)
    {
        if (context is not ContextBase contextBase)
        {
            return Result.Continue<FormattableStringParserResult>();
        }

        var dataObjectInfo = contextBase.SourceModel;
        var entitiesNamespace = contextBase.Settings.DefaultEntityNamespace;
        return value switch
        {
            "EntityName" => Result.Success<FormattableStringParserResult>(dataObjectInfo.Name),
            "EntityFullName" => Result.Success<FormattableStringParserResult>(dataObjectInfo.GetEntityFullName(entitiesNamespace)),
            "DatabaseTableName" => Result.Success<FormattableStringParserResult>(dataObjectInfo.GetDatabaseTableName()),
            _ => Result.Continue<FormattableStringParserResult>()
        };
    }
}
