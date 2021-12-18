using System;
using System.Collections.Generic;
using System.Linq;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Extensions;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassConstructorBuilderExtensions
    {
        internal static ClassConstructorBuilder AddLiteralCodeStatements(this ClassConstructorBuilder instance, params string[] statements)
            => instance.AddCodeStatements(statements.ToLiteralCodeStatementBuilders());

        internal static ClassConstructorBuilder AddLiteralCodeStatements(this ClassConstructorBuilder instance, IEnumerable<string> statements)
            => instance.AddLiteralCodeStatements(statements.ToArray());

        internal static ClassConstructorBuilder AddParameter(this ClassConstructorBuilder instance, string name, string typeName)
            => instance.AddParameters(new ParameterBuilder().WithName(name).WithTypeName(typeName));

        internal static ClassConstructorBuilder AddParameter(this ClassConstructorBuilder instance, string name, Type type)
            => instance.AddParameters(new ParameterBuilder().WithName(name).WithType(type));

        internal static ClassConstructorBuilder ChainCallToBaseUsingParameters(this ClassConstructorBuilder instance)
            => instance.WithChainCall($"base({string.Join(", ", instance.Parameters.Select(x => x.Name))})");
    }
}
