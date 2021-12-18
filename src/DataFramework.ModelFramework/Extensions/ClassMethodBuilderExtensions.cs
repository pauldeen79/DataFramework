using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common.Extensions;
using CrossCutting.Data.Abstractions;
using DataFramework.Abstractions;
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

        public static ClassMethodBuilder AddCommandProviderMethod(this ClassMethodBuilder instance,
                                                                  IDataObjectInfo dataObjectInfo,
                                                                  string preventMetadataName,
                                                                  DatabaseOperation operation,
                                                                  string commandType,
                                                                  string commentText)
            => instance.Chain(builder =>
            {
                if (!dataObjectInfo.Metadata.GetBooleanValue(preventMetadataName))
                {
                    instance.AddLiteralCodeStatements
                    (
                        $"    case {typeof(DatabaseOperation).FullName}.{operation}:",
                        $"        return new {commandType}(\"{commentText}\", source, {typeof(DatabaseOperation).FullName}.{operation}, {operation.GetMethodNamePrefix()}Parameters);"
                    );
                }
            });
    }
}
