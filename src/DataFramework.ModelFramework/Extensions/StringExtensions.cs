using System;
using System.Collections.Generic;
using ModelFramework.Common.Extensions;

namespace DataFramework.ModelFramework.Extensions
{
    public static class StringExtensions
    {
        public static string FixGenericParameter(this string value, string typeName)
            => value.Replace("<T>", string.Format("<{0}>", typeName));

        public static bool IsSupportedByMap(this string instance)
        {
            if (instance.IsRequiredEnum()) return true;
            if (instance.IsOptionalEnum()) return true;
            var type = Type.GetType(instance);
            return type != null && _readerMethodNames.ContainsKey(type);
        }

        private static Dictionary<Type, string> _readerMethodNames = new Dictionary<Type, string>
        {
            { typeof(string), "GetString" },
            { typeof(int), "GetInt32" },
            { typeof(int?), "GetNullableInt32" },
            { typeof(short), "GetInt16" },
            { typeof(short?), "GetNullableInt16" },
            { typeof(long), "GetInt64" },
            { typeof(long?), "GetNullableInt64" },
            { typeof(byte), "GetByte" },
            { typeof(byte?), "GetNullableByte" },
            { typeof(bool), "GetBoolean" },
            { typeof(bool?), "GetNullableBoolean" },
            { typeof(Guid), "GetGuid" },
            { typeof(Guid?), "GetNullableGuid" },
            { typeof(double), "GetDouble" },
            { typeof(double?), "GetNullableDouble" },
            { typeof(float), "GetFloat" },
            { typeof(float?), "GetNullableFloat" },
            { typeof(decimal), "GetDecimal" },
            { typeof(decimal?), "GetNullableDecimal" },
            { typeof(DateTime), "GetDateTime" },
            { typeof(DateTime?), "GetNullableDateTime" },
            { typeof(byte[]), "GetByteArray" },
        };

        public static string GetSqlReaderMethodName(this string instance, bool isNullable)
        {
            if (instance.IsRequiredEnum() && !isNullable)
            {
                // assumption: enum is int32.
                return "GetInt32";
            }

            if (instance.IsOptionalEnum() || (instance.IsRequiredEnum() && isNullable))
            {
                // assumption: enum is int32.
                return "GetNullableInt32";
            }

            var type = Type.GetType(instance);
            if (type == null)
            {
                return "GetValue";
            }
            if (!_readerMethodNames.TryGetValue(type, out var name))
            {
                return "GetValue";
            }

            if (isNullable)
            {
                return name.Replace("Get", "GetNullable");
            }

            return name;
        }
    }
}
