﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.0
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
    public partial record Metadata : MetadataBase
    {
        public Metadata(Metadata original) : base((MetadataBase)original)
        {
        }

        public Metadata(string name, object? value) : base(name, value)
        {
            if (name == null) throw new System.ArgumentNullException("name");
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}

