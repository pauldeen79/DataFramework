﻿namespace DataFramework.CodeGeneration.Models.Pipelines;

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
}
