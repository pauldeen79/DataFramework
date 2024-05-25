namespace DataFramework.CodeGeneration.Models.Pipelines;

internal interface IPipelineSettings
{
    ConcurrencyCheckBehavior ConcurrencyCheckBehavior { get; }
    EntityClassType EntityClassType { get; }
    [Required(AllowEmptyStrings = true)] string DefaultEntityNamespace { get; }
    [DefaultValue(true)] bool AddComponentModelAttributes { get; }
}
