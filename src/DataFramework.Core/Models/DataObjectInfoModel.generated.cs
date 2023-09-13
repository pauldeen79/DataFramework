﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 7.0.11
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
    public partial class DataObjectInfoModel : System.ComponentModel.DataAnnotations.IValidatableObject
    {
        public System.Collections.Generic.List<DataFramework.Core.Models.FieldInfoModel> Fields
        {
            get;
            set;
        }

        public string? AssemblyName
        {
            get;
            set;
        }

        public string? TypeName
        {
            get;
            set;
        }

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

        public bool IsQueryable
        {
            get;
            set;
        }

        public System.Collections.Generic.List<DataFramework.Core.Models.MetadataModel> Metadata
        {
            get;
            set;
        }

        public DataFramework.Abstractions.IDataObjectInfo ToEntity()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            return new DataFramework.Core.DataObjectInfo(Fields.Select(x => x.ToEntity()), AssemblyName, TypeName, Name, Description, DisplayName, IsVisible, IsReadOnly, IsQueryable, Metadata.Select(x => x.ToEntity()));
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var instance = new DataFramework.Core.DataObjectInfoBase(Fields.Select(x => x.ToEntity()), AssemblyName, TypeName, Name, Description, DisplayName, IsVisible, IsReadOnly, IsQueryable, Metadata.Select(x => x.ToEntity()));
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning restore CS8604 // Possible null reference argument.
            var results = new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>();
            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(instance, new System.ComponentModel.DataAnnotations.ValidationContext(instance, null, null), results, true);
            return results;
        }

        public DataObjectInfoModel()
        {
            Fields = new System.Collections.Generic.List<DataFramework.Core.Models.FieldInfoModel>();
            Metadata = new System.Collections.Generic.List<DataFramework.Core.Models.MetadataModel>();
            #pragma warning disable CS8603 // Possible null reference return.
            Name = string.Empty;
            IsVisible = default(System.Boolean)!;
            IsReadOnly = default(System.Boolean)!;
            IsQueryable = default(System.Boolean)!;
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public DataObjectInfoModel(DataFramework.Abstractions.IDataObjectInfo source)
        {
            Fields = new System.Collections.Generic.List<DataFramework.Core.Models.FieldInfoModel>();
            Metadata = new System.Collections.Generic.List<DataFramework.Core.Models.MetadataModel>();
            Fields.AddRange(source.Fields.Select(x => new DataFramework.Core.Models.FieldInfoModel(x)));
            AssemblyName = source.AssemblyName;
            TypeName = source.TypeName;
            Name = source.Name;
            Description = source.Description;
            DisplayName = source.DisplayName;
            IsVisible = source.IsVisible;
            IsReadOnly = source.IsReadOnly;
            IsQueryable = source.IsQueryable;
            Metadata.AddRange(source.Metadata.Select(x => new DataFramework.Core.Models.MetadataModel(x)));
        }
    }
#nullable restore
}

