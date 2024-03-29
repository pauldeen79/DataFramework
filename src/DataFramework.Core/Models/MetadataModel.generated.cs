﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.3
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
    public partial class MetadataModel : System.ComponentModel.DataAnnotations.IValidatableObject
    {
        public string Name
        {
            get;
            set;
        }

        public object? Value
        {
            get;
            set;
        }

        public DataFramework.Abstractions.IMetadata ToEntity()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            return new DataFramework.Core.Metadata(Name, Value);
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var instance = new DataFramework.Core.MetadataBase(Name, Value);
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning restore CS8604 // Possible null reference argument.
            var results = new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>();
            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(instance, new System.ComponentModel.DataAnnotations.ValidationContext(instance), results, true);
            return results;
        }

        public MetadataModel()
        {
            Name = string.Empty;
        }

        public MetadataModel(DataFramework.Abstractions.IMetadata source)
        {
            Name = source.Name;
            Value = source.Value;
        }
    }
#nullable restore
}

