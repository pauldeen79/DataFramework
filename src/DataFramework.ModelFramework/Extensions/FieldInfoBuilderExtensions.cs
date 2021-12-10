using CrossCutting.Common.Extensions;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static class FieldInfoBuilderExtensions
    {
        public static FieldInfoBuilder WithStringLength(this FieldInfoBuilder instance, int maxLength, int? minimumLength = null)
            => instance.ReplaceMetadata(Entities.EntitiesAttribute, CreateStringLengthAttribute(maxLength, minimumLength));

        public static FieldInfoBuilder WithMinLength(this FieldInfoBuilder instance, int length)
            => instance.ReplaceMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.MinLength", length)
                .Build());

        public static FieldInfoBuilder WithMaxLength(this FieldInfoBuilder instance, int length)
            => instance.ReplaceMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.MaxLength", length)
                .Build());

        public static FieldInfoBuilder WithRange(this FieldInfoBuilder instance, int minimumValue, int maximumValue)
            => instance.ReplaceMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .WithName("System.ComponentModel.DataAnnotations.Range")
                .AddParameters(new AttributeParameterBuilder().WithValue(minimumValue),
                               new AttributeParameterBuilder().WithValue(maximumValue))
                .Build());

        public static FieldInfoBuilder WithIsRequired(this FieldInfoBuilder instance)
            => instance.ReplaceMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .WithName("System.ComponentModel.DataAnnotations.Required").Build());

        public static FieldInfoBuilder WithRegularExpression(this FieldInfoBuilder instance, string pattern)
            => instance.ReplaceMetadata(Entities.EntitiesAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.RegularExpression", pattern).Build());

        public static FieldInfoBuilder WithIsRowVersion(this FieldInfoBuilder instance, bool? isRowVersion)
            => instance.ReplaceMetadata(Database.IsRowVersion, isRowVersion);

        public static FieldInfoBuilder WithIsSqlRequired(this FieldInfoBuilder instance, bool? isSqlRequired)
            => instance.ReplaceMetadata(Database.IsRequired, isSqlRequired);

        public static FieldInfoBuilder WithSkipFieldOnFind(this FieldInfoBuilder instance, bool? skipFieldOnFind)
            => instance.ReplaceMetadata(Database.SkipFieldOnFind, skipFieldOnFind);

        public static FieldInfoBuilder WithSqlReaderMethodName(this FieldInfoBuilder instance, string? sqlReaderMethodName)
            => instance.ReplaceMetadata(Database.SqlReaderMethodName, sqlReaderMethodName);

        public static FieldInfoBuilder WithIsSelectField(this FieldInfoBuilder instance, bool? isSelectField)
            => instance.ReplaceMetadata(Database.IsSelectField, isSelectField);

        public static FieldInfoBuilder WithDatabaseFieldName(this FieldInfoBuilder instance, string? fieldName)
            => instance.ReplaceMetadata(Database.FieldName, fieldName);

        public static FieldInfoBuilder WithDatabaseFieldAlias(this FieldInfoBuilder instance, string? fieldAlias)
            => instance.ReplaceMetadata(Database.FieldAlias, fieldAlias);

        public static FieldInfoBuilder WithSqlFieldType(this FieldInfoBuilder instance, string? sqlFieldType)
            => instance.ReplaceMetadata(Database.SqlFieldType, sqlFieldType);

        public static FieldInfoBuilder WithSqlIsStringMaxLength(this FieldInfoBuilder instance, bool? isMaxLength)
            => instance.ReplaceMetadata(Database.IsMaxLength, isMaxLength);

        public static FieldInfoBuilder WithSqlNumericPrecision(this FieldInfoBuilder instance, byte? precision)
            => instance.ReplaceMetadata(Database.NumericPrecision, precision);

        public static FieldInfoBuilder WithSqlNumericScale(this FieldInfoBuilder instance, byte? scale)
            => instance.ReplaceMetadata(Database.NumericScale, scale);

        public static FieldInfoBuilder WithSqlStringCollation(this FieldInfoBuilder instance, string? collation)
            => instance.ReplaceMetadata(Database.SqlStringCollation, collation);

        public static FieldInfoBuilder WithCheckConstraintExpression(this FieldInfoBuilder instance, string? checkConstraintExpression)
            => instance.ReplaceMetadata(Database.CheckConstraintExpression, checkConstraintExpression);

        public static FieldInfoBuilder WithUseOnInsert(this FieldInfoBuilder instance, bool? useOnInsert)
            => instance.ReplaceMetadata(Database.UseOnInsert, useOnInsert);

        public static FieldInfoBuilder WithUseOnUpdate(this FieldInfoBuilder instance, bool? useOnUpdate)
            => instance.ReplaceMetadata(Database.UseOnUpdate, useOnUpdate);

        public static FieldInfoBuilder WithUseOnSelect(this FieldInfoBuilder instance, bool? useOnSelect)
            => instance.ReplaceMetadata(Database.UseOnSelect, useOnSelect);

        private static IAttribute CreateStringLengthAttribute(int maxLength, int? minimumLength)
        {
            var builder = new AttributeBuilder().AddNameAndParameter("System.ComponentModel.DataAnnotations.StringLength", maxLength);
            if (minimumLength != null)
            {
                builder.AddNameAndParameter("MinimumLength", minimumLength.Value);
            }

            return builder.Build();
        }

        private static FieldInfoBuilder ReplaceMetadata(this FieldInfoBuilder instance, string name, object? newValue)
            => instance.Chain(() =>
            {
                instance.Metadata.Replace(name, newValue);
            });
    }
}
