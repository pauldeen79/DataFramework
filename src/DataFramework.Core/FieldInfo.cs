﻿using System;
using CrossCutting.Common;
using DataFramework.Abstractions;

namespace DataFramework.Core
{
    public record FieldInfo : IFieldInfo
    {
        public string Name
        {
            get;
        }

        public string? Description
        {
            get;
        }

        public string? DisplayName
        {
            get;
        }

        public string? TypeName
        {
            get;
        }

        public bool IsNullable
        {
            get;
        }

        public bool IsVisible
        {
            get;
        }

        public bool IsReadOnly
        {
            get;
        }

        public bool IsIdentityField
        {
            get;
        }

        public bool IsComputed
        {
            get;
        }

        public bool IsPersistable
        {
            get;
        }

        public bool CanGet
        {
            get;
        }

        public bool CanSet
        {
            get;
        }

        public bool UseForConcurrencyCheck
        {
            get;
        }

        public object? DefaultValue
        {
            get;
        }

        public ValueCollection<IMetadata> Metadata
        {
            get;
        }

#pragma warning disable S107 // Methods should not have too many parameters
        public FieldInfo(string name,
                         string? typeName,
                         string? description,
                         string? displayName,
                         bool isNullable,
                         bool isVisible,
                         bool isReadOnly,
                         bool isIdentityField,
                         bool isComputed,
                         bool isPersistable,
                         bool canGet,
                         bool canSet,
                         bool useForConcurrencyCheck,
                         object? defaultValue,
                         ValueCollection<IMetadata> metadata)
#pragma warning restore S107 // Methods should not have too many parameters
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Description = description;
            DisplayName = displayName;
            TypeName = typeName;
            IsNullable = isNullable;
            IsVisible = isVisible;
            IsReadOnly = isReadOnly;
            IsIdentityField = isIdentityField;
            IsComputed = isComputed;
            IsPersistable = isPersistable;
            CanGet = canGet;
            CanSet = canSet;
            UseForConcurrencyCheck = useForConcurrencyCheck;
            DefaultValue = defaultValue;
            Metadata = metadata;
        }

        public override string ToString() => Name;
    }
}
