using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class FieldInfoExtensions
    {
        internal static string CreatePropertyName(this IFieldInfo instance, IDataObjectInfo dataObjectInfo)
            => instance.Name == dataObjectInfo.Name
                ? string.Format(dataObjectInfo.Metadata.GetStringValue(Entities.PropertyNameDeconflictionFormatString, "{0}Property"), instance.Name).Sanitize()
                : instance.Name.Sanitize();

        internal static bool IsRequired(this IFieldInfo instance)
            => instance.Metadata.GetValues<IAttribute>(Entities.FieldAttribute).Any(a => a.Name == "System.ComponentModel.DataAnnotations.Required");

        internal static int? GetStringMaxLength(this IFieldInfo instance)
        {
            var maxLengthAttribute = instance.Metadata.Where(md => md.Name == Entities.FieldAttribute)
                                                      .Select(md => md.Value)
                                                      .OfType<IAttribute>()
                                                      .FirstOrDefault(a => a.Name == "System.ComponentModel.DataAnnotations.MaxLength");

            var length = AttributeParameterFirstValue(maxLengthAttribute);
            if (length == null)
            {
                var stringLengthAttribute = instance.Metadata.Where(md => md.Name == Entities.FieldAttribute)
                                                             .Select(md => md.Value)
                                                             .OfType<IAttribute>()
                                                             .FirstOrDefault(a => a.Name == "System.ComponentModel.DataAnnotations.StringLength");
                length = AttributeParameterFirstValue(stringLengthAttribute);
            }

            return length;
        }

        private static int? AttributeParameterFirstValue(IAttribute attribute)
        {
            if (attribute == null)
            {
                return null;
            }

            if (attribute.Parameters.Count == 0)
            {
                return null;
            }

            if (attribute.Parameters.First().Value is int i)
            {
                return i;
            }

            return null;
        }

        internal static bool IsRowVersion(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.IsRowVersion);

        internal static bool IsSqlRequired(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.IsRequired, () => instance.IsNullable || instance.IsRequired());

        /// <summary>
        /// Indicates whether the field should be skipped on Find method, even though it is an identity field
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        internal static bool SkipFieldOnFind(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.SkipFieldOnFind);

        internal static ParameterBuilder ToParameterBuilder(this IFieldInfo instance)
            => new ParameterBuilder().WithName(instance.Name.Sanitize().ToPascalCase())
                                     .WithTypeName(instance.TypeName)
                                     .WithDefaultValue(instance.DefaultValue)
                                     .WithIsNullable(instance.IsNullable);

        internal static string GetSqlReaderMethodName(this IFieldInfo instance)
        {
            var metadataValue = instance.Metadata.GetStringValue(Database.SqlReaderMethodName);
            if (!string.IsNullOrEmpty(metadataValue))
            {
                return metadataValue;
            }

            if (instance.TypeName == null || instance.TypeName.Length == 0)
            {
                //assume object
                return "GetValue";
            }

            return instance.TypeName.GetSqlReaderMethodName(instance.IsNullable);
        }

        /// <summary>
        /// Indicates whether the field should be included in a Select (read) operation on database
        /// </summary>
        /// <remarks>Metadata value overrides IsPersistable, both True and False</remarks>
        /// <param name="instance"></param>
        /// <returns></returns>
        internal static bool IsSelectField(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.IsSelectField, instance.IsPersistable);

        internal static string GetDatabaseFieldName(this IFieldInfo instance)
            => instance.Metadata.GetStringValue(Database.FieldName, instance.Name);

        /// <summary>
        /// Gets field alias, or field name when not present
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        internal static string GetDatabaseFieldAlias(this IFieldInfo instance)
            => instance.Metadata.GetStringValue(Database.FieldAlias, () => instance.GetDatabaseFieldName());

        internal static bool IsSqlIdentity(this IFieldInfo instance)
            => instance.GetSqlFieldType().StartsWith("IDENTITY");

        internal static string GetSqlFieldType(this IFieldInfo instance,
                                               bool includeSpecificProperties = false)
        {
            var metadataValue = instance.Metadata.GetStringValue(Database.SqlFieldType);
            if (!string.IsNullOrEmpty(metadataValue))
            {
                return includeSpecificProperties
                    ? metadataValue
                    : RemoveSpecificPropertiesFromSqlType(metadataValue);
            }

            if (string.IsNullOrEmpty(instance.TypeName))
            {
                return string.Empty;
            }

            if (instance.TypeName == typeof(string).FullName || instance.TypeName == typeof(string).AssemblyQualifiedName)
            {
                return GetSqlVarcharType(instance, includeSpecificProperties, 32);
            }

            if (instance.TypeName == typeof(decimal).FullName
                || instance.TypeName == typeof(decimal).AssemblyQualifiedName
                || instance.TypeName == typeof(decimal?).FullName
                || instance.TypeName == typeof(decimal?).AssemblyQualifiedName)
            {
                return GetSqlDecimalType(instance, includeSpecificProperties);
            }

            if (instance.TypeName.IsRequiredEnum() || instance.TypeName.IsOptionalEnum())
            {
                return "int";
            }

            var type = Type.GetType(instance.TypeName);
            if (type != null && _sqlTypeNameMappings.TryGetValue(type, out var sqlType))
            {
                return sqlType;
            }

            return string.Empty;
        }

        internal static bool IsSqlStringMaxLength(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.IsMaxLength);

        internal static byte? GetSqlNumericPrecision(this IFieldInfo instance)
            => instance.Metadata.GetValue(Database.NumericPrecision, () => default(byte?));

        internal static byte? GetSqlNumericScale(this IFieldInfo instance)
            => instance.Metadata.GetValue(Database.NumericScale, () => default(byte?));

        internal static string GetSqlStringCollation(this IFieldInfo instance)
            => instance.Metadata.GetStringValue(Database.SqlStringCollation);

        internal static string GetCheckConstraintExpression(this IFieldInfo instance)
            => instance.Metadata.GetStringValue(Database.CheckConstraintExpression);

        /// <summary>
        /// Determines whether the specified field should be used on Insert in database
        /// </summary>
        /// <param name="instance"></param>
        /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
        internal static bool UseOnInsert(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.UseOnInsert, instance.IsPersistable && !instance.IsIdentityField && !instance.IsSqlIdentity() && !instance.IsComputed && instance.TypeName?.IsSupportedByMap() == true);

        /// <summary>
        /// Determines whether the specified field should be used on Update in database
        /// </summary>
        /// <param name="instance"></param>
        /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
        internal static bool UseOnUpdate(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.UseOnUpdate, instance.IsPersistable && !instance.IsIdentityField && !instance.IsSqlIdentity() && !instance.IsComputed && instance.TypeName?.IsSupportedByMap() == true);

        /// <summary>
        /// Determines whether the specified field should be used on Delete in database
        /// </summary>
        /// <param name="instance"></param>
        /// <remarks>Metadata value overrides IsPersistable/IsIdentityField/IsComputableField, both True and False</remarks>
        internal static bool UseOnDelete(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.UseOnDelete, instance.IsPersistable && !instance.IsIdentityField && !instance.IsSqlIdentity() && !instance.IsComputed && instance.TypeName?.IsSupportedByMap() == true);

        /// <summary>
        /// Determines whether the specified field should always be used on Select in database
        /// </summary>
        /// <remarks>Metadata value overrides IsPersistable, both True and False</remarks>
        /// <param name="instance"></param>
        internal static bool UseOnSelect(this IFieldInfo instance)
            => instance.Metadata.GetBooleanValue(Database.UseOnSelect, instance.IsPersistable);

        private static string GetSqlDecimalType(IFieldInfo instance, bool includeSpecificProperties)
            => includeSpecificProperties
                ? $"decimal({instance.GetSqlNumericPrecision() ?? 8},{instance.GetSqlNumericScale() ?? 0})"
                : "decimal";

        private static string GetSqlVarcharType(IFieldInfo instance, bool includeSpecificProperties, int defaultLength)
        {
            if (!includeSpecificProperties)
            {
                return "varchar";
            }
            var length = instance.IsSqlStringMaxLength()
                ? "max"
                : instance.GetSqlStringLength(defaultLength).ToString(CultureInfo.InvariantCulture);
            return $"varchar({length})";
        }

        private static int GetSqlStringLength(this IFieldInfo instance, int defaultLength)
            => instance.Metadata.GetValue(Database.SqlStringLength, () => instance.GetStringMaxLength() ?? defaultLength);

        private static string RemoveSpecificPropertiesFromSqlType(string sqlType)
            => sqlType.IndexOf("(") > -1
                ? sqlType.Substring(0, sqlType.IndexOf("("))
                : sqlType;

        private static Dictionary<Type, string> _sqlTypeNameMappings = new Dictionary<Type, string>
        {
            { typeof(int), "int" },
            { typeof(int?), "int" },
            { typeof(short), "smallint" },
            { typeof(short?), "smallint" },
            { typeof(long), "bigint" },
            { typeof(long?), "bigint" },
            { typeof(byte), "tinyint" },
            { typeof(byte?), "tinyint" },
            { typeof(bool), "bit" },
            { typeof(bool?), "bit" },
            { typeof(Guid), "uniqueidentifier" },
            { typeof(Guid?), "uniqueidentifier" },
            { typeof(double), "float" },
            { typeof(double?), "float" },
            { typeof(float), "real" },
            { typeof(float?), "real" },
            { typeof(DateTime), "datetime" },
            { typeof(DateTime?), "datetime" },
            { typeof(byte[]), "varbinary" },
        };
    }
}
