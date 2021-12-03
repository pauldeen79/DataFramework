using System.Collections.Generic;
using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class CollectionOfAttributeBuilderExtensions
    {
        internal static void AddConditionalModelClassAttribute(this ICollection<AttributeBuilder> attributesList, string name, object? value, bool condition = true)
        {
            if (!condition)
            {
                return;
            }
            attributesList.Add(new AttributeBuilder().WithName(name).AddParameters(new AttributeParameterBuilder().WithValue(value)));
        }

        internal static void AddModelClassAttribute(this ICollection<AttributeBuilder> attributesList, string name, object? value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return;
            }
            attributesList.Add(new AttributeBuilder().WithName(name).AddParameters(new AttributeParameterBuilder().WithValue(value)));
        }
    }
}
