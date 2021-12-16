using System;
using ModelFramework.Common.Extensions;

namespace DataFramework.ModelFramework.Extensions
{
    public static class TypeExtensions
    {
        public static string CreateGenericTypeName(this Type instance, string genericTypeConstraintName)
            => instance.MakeGenericType(typeof(object)).FullName.Replace("<System.Object>", $"<{genericTypeConstraintName}>").FixTypeName();
    }
}
