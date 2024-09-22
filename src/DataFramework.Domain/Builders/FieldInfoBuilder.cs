namespace DataFramework.Domain.Builders;

public partial class FieldInfoBuilder
{
    public FieldInfoBuilder WithType(Type type) //TODO: Detect nullability automatically
        => WithTypeName(type.IsNotNull(nameof(type)).AssemblyQualifiedName).WithIsValueType(type.IsValueType);
}
