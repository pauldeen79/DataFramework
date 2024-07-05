namespace DataFramework.Pipelines.PlaceholderProcessors;

public class DataObjectInfoProcessor : IPlaceholderProcessor
{
    public int Order => 30;

    public Result<FormattableStringParserResult> Process(string value, IFormatProvider formatProvider, object? context, IFormattableStringParser formattableStringParser)
    {
        if (context is not DataObjectInfo dataObjectInfo)
        {
            return Result.Continue<FormattableStringParserResult>();
        }

        return value switch
        {
            "DatabaseTableName" => Result.Success<FormattableStringParserResult>(dataObjectInfo.GetDatabaseTableName()),
            _ => Result.Continue<FormattableStringParserResult>()
        };
    }
}
