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
    public partial record MetadataBase : DataFramework.Abstractions.IMetadata
    {
        public string Name
        {
            get;
        }

        public object? Value
        {
            get;
        }

        public MetadataBase(string name, object? value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
#nullable restore
}

