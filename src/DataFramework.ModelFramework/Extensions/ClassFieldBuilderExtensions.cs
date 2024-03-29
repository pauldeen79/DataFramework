﻿namespace DataFramework.ModelFramework.Extensions;

internal static class ClassFieldBuilderExtensions
{
    internal static ClassFieldBuilder FillFrom(this ClassFieldBuilder instance, IFieldInfo fieldInfo)
        => instance.WithName($"_{fieldInfo.Name.ToPascalCase()}")
            .WithTypeName(fieldInfo.GetPropertyTypeName().FixTypeName())
            .WithIsNullable(fieldInfo.IsNullable);
}
