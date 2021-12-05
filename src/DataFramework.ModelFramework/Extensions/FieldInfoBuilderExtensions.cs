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

        public static FieldInfoBuilder WithMinLength(this FieldInfoBuilder instance, int length)
            => instance.AddMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.MinLength", length)
                .Build());

        public static FieldInfoBuilder WithMaxLength(this FieldInfoBuilder instance, int length)
            => instance.AddMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.MaxLength", length)
                .Build());

        public static FieldInfoBuilder WithRange(this FieldInfoBuilder instance, int minimumValue, int maximumValue)
            => instance.AddMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .WithName("System.ComponentModel.DataAnnotations.Range")
                .AddParameters(new AttributeParameterBuilder().WithValue(minimumValue),
                               new AttributeParameterBuilder().WithValue(maximumValue))
                .Build());

        public static FieldInfoBuilder WithIsRequired(this FieldInfoBuilder instance)
            => instance.AddMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .WithName("System.ComponentModel.DataAnnotations.Required").Build());

        public static FieldInfoBuilder WithRegularExpression(this FieldInfoBuilder instance, string pattern)
            => instance.AddMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.RegularExpression", pattern).Build());

        private static IAttribute CreateStringLengthAttribute(int maxLength, int? minimumLength)
        {
            var builder = new AttributeBuilder().AddNameAndParameter("System.ComponentModel.DataAnnotations.StringLength", maxLength);
            if (minimumLength != null)
            {
                builder.AddNameAndParameter("MinimumLength", minimumLength.Value);
            }

            return builder.Build();
        }
    }
}
