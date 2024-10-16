namespace DataFramework.CodeGeneration.Models;

public interface IEntityMapping
{
    [Required] string PropertyName { get; }
    [Required] object Mapping { get; }
}
