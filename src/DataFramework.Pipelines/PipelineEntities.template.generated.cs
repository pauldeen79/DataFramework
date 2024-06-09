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
    public partial class CodeStatementsMapping
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public DataFramework.Domain.DataObjectInfo SourceDataObjectInfo
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public DataFramework.Domain.FieldInfo SourceFieldInfo
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.Builders.CodeStatementBaseBuilder> CodeStatements
        {
            get;
        }

        public CodeStatementsMapping(DataFramework.Domain.DataObjectInfo sourceDataObjectInfo, DataFramework.Domain.FieldInfo sourceFieldInfo, System.Collections.Generic.IEnumerable<ClassFramework.Domain.Builders.CodeStatementBaseBuilder> codeStatements)
        {
            this.SourceDataObjectInfo = sourceDataObjectInfo;
            this.SourceFieldInfo = sourceFieldInfo;
            this.CodeStatements = codeStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.Builders.CodeStatementBaseBuilder>(codeStatements);
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder ToBuilder()
        {
            return new DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder(this);
        }
    }
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

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DefaultBuilderNamespace
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool AddComponentModelAttributes
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool AddValidationCodeInConstructor
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool AddToBuilderMethod
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DataFramework.Pipelines.CodeStatementsMapping> CodeStatementMappings
        {
            get;
        }

        public PipelineSettings(DataFramework.Pipelines.Domains.ConcurrencyCheckBehavior concurrencyCheckBehavior, DataFramework.Pipelines.Domains.EntityClassType entityClassType, string defaultEntityNamespace, string defaultBuilderNamespace, bool addComponentModelAttributes, bool addValidationCodeInConstructor, bool addToBuilderMethod, System.Collections.Generic.IEnumerable<DataFramework.Pipelines.CodeStatementsMapping> codeStatementMappings)
        {
            this.ConcurrencyCheckBehavior = concurrencyCheckBehavior;
            this.EntityClassType = entityClassType;
            this.DefaultEntityNamespace = defaultEntityNamespace;
            this.DefaultBuilderNamespace = defaultBuilderNamespace;
            this.AddComponentModelAttributes = addComponentModelAttributes;
            this.AddValidationCodeInConstructor = addValidationCodeInConstructor;
            this.AddToBuilderMethod = addToBuilderMethod;
            this.CodeStatementMappings = codeStatementMappings is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DataFramework.Pipelines.CodeStatementsMapping>(codeStatementMappings);
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder ToBuilder()
        {
            return new DataFramework.Pipelines.Builders.PipelineSettingsBuilder(this);
        }
    }
}
#nullable disable
