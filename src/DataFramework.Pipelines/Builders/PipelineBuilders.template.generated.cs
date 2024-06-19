﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.6
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
namespace DataFramework.Pipelines.Builders
{
    public partial class CodeStatementsMappingBuilder : System.ComponentModel.INotifyPropertyChanged
    {
        private DataFramework.Domain.Builders.DataObjectInfoBuilder _sourceDataObjectInfo;

        private DataFramework.Domain.Builders.FieldInfoBuilder _sourceFieldInfo;

        private System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.Builders.CodeStatementBaseBuilder> _codeStatements;

        public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public DataFramework.Domain.Builders.DataObjectInfoBuilder SourceDataObjectInfo
        {
            get
            {
                return _sourceDataObjectInfo;
            }
            set
            {
                _sourceDataObjectInfo = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(SourceDataObjectInfo));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public DataFramework.Domain.Builders.FieldInfoBuilder SourceFieldInfo
        {
            get
            {
                return _sourceFieldInfo;
            }
            set
            {
                _sourceFieldInfo = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(SourceFieldInfo));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.Builders.CodeStatementBaseBuilder> CodeStatements
        {
            get
            {
                return _codeStatements;
            }
            set
            {
                _codeStatements = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CodeStatements));
            }
        }

        public CodeStatementsMappingBuilder(DataFramework.Pipelines.CodeStatementsMapping source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _codeStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.Builders.CodeStatementBaseBuilder>();
            _sourceDataObjectInfo = source.SourceDataObjectInfo.ToBuilder();
            _sourceFieldInfo = source.SourceFieldInfo.ToBuilder();
            if (source.CodeStatements is not null) foreach (var item in source.CodeStatements) _codeStatements.Add(item);
        }

        public CodeStatementsMappingBuilder()
        {
            _codeStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.Builders.CodeStatementBaseBuilder>();
            _sourceDataObjectInfo = new DataFramework.Domain.Builders.DataObjectInfoBuilder()!;
            _sourceFieldInfo = new DataFramework.Domain.Builders.FieldInfoBuilder()!;
            SetDefaultValues();
        }

        public DataFramework.Pipelines.CodeStatementsMapping Build()
        {
            return new DataFramework.Pipelines.CodeStatementsMapping(SourceDataObjectInfo.Build(), SourceFieldInfo.Build(), CodeStatements);
        }

        partial void SetDefaultValues();

        public DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder AddCodeStatements(System.Collections.Generic.IEnumerable<ClassFramework.Domain.Builders.CodeStatementBaseBuilder> codeStatements)
        {
            if (codeStatements is null) throw new System.ArgumentNullException(nameof(codeStatements));
            return AddCodeStatements(codeStatements.ToArray());
        }

        public DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder AddCodeStatements(params ClassFramework.Domain.Builders.CodeStatementBaseBuilder[] codeStatements)
        {
            if (codeStatements is null) throw new System.ArgumentNullException(nameof(codeStatements));
            foreach (var item in codeStatements) CodeStatements.Add(item);
            return this;
        }

        public DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder WithSourceDataObjectInfo(DataFramework.Domain.Builders.DataObjectInfoBuilder sourceDataObjectInfo)
        {
            if (sourceDataObjectInfo is null) throw new System.ArgumentNullException(nameof(sourceDataObjectInfo));
            SourceDataObjectInfo = sourceDataObjectInfo;
            return this;
        }

        public DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder WithSourceFieldInfo(DataFramework.Domain.Builders.FieldInfoBuilder sourceFieldInfo)
        {
            if (sourceFieldInfo is null) throw new System.ArgumentNullException(nameof(sourceFieldInfo));
            SourceFieldInfo = sourceFieldInfo;
            return this;
        }

