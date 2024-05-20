namespace DataFramework.CodeGeneration.Models.Pipelines;

internal interface IPipelineSettings
{
    ConcurrencyCheckBehavior ConcurrencyCheckBehavior { get; }
    EntityClassType EntityClassType { get; }
}
