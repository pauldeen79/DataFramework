using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static class FieldInfoBuilderExtensions
    {
        public static FieldInfoBuilder WithStringLength(this FieldInfoBuilder instance, int maxLength, int? minimumLength = null)
            => instance.AddMetadata(Entities.EntitiesAttribute, CreateStringLengthAttribute(maxLength, minimumLength));

        public static FieldInfoBuilder WithIsRequired(this FieldInfoBuilder instance)
            => instance.AddMetadata(Entities.EntitiesAttribute, new AttributeBuilder().WithName("System.ComponentModel.DataAnnotations.Required").Build());

        private static IAttribute CreateStringLengthAttribute(int maxLength, int? minimumLength)
        {
            var builder = new AttributeBuilder().WithName("System.ComponentModel.DataAnnotations.StringLength").AddParameters(new AttributeParameterBuilder().WithValue(maxLength));
            if (minimumLength != null)
            {
                builder.AddParameters(new AttributeParameterBuilder().WithName("MinimumLength").WithValue(minimumLength.Value).Build());
            }
                    
            return builder.Build();
        }
    }
}
