namespace DataFramework.CodeGeneration.Models.Pipelines;

internal interface IPipelineSettings
{
    ConcurrencyCheckBehavior ConcurrencyCheckBehavior { get; }
    EntityClassType EntityClassType { get; }
    [Required(AllowEmptyStrings = true)] string DefaultEntityNamespace { get; }
    [Required(AllowEmptyStrings = true)] string DefaultBuilderNamespace { get; }
    [DefaultValue(true)] bool AddComponentModelAttributes { get; }
    [DefaultValue(true)] bool AddValidationCodeInConstructor { get; }
    [DefaultValue(true)] bool AddToBuilderMethod { get; }
}
