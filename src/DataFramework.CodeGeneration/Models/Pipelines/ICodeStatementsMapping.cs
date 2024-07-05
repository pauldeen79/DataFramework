namespace DataFramework.CodeGeneration.Models.Pipelines;

internal interface ICodeStatementsMapping
{
    [Required] IDataObjectInfo SourceDataObjectInfo { get; }
    [Required] IFieldInfo SourceFieldInfo { get; }
    [Required][ValidateObject] IReadOnlyCollection<CodeStatementBase> CodeStatements { get; }
}
