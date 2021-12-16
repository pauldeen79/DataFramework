using System;
using System.Collections.Generic;
using System.Linq;
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
            => instance.ReplaceMetadata(Entities.FieldAttribute, CreateStringLengthAttribute(maxLength, minimumLength));

        public static FieldInfoBuilder WithMinLength(this FieldInfoBuilder instance, int length)
            => instance.ReplaceMetadata(Entities.FieldAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.MinLength", length)
                .Build());

        public static FieldInfoBuilder WithMaxLength(this FieldInfoBuilder instance, int length)
            => instance.ReplaceMetadata(Entities.FieldAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.MaxLength", length)
                .Build());

        public static FieldInfoBuilder WithRange(this FieldInfoBuilder instance, int minimumValue, int maximumValue)
            => instance.ReplaceMetadata(Entities.FieldAttribute, new AttributeBuilder()
                .WithName("System.ComponentModel.DataAnnotations.Range")
                .AddParameters(new AttributeParameterBuilder().WithValue(minimumValue),
                               new AttributeParameterBuilder().WithValue(maximumValue))
                .Build());

        public static FieldInfoBuilder WithIsRequired(this FieldInfoBuilder instance, bool required = true)
            => instance.ReplaceMetadata(Entities.FieldAttribute, required
                ? new AttributeBuilder().WithName("System.ComponentModel.DataAnnotations.Required").Build()
                : null);

        public static FieldInfoBuilder WithRegularExpression(this FieldInfoBuilder instance, string pattern)
            => instance.ReplaceMetadata(Entities.FieldAttribute, new AttributeBuilder()
                .AddNameAndParameter("System.ComponentModel.DataAnnotations.RegularExpression", pattern).Build());

        public static FieldInfoBuilder WithIsRowVersion(this FieldInfoBuilder instance, bool? isRowVersion = true)
            => instance.ReplaceMetadata(Database.IsRowVersion, isRowVersion);

        public static FieldInfoBuilder WithIsSqlRequired(this FieldInfoBuilder instance, bool? isSqlRequired = true)
            => instance.ReplaceMetadata(Database.IsRequired, isSqlRequired);

        public static FieldInfoBuilder WithSkipFieldOnFind(this FieldInfoBuilder instance, bool? skipFieldOnFind = true)
            => instance.ReplaceMetadata(Database.SkipFieldOnFind, skipFieldOnFind);

        public static FieldInfoBuilder WithSqlReaderMethodName(this FieldInfoBuilder instance, string? sqlReaderMethodName)
            => instance.ReplaceMetadata(Database.SqlReaderMethodName, sqlReaderMethodName);

        public static FieldInfoBuilder WithIsSelectField(this FieldInfoBuilder instance, bool? isSelectField = true)
            => instance.ReplaceMetadata(Database.IsSelectField, isSelectField);

        public static FieldInfoBuilder WithDatabaseFieldName(this FieldInfoBuilder instance, string? fieldName)
            => instance.ReplaceMetadata(Database.FieldName, fieldName);

        public static FieldInfoBuilder WithDatabaseFieldAlias(this FieldInfoBuilder instance, string? fieldAlias)
            => instance.ReplaceMetadata(Database.FieldAlias, fieldAlias);

        public static FieldInfoBuilder WithSqlFieldType(this FieldInfoBuilder instance, string? sqlFieldType)
            => instance.ReplaceMetadata(Database.SqlFieldType, sqlFieldType);

        public static FieldInfoBuilder WithSqlStringLength(this FieldInfoBuilder instance, int? length)
            => instance.ReplaceMetadata(Database.SqlStringLength, length);

        public static FieldInfoBuilder WithSqlIsStringMaxLength(this FieldInfoBuilder instance, bool? isMaxLength = true)
            => instance.ReplaceMetadata(Database.IsMaxLength, isMaxLength);

        public static FieldInfoBuilder WithSqlNumericPrecision(this FieldInfoBuilder instance, byte? precision)
            => instance.ReplaceMetadata(Database.NumericPrecision, precision);

        public static FieldInfoBuilder WithSqlNumericScale(this FieldInfoBuilder instance, byte? scale)
            => instance.ReplaceMetadata(Database.NumericScale, scale);

        public static FieldInfoBuilder WithSqlStringCollation(this FieldInfoBuilder instance, string? collation)
            => instance.ReplaceMetadata(Database.SqlStringCollation, collation);

        public static FieldInfoBuilder WithCheckConstraintExpression(this FieldInfoBuilder instance, string? checkConstraintExpression)
            => instance.ReplaceMetadata(Database.CheckConstraintExpression, checkConstraintExpression);

        public static FieldInfoBuilder WithUseOnInsert(this FieldInfoBuilder instance, bool? useOnInsert = true)
            => instance.ReplaceMetadata(Database.UseOnInsert, useOnInsert);

        public static FieldInfoBuilder WithUseOnUpdate(this FieldInfoBuilder instance, bool? useOnUpdate = true)
            => instance.ReplaceMetadata(Database.UseOnUpdate, useOnUpdate);

        public static FieldInfoBuilder WithUseOnDelete(this FieldInfoBuilder instance, bool? useOnDelete = true)
            => instance.ReplaceMetadata(Database.UseOnDelete, useOnDelete);

        public static FieldInfoBuilder WithUseOnSelect(this FieldInfoBuilder instance, bool? useOnSelect = true)
            => instance.ReplaceMetadata(Database.UseOnSelect, useOnSelect);

        public static FieldInfoBuilder WithPropertyType(this FieldInfoBuilder instance, Type? propertyType = null)
            => instance.ReplaceMetadata(Entities.PropertyType, propertyType?.FullName);

        public static FieldInfoBuilder WithPropertyTypeName(this FieldInfoBuilder instance, string? propertyTypeName = null)
            => instance.ReplaceMetadata(Entities.PropertyType, propertyTypeName);

        public static FieldInfoBuilder AddComputedFieldStatements(this FieldInfoBuilder instance, params ICodeStatement[] codeStatements)
            => instance.AddMetadata(codeStatements.Select(x => new MetadataBuilder().WithName(Entities.ComputedFieldStatement).WithValue(x)));

        public static FieldInfoBuilder AddComputedFieldStatements(this FieldInfoBuilder instance, IEnumerable<ICodeStatement> codeStatements)
            => instance.AddComputedFieldStatements(codeStatements.ToArray());

        public static FieldInfoBuilder AddComputedFieldStatements(this FieldInfoBuilder instance, params ICodeStatementBuilder[] codeStatementBuilders)
            => instance.AddMetadata(codeStatementBuilders.Select(x => new MetadataBuilder().WithName(Entities.ComputedFieldStatement).WithValue(x.Build())));

        public static FieldInfoBuilder AddComputedFieldStatements(this FieldInfoBuilder instance, IEnumerable<ICodeStatementBuilder> codeStatementBuilders)
            => instance.AddComputedFieldStatements(codeStatementBuilders.ToArray());

        public static FieldInfoBuilder AddAttributes(this FieldInfoBuilder instance, params IAttribute[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Entities.FieldAttribute).WithValue(x)));

        public static FieldInfoBuilder AddAttributes(this FieldInfoBuilder instance, IEnumerable<IAttribute> attributes)
            => instance.AddAttributes(attributes.ToArray());

        public static FieldInfoBuilder AddAttributes(this FieldInfoBuilder instance, params AttributeBuilder[] attributes)
            => instance.AddMetadata(attributes.Select(x => new MetadataBuilder().WithName(Entities.FieldAttribute).WithValue(x.Build())));

        public static FieldInfoBuilder AddAttributes(this FieldInfoBuilder instance, IEnumerable<AttributeBuilder> attributes)
            => instance.AddAttributes(attributes.ToArray());

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
