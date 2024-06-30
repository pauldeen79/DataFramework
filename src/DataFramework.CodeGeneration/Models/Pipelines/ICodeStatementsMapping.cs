namespace DataFramework.CodeGeneration.Models.Pipelines;

internal interface ICodeStatementsMapping
{
    [Required] IDataObjectInfo SourceDataObjectInfo { get; }
    [Required] IFieldInfo SourceFieldInfo { get; }
    [Required] IReadOnlyCollection<CodeStatementBaseBuilder> CodeStatements { get; }
}
