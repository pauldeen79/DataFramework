namespace DataFramework.CodeGeneration.Models.Pipelines;

internal interface IPipelineSettings
{
    ConcurrencyCheckBehavior ConcurrencyCheckBehavior { get; }
    EntityClassType EntityClassType { get; }
    [Required(AllowEmptyStrings = true)] string DefaultEntityNamespace { get; }
    [Required(AllowEmptyStrings = true)] string DefaultBuilderNamespace { get; }
    [DefaultValue(true)] bool AddComponentModelAttributes { get; }
    [Required] IReadOnlyCollection<ICodeStatementsMapping> CodeStatementMappings { get; }
    Visibility CommandEntityProviderVisibility { get; }
    [Required(AllowEmptyStrings = true)] string CommandEntityProviderNamespace { get; }
    [DefaultValue(true)] bool CommandProviderEnableAdd { get; }
    [DefaultValue(true)] bool CommandProviderEnableUpdate { get; }
    [DefaultValue(true)] bool CommandProviderEnableDelete { get; }
    [Required] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderAddResultEntityStatements { get; }
    [Required] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderAddAfterReadStatements { get; }
    [Required] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderUpdateResultEntityStatements { get; }
    [Required] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderUpdateAfterReadStatements { get; }
    [Required] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderDeleteResultEntityStatements { get; }
    [Required] IReadOnlyCollection<CodeStatementBase> CommandEntityProviderDeleteAfterReadStatements { get; }
}
