namespace DataFramework.ModelFramework.Extensions;

internal static class AttributeBuilderExtensions
{
    internal static AttributeBuilder ForCodeGenerator(this AttributeBuilder instance, string codeGeneratorName)
        => instance.ForCodeGenerator(codeGeneratorName, typeof(AttributeBuilderExtensions).Assembly.GetName().Version.ToString());

    internal static AttributeBuilder AddNameAndOptionalParameter(this AttributeBuilder instance, string name, object? value)
    {
        instance.WithName(name);

        if (value != null)
        {
            instance.AddParameters(new AttributeParameterBuilder().WithValue(value));
        }

        return instance;
    }
}