        protected void HandlePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class PipelineSettingsBuilder : System.ComponentModel.INotifyPropertyChanged
    {
        private DataFramework.Pipelines.Domains.ConcurrencyCheckBehavior _concurrencyCheckBehavior;

        private DataFramework.Pipelines.Domains.EntityClassType _entityClassType;

        private string _defaultEntityNamespace;

        private string _defaultBuilderNamespace;

        private bool _addComponentModelAttributes;

        private bool _addValidationCodeInConstructor;

        private bool _addToBuilderMethod;

        private System.Collections.ObjectModel.ObservableCollection<DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder> _codeStatementMappings;

        public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

        public DataFramework.Pipelines.Domains.ConcurrencyCheckBehavior ConcurrencyCheckBehavior
        {
            get
            {
                return _concurrencyCheckBehavior;
            }
            set
            {
                _concurrencyCheckBehavior = value;
                HandlePropertyChanged(nameof(ConcurrencyCheckBehavior));
            }
        }

        public DataFramework.Pipelines.Domains.EntityClassType EntityClassType
        {
            get
            {
                return _entityClassType;
            }
            set
            {
                _entityClassType = value;
                HandlePropertyChanged(nameof(EntityClassType));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DefaultEntityNamespace
        {
            get
            {
                return _defaultEntityNamespace;
            }
            set
            {
                _defaultEntityNamespace = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(DefaultEntityNamespace));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DefaultBuilderNamespace
        {
            get
            {
                return _defaultBuilderNamespace;
            }
            set
            {
                _defaultBuilderNamespace = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(DefaultBuilderNamespace));
            }
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool AddComponentModelAttributes
        {
            get
            {
                return _addComponentModelAttributes;
            }
            set
            {
                _addComponentModelAttributes = value;
                HandlePropertyChanged(nameof(AddComponentModelAttributes));
            }
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool AddValidationCodeInConstructor
        {
            get
            {
                return _addValidationCodeInConstructor;
            }
            set
            {
                _addValidationCodeInConstructor = value;
                HandlePropertyChanged(nameof(AddValidationCodeInConstructor));
            }
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool AddToBuilderMethod
        {
            get
            {
                return _addToBuilderMethod;
            }
            set
            {
                _addToBuilderMethod = value;
                HandlePropertyChanged(nameof(AddToBuilderMethod));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.ObjectModel.ObservableCollection<DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder> CodeStatementMappings
        {
            get
            {
                return _codeStatementMappings;
            }
            set
            {
                _codeStatementMappings = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CodeStatementMappings));
            }
        }

        public PipelineSettingsBuilder(DataFramework.Pipelines.PipelineSettings source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _codeStatementMappings = new System.Collections.ObjectModel.ObservableCollection<DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder>();
            _concurrencyCheckBehavior = source.ConcurrencyCheckBehavior;
            _entityClassType = source.EntityClassType;
            _defaultEntityNamespace = source.DefaultEntityNamespace;
            _defaultBuilderNamespace = source.DefaultBuilderNamespace;
            _addComponentModelAttributes = source.AddComponentModelAttributes;
            _addValidationCodeInConstructor = source.AddValidationCodeInConstructor;
            _addToBuilderMethod = source.AddToBuilderMethod;
            if (source.CodeStatementMappings is not null) foreach (var item in source.CodeStatementMappings.Select(x => x.ToBuilder())) _codeStatementMappings.Add(item);
        }

        public PipelineSettingsBuilder()
        {
            _codeStatementMappings = new System.Collections.ObjectModel.ObservableCollection<DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder>();
            _defaultEntityNamespace = string.Empty;
            _defaultBuilderNamespace = string.Empty;
            _addComponentModelAttributes = true;
            _addValidationCodeInConstructor = true;
            _addToBuilderMethod = true;
            SetDefaultValues();
        }

        public DataFramework.Pipelines.PipelineSettings Build()
        {
            return new DataFramework.Pipelines.PipelineSettings(ConcurrencyCheckBehavior, EntityClassType, DefaultEntityNamespace, DefaultBuilderNamespace, AddComponentModelAttributes, AddValidationCodeInConstructor, AddToBuilderMethod, CodeStatementMappings.Select(x => x.Build()!).ToList().AsReadOnly());
        }

        partial void SetDefaultValues();

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCodeStatementMappings(System.Collections.Generic.IEnumerable<DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder> codeStatementMappings)
        {
            if (codeStatementMappings is null) throw new System.ArgumentNullException(nameof(codeStatementMappings));
            return AddCodeStatementMappings(codeStatementMappings.ToArray());
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCodeStatementMappings(params DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder[] codeStatementMappings)
        {
            if (codeStatementMappings is null) throw new System.ArgumentNullException(nameof(codeStatementMappings));
            foreach (var item in codeStatementMappings) CodeStatementMappings.Add(item);
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithConcurrencyCheckBehavior(DataFramework.Pipelines.Domains.ConcurrencyCheckBehavior concurrencyCheckBehavior)
        {
            ConcurrencyCheckBehavior = concurrencyCheckBehavior;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithEntityClassType(DataFramework.Pipelines.Domains.EntityClassType entityClassType)
        {
            EntityClassType = entityClassType;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithDefaultEntityNamespace(string defaultEntityNamespace)
        {
            if (defaultEntityNamespace is null) throw new System.ArgumentNullException(nameof(defaultEntityNamespace));
            DefaultEntityNamespace = defaultEntityNamespace;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithDefaultBuilderNamespace(string defaultBuilderNamespace)
        {
            if (defaultBuilderNamespace is null) throw new System.ArgumentNullException(nameof(defaultBuilderNamespace));
            DefaultBuilderNamespace = defaultBuilderNamespace;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithAddComponentModelAttributes(bool addComponentModelAttributes = true)
        {
            AddComponentModelAttributes = addComponentModelAttributes;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithAddValidationCodeInConstructor(bool addValidationCodeInConstructor = true)
        {
            AddValidationCodeInConstructor = addValidationCodeInConstructor;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithAddToBuilderMethod(bool addToBuilderMethod = true)
        {
            AddToBuilderMethod = addToBuilderMethod;
            return this;
        }

        protected void HandlePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
#nullable disable
