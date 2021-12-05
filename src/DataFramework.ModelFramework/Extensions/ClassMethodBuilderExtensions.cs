using System;
using System.Collections.Generic;
using System.Linq;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Extensions;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassMethodBuilderExtensions
    {
        internal static ClassMethodBuilder AddLiteralCodeStatements(this ClassMethodBuilder instance, params string[] statements)
            => instance.AddCodeStatements(statements.ToLiteralCodeStatementBuilders());

        internal static ClassMethodBuilder AddLiteralCodeStatements(this ClassMethodBuilder instance, IEnumerable<string> statements)
            => instance.AddLiteralCodeStatements(statements.ToArray());

        internal static ClassMethodBuilder AddParameter(this ClassMethodBuilder instance, string name, Type type)
            => instance.AddParameters(new ParameterBuilder().WithName(name).WithType(type));

        internal static ClassMethodBuilder AddParameter(this ClassMethodBuilder instance, string name, string typeName)
            => instance.AddParameters(new ParameterBuilder().WithName(name).WithTypeName(typeName));
    }
}
