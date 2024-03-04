﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.2
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFramework.Core.Models
{
#nullable enable
    public partial class FieldInfoModel : System.ComponentModel.DataAnnotations.IValidatableObject
    {
        public string Name
        {
            get;
            set;
        }

        public string? Description
        {
            get;
            set;
        }

        public string? DisplayName
        {
            get;
            set;
        }

        public string? TypeName
        {
            get;
            set;
        }

        public bool IsNullable
        {
            get;
            set;
        }

        public bool IsVisible
        {
            get;
            set;
        }

        public bool IsReadOnly
        {
            get;
            set;
        }

        public bool IsIdentityField
        {
            get;
            set;
        }

        public bool IsComputed
        {
            get;
            set;
        }

        public bool IsPersistable
        {
            get;
            set;
        }

        public bool CanGet
        {
            get;
            set;
        }

        public bool CanSet
        {
            get;
            set;
        }

        public bool UseForConcurrencyCheck
        {
            get;
            set;
        }

        public object? DefaultValue
        {
            get;
            set;
        }

        public System.Collections.Generic.List<DataFramework.Core.Models.MetadataModel> Metadata
        {
            get;
            set;
        }

        public DataFramework.Abstractions.IFieldInfo ToEntity()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            return new DataFramework.Core.FieldInfo(Name, Description, DisplayName, TypeName, IsNullable, IsVisible, IsReadOnly, IsIdentityField, IsComputed, IsPersistable, CanGet, CanSet, UseForConcurrencyCheck, DefaultValue, Metadata.Select(x => x.ToEntity()));
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var instance = new DataFramework.Core.FieldInfoBase(Name, Description, DisplayName, TypeName, IsNullable, IsVisible, IsReadOnly, IsIdentityField, IsComputed, IsPersistable, CanGet, CanSet, UseForConcurrencyCheck, DefaultValue, Metadata.Select(x => x.ToEntity()));
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning restore CS8604 // Possible null reference argument.
            var results = new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>();
            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(instance, new System.ComponentModel.DataAnnotations.ValidationContext(instance), results, true);
            return results;
        }

        public FieldInfoModel()
        {
            Metadata = new System.Collections.Generic.List<DataFramework.Core.Models.MetadataModel>();
            Name = string.Empty;
        }

        public FieldInfoModel(DataFramework.Abstractions.IFieldInfo source)
        {
            Metadata = new System.Collections.Generic.List<DataFramework.Core.Models.MetadataModel>();
            Name = source.Name;
            Description = source.Description;
            DisplayName = source.DisplayName;
            TypeName = source.TypeName;
            IsNullable = source.IsNullable;
            IsVisible = source.IsVisible;
            IsReadOnly = source.IsReadOnly;
            IsIdentityField = source.IsIdentityField;
            IsComputed = source.IsComputed;
            IsPersistable = source.IsPersistable;
            CanGet = source.CanGet;
            CanSet = source.CanSet;
            UseForConcurrencyCheck = source.UseForConcurrencyCheck;
            DefaultValue = source.DefaultValue;
            Metadata.AddRange(source.Metadata.Select(x => new DataFramework.Core.Models.MetadataModel(x)));
        }
    }
#nullable restore
}

