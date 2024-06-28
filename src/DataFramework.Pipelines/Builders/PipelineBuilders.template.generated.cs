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

        private System.Collections.ObjectModel.ObservableCollection<DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder> _codeStatementMappings;

        private ClassFramework.Domain.Domains.Visibility _commandEntityProviderVisibility;

        private string _commandEntityProviderNamespace;

        private bool _commandProviderEnableAdd;

        private bool _commandProviderEnableUpdate;

        private bool _commandProviderEnableDelete;

        private System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> _commandEntityProviderAddResultEntityStatements;

        private System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> _commandEntityProviderAddAfterReadStatements;

        private System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> _commandEntityProviderUpdateResultEntityStatements;

        private System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> _commandEntityProviderUpdateAfterReadStatements;

        private System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> _commandEntityProviderDeleteResultEntityStatements;

        private System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> _commandEntityProviderDeleteAfterReadStatements;

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

        public ClassFramework.Domain.Domains.Visibility CommandEntityProviderVisibility
        {
            get
            {
                return _commandEntityProviderVisibility;
            }
            set
            {
                _commandEntityProviderVisibility = value;
                HandlePropertyChanged(nameof(CommandEntityProviderVisibility));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string CommandEntityProviderNamespace
        {
            get
            {
                return _commandEntityProviderNamespace;
            }
            set
            {
                _commandEntityProviderNamespace = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CommandEntityProviderNamespace));
            }
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool CommandProviderEnableAdd
        {
            get
            {
                return _commandProviderEnableAdd;
            }
            set
            {
                _commandProviderEnableAdd = value;
                HandlePropertyChanged(nameof(CommandProviderEnableAdd));
            }
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool CommandProviderEnableUpdate
        {
            get
            {
                return _commandProviderEnableUpdate;
            }
            set
            {
                _commandProviderEnableUpdate = value;
                HandlePropertyChanged(nameof(CommandProviderEnableUpdate));
            }
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool CommandProviderEnableDelete
        {
            get
            {
                return _commandProviderEnableDelete;
            }
            set
            {
                _commandProviderEnableDelete = value;
                HandlePropertyChanged(nameof(CommandProviderEnableDelete));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderAddResultEntityStatements
        {
            get
            {
                return _commandEntityProviderAddResultEntityStatements;
            }
            set
            {
                _commandEntityProviderAddResultEntityStatements = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CommandEntityProviderAddResultEntityStatements));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderAddAfterReadStatements
        {
            get
            {
                return _commandEntityProviderAddAfterReadStatements;
            }
            set
            {
                _commandEntityProviderAddAfterReadStatements = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CommandEntityProviderAddAfterReadStatements));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderUpdateResultEntityStatements
        {
            get
            {
                return _commandEntityProviderUpdateResultEntityStatements;
            }
            set
            {
                _commandEntityProviderUpdateResultEntityStatements = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CommandEntityProviderUpdateResultEntityStatements));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderUpdateAfterReadStatements
        {
            get
            {
                return _commandEntityProviderUpdateAfterReadStatements;
            }
            set
            {
                _commandEntityProviderUpdateAfterReadStatements = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CommandEntityProviderUpdateAfterReadStatements));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderDeleteResultEntityStatements
        {
            get
            {
                return _commandEntityProviderDeleteResultEntityStatements;
            }
            set
            {
                _commandEntityProviderDeleteResultEntityStatements = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CommandEntityProviderDeleteResultEntityStatements));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderDeleteAfterReadStatements
        {
            get
            {
                return _commandEntityProviderDeleteAfterReadStatements;
            }
            set
            {
                _commandEntityProviderDeleteAfterReadStatements = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(CommandEntityProviderDeleteAfterReadStatements));
            }
        }

        public PipelineSettingsBuilder(DataFramework.Pipelines.PipelineSettings source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _codeStatementMappings = new System.Collections.ObjectModel.ObservableCollection<DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder>();
            _commandEntityProviderAddResultEntityStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderAddAfterReadStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderUpdateResultEntityStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderUpdateAfterReadStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderDeleteResultEntityStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderDeleteAfterReadStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _concurrencyCheckBehavior = source.ConcurrencyCheckBehavior;
            _entityClassType = source.EntityClassType;
            _defaultEntityNamespace = source.DefaultEntityNamespace;
            _defaultBuilderNamespace = source.DefaultBuilderNamespace;
            _addComponentModelAttributes = source.AddComponentModelAttributes;
            if (source.CodeStatementMappings is not null) foreach (var item in source.CodeStatementMappings.Select(x => x.ToBuilder())) _codeStatementMappings.Add(item);
            _commandEntityProviderVisibility = source.CommandEntityProviderVisibility;
            _commandEntityProviderNamespace = source.CommandEntityProviderNamespace;
            _commandProviderEnableAdd = source.CommandProviderEnableAdd;
            _commandProviderEnableUpdate = source.CommandProviderEnableUpdate;
            _commandProviderEnableDelete = source.CommandProviderEnableDelete;
            if (source.CommandEntityProviderAddResultEntityStatements is not null) foreach (var item in source.CommandEntityProviderAddResultEntityStatements) _commandEntityProviderAddResultEntityStatements.Add(item);
            if (source.CommandEntityProviderAddAfterReadStatements is not null) foreach (var item in source.CommandEntityProviderAddAfterReadStatements) _commandEntityProviderAddAfterReadStatements.Add(item);
            if (source.CommandEntityProviderUpdateResultEntityStatements is not null) foreach (var item in source.CommandEntityProviderUpdateResultEntityStatements) _commandEntityProviderUpdateResultEntityStatements.Add(item);
            if (source.CommandEntityProviderUpdateAfterReadStatements is not null) foreach (var item in source.CommandEntityProviderUpdateAfterReadStatements) _commandEntityProviderUpdateAfterReadStatements.Add(item);
            if (source.CommandEntityProviderDeleteResultEntityStatements is not null) foreach (var item in source.CommandEntityProviderDeleteResultEntityStatements) _commandEntityProviderDeleteResultEntityStatements.Add(item);
            if (source.CommandEntityProviderDeleteAfterReadStatements is not null) foreach (var item in source.CommandEntityProviderDeleteAfterReadStatements) _commandEntityProviderDeleteAfterReadStatements.Add(item);
        }

        public PipelineSettingsBuilder()
        {
            _codeStatementMappings = new System.Collections.ObjectModel.ObservableCollection<DataFramework.Pipelines.Builders.CodeStatementsMappingBuilder>();
            _commandEntityProviderAddResultEntityStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderAddAfterReadStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderUpdateResultEntityStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderUpdateAfterReadStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderDeleteResultEntityStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _commandEntityProviderDeleteAfterReadStatements = new System.Collections.ObjectModel.ObservableCollection<ClassFramework.Domain.CodeStatementBase>();
            _defaultEntityNamespace = string.Empty;
            _defaultBuilderNamespace = string.Empty;
            _addComponentModelAttributes = true;
            _commandEntityProviderNamespace = string.Empty;
            _commandProviderEnableAdd = true;
            _commandProviderEnableUpdate = true;
            _commandProviderEnableDelete = true;
            SetDefaultValues();
        }

        public DataFramework.Pipelines.PipelineSettings Build()
        {
            return new DataFramework.Pipelines.PipelineSettings(ConcurrencyCheckBehavior, EntityClassType, DefaultEntityNamespace, DefaultBuilderNamespace, AddComponentModelAttributes, CodeStatementMappings.Select(x => x.Build()!).ToList().AsReadOnly(), CommandEntityProviderVisibility, CommandEntityProviderNamespace, CommandProviderEnableAdd, CommandProviderEnableUpdate, CommandProviderEnableDelete, CommandEntityProviderAddResultEntityStatements, CommandEntityProviderAddAfterReadStatements, CommandEntityProviderUpdateResultEntityStatements, CommandEntityProviderUpdateAfterReadStatements, CommandEntityProviderDeleteResultEntityStatements, CommandEntityProviderDeleteAfterReadStatements);
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

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderAddResultEntityStatements(System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderAddResultEntityStatements)
        {
            if (commandEntityProviderAddResultEntityStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderAddResultEntityStatements));
            return AddCommandEntityProviderAddResultEntityStatements(commandEntityProviderAddResultEntityStatements.ToArray());
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderAddResultEntityStatements(params ClassFramework.Domain.CodeStatementBase[] commandEntityProviderAddResultEntityStatements)
        {
            if (commandEntityProviderAddResultEntityStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderAddResultEntityStatements));
            foreach (var item in commandEntityProviderAddResultEntityStatements) CommandEntityProviderAddResultEntityStatements.Add(item);
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderAddAfterReadStatements(System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderAddAfterReadStatements)
        {
            if (commandEntityProviderAddAfterReadStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderAddAfterReadStatements));
            return AddCommandEntityProviderAddAfterReadStatements(commandEntityProviderAddAfterReadStatements.ToArray());
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderAddAfterReadStatements(params ClassFramework.Domain.CodeStatementBase[] commandEntityProviderAddAfterReadStatements)
        {
            if (commandEntityProviderAddAfterReadStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderAddAfterReadStatements));
            foreach (var item in commandEntityProviderAddAfterReadStatements) CommandEntityProviderAddAfterReadStatements.Add(item);
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderUpdateResultEntityStatements(System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderUpdateResultEntityStatements)
        {
            if (commandEntityProviderUpdateResultEntityStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderUpdateResultEntityStatements));
            return AddCommandEntityProviderUpdateResultEntityStatements(commandEntityProviderUpdateResultEntityStatements.ToArray());
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderUpdateResultEntityStatements(params ClassFramework.Domain.CodeStatementBase[] commandEntityProviderUpdateResultEntityStatements)
        {
            if (commandEntityProviderUpdateResultEntityStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderUpdateResultEntityStatements));
            foreach (var item in commandEntityProviderUpdateResultEntityStatements) CommandEntityProviderUpdateResultEntityStatements.Add(item);
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderUpdateAfterReadStatements(System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderUpdateAfterReadStatements)
        {
            if (commandEntityProviderUpdateAfterReadStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderUpdateAfterReadStatements));
            return AddCommandEntityProviderUpdateAfterReadStatements(commandEntityProviderUpdateAfterReadStatements.ToArray());
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderUpdateAfterReadStatements(params ClassFramework.Domain.CodeStatementBase[] commandEntityProviderUpdateAfterReadStatements)
        {
            if (commandEntityProviderUpdateAfterReadStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderUpdateAfterReadStatements));
            foreach (var item in commandEntityProviderUpdateAfterReadStatements) CommandEntityProviderUpdateAfterReadStatements.Add(item);
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderDeleteResultEntityStatements(System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderDeleteResultEntityStatements)
        {
            if (commandEntityProviderDeleteResultEntityStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderDeleteResultEntityStatements));
            return AddCommandEntityProviderDeleteResultEntityStatements(commandEntityProviderDeleteResultEntityStatements.ToArray());
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderDeleteResultEntityStatements(params ClassFramework.Domain.CodeStatementBase[] commandEntityProviderDeleteResultEntityStatements)
        {
            if (commandEntityProviderDeleteResultEntityStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderDeleteResultEntityStatements));
            foreach (var item in commandEntityProviderDeleteResultEntityStatements) CommandEntityProviderDeleteResultEntityStatements.Add(item);
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderDeleteAfterReadStatements(System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderDeleteAfterReadStatements)
        {
            if (commandEntityProviderDeleteAfterReadStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderDeleteAfterReadStatements));
            return AddCommandEntityProviderDeleteAfterReadStatements(commandEntityProviderDeleteAfterReadStatements.ToArray());
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder AddCommandEntityProviderDeleteAfterReadStatements(params ClassFramework.Domain.CodeStatementBase[] commandEntityProviderDeleteAfterReadStatements)
        {
            if (commandEntityProviderDeleteAfterReadStatements is null) throw new System.ArgumentNullException(nameof(commandEntityProviderDeleteAfterReadStatements));
            foreach (var item in commandEntityProviderDeleteAfterReadStatements) CommandEntityProviderDeleteAfterReadStatements.Add(item);
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

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithCommandEntityProviderVisibility(ClassFramework.Domain.Domains.Visibility commandEntityProviderVisibility)
        {
            CommandEntityProviderVisibility = commandEntityProviderVisibility;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithCommandEntityProviderNamespace(string commandEntityProviderNamespace)
        {
            if (commandEntityProviderNamespace is null) throw new System.ArgumentNullException(nameof(commandEntityProviderNamespace));
            CommandEntityProviderNamespace = commandEntityProviderNamespace;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithCommandProviderEnableAdd(bool commandProviderEnableAdd = true)
        {
            CommandProviderEnableAdd = commandProviderEnableAdd;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithCommandProviderEnableUpdate(bool commandProviderEnableUpdate = true)
        {
            CommandProviderEnableUpdate = commandProviderEnableUpdate;
            return this;
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder WithCommandProviderEnableDelete(bool commandProviderEnableDelete = true)
        {
            CommandProviderEnableDelete = commandProviderEnableDelete;
            return this;
        }

        protected void HandlePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
#nullable disable
