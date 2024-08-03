namespace DataFramework.Core;

public partial record FieldInfo : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return new ValidationResult("Name cannot be null or whitespace", [nameof(Name)]);
        }
    }

    public override string ToString() => Name;
}
