﻿namespace DataFramework.Domain.Builders;

public partial class FieldInfoBuilder
{
    public FieldInfoBuilder WithType(Type type)
        => WithTypeName(type?.AssemblyQualifiedName!);
}