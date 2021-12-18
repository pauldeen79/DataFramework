﻿using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Extensions;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassPropertyBuilderExtensions
    {
        internal static ClassPropertyBuilder Fill(this ClassPropertyBuilder instance, IFieldInfo field)
            => instance
                .WithTypeName(field.Metadata.GetStringValue(Entities.PropertyType, field.TypeName ?? "System.Object"))
                .WithIsNullable(field.IsNullable)
                .WithVisibility(field.Metadata.GetValue(Entities.Visibility, () => field.IsVisible.ToVisibility()))
                .WithGetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, () => field.IsVisible.ToVisibility()))
                .WithSetterVisibility(field.Metadata.GetValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, () => field.IsVisible.ToVisibility()))
                .AddMetadata(field.Metadata.Convert());

        internal static ClassPropertyBuilder AddGetterLiteralCodeStatements(this ClassPropertyBuilder instance, params string[] statements)
            => instance.AddGetterCodeStatements(statements.ToLiteralCodeStatementBuilders());

        internal static ClassPropertyBuilder AddGetterLiteralCodeStatements(this ClassPropertyBuilder instance, IEnumerable<string> statements)
            => instance.AddGetterLiteralCodeStatements(statements.ToArray());
    }
}
