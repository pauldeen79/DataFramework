﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 6.0.2
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFramework.Core
{
#nullable enable
    public partial record FieldInfo : DataFramework.Abstractions.IFieldInfo
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

        public CrossCutting.Common.ValueCollection<DataFramework.Abstractions.IMetadata> Metadata
        {
            get;
        }

        public FieldInfo(string name, string? description, string? displayName, string? typeName, bool isNullable, bool isVisible, bool isReadOnly, bool isIdentityField, bool isComputed, bool isPersistable, bool canGet, bool canSet, bool useForConcurrencyCheck, object? defaultValue, System.Collections.Generic.IEnumerable<DataFramework.Abstractions.IMetadata> metadata)
        {
            this.Name = name;
            this.Description = description;
            this.DisplayName = displayName;
            this.TypeName = typeName;
            this.IsNullable = isNullable;
            this.IsVisible = isVisible;
            this.IsReadOnly = isReadOnly;
            this.IsIdentityField = isIdentityField;
            this.IsComputed = isComputed;
            this.IsPersistable = isPersistable;
            this.CanGet = canGet;
            this.CanSet = canSet;
            this.UseForConcurrencyCheck = useForConcurrencyCheck;
            this.DefaultValue = defaultValue;
            this.Metadata = new CrossCutting.Common.ValueCollection<DataFramework.Abstractions.IMetadata>(metadata);
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}
