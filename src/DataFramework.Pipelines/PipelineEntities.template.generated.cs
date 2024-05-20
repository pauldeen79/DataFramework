﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.5
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace DataFramework.Pipelines
{
    public partial class PipelineSettings
    {
        public DataFramework.Pipelines.Domains.ConcurrencyCheckBehavior ConcurrencyCheckBehavior
        {
            get;
        }

        public DataFramework.Pipelines.Domains.EntityClassType EntityClassType
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DefaultEntityNamespace
        {
            get;
        }

        public PipelineSettings(DataFramework.Pipelines.Domains.ConcurrencyCheckBehavior concurrencyCheckBehavior, DataFramework.Pipelines.Domains.EntityClassType entityClassType, string defaultEntityNamespace)
        {
            this.ConcurrencyCheckBehavior = concurrencyCheckBehavior;
            this.EntityClassType = entityClassType;
            this.DefaultEntityNamespace = defaultEntityNamespace;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder ToBuilder()
        {
            return new DataFramework.Pipelines.Builders.PipelineSettingsBuilder(this);
        }
    }
}
#nullable disable
