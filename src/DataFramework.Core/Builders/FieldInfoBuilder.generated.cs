﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 6.0.3
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFramework.Core.Builders
{
#nullable enable
    public partial class FieldInfoBuilder
    {
        public string Name
        {
            get
            {
                return _nameDelegate.Value;
            }
            set
            {
                _nameDelegate = new (() => value);
            }
        }

        public string? Description
        {
            get
            {
                return _descriptionDelegate.Value;
            }
            set
            {
                _descriptionDelegate = new (() => value);
            }
        }

        public string? DisplayName
        {
            get
            {
                return _displayNameDelegate.Value;
            }
            set
            {
                _displayNameDelegate = new (() => value);
            }
        }

        public string? TypeName
        {
            get
            {
                return _typeNameDelegate.Value;
            }
            set
            {
                _typeNameDelegate = new (() => value);
            }
        }

        public bool IsNullable
        {
            get
            {
                return _isNullableDelegate.Value;
            }
            set
            {
                _isNullableDelegate = new (() => value);
            }
        }

        public bool IsVisible
        {
            get
            {
                return _isVisibleDelegate.Value;
            }
            set
            {
                _isVisibleDelegate = new (() => value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnlyDelegate.Value;
            }
            set
            {
                _isReadOnlyDelegate = new (() => value);
            }
        }

        public bool IsIdentityField
        {
            get
            {
                return _isIdentityFieldDelegate.Value;
            }
            set
            {
                _isIdentityFieldDelegate = new (() => value);
            }
        }

        public bool IsComputed
        {
            get
            {
                return _isComputedDelegate.Value;
            }
            set
            {
                _isComputedDelegate = new (() => value);
            }
        }

        public bool IsPersistable
        {
            get
            {
                return _isPersistableDelegate.Value;
            }
            set
            {
                _isPersistableDelegate = new (() => value);
            }
        }

        public bool CanGet
        {
            get
            {
                return _canGetDelegate.Value;
            }
            set
            {
                _canGetDelegate = new (() => value);
            }
        }

        public bool CanSet
        {
            get
            {
                return _canSetDelegate.Value;
            }
            set
            {
                _canSetDelegate = new (() => value);
            }
        }

        public bool UseForConcurrencyCheck
        {
            get
            {
                return _useForConcurrencyCheckDelegate.Value;
            }
            set
            {
                _useForConcurrencyCheckDelegate = new (() => value);
            }
        }

        public object? DefaultValue
        {
            get
            {
                return _defaultValueDelegate.Value;
            }
            set
            {
                _defaultValueDelegate = new (() => value);
            }
        }

        public System.Collections.Generic.List<DataFramework.Core.Builders.MetadataBuilder> Metadata
        {
            get;
            set;
        }

        public FieldInfoBuilder AddMetadata(params DataFramework.Core.Builders.MetadataBuilder[] metadata)
        {
            Metadata.AddRange(metadata);
            return this;
        }

        public FieldInfoBuilder AddMetadata(System.Collections.Generic.IEnumerable<DataFramework.Core.Builders.MetadataBuilder> metadata)
        {
            return AddMetadata(metadata.ToArray());
        }

        public FieldInfoBuilder AddMetadata(string name, object? value)
        {
            AddMetadata(new DataFramework.Core.Builders.MetadataBuilder().WithName(name).WithValue(value));
            return this;
        }

        public DataFramework.Abstractions.IFieldInfo Build()
        {
            return new DataFramework.Core.FieldInfo(Name, Description, DisplayName, TypeName, IsNullable, IsVisible, IsReadOnly, IsIdentityField, IsComputed, IsPersistable, CanGet, CanSet, UseForConcurrencyCheck, DefaultValue, Metadata.Select(x => x.Build()));
        }

        public FieldInfoBuilder WithCanGet(bool canGet = true)
        {
            CanGet = canGet;
            return this;
        }

        public FieldInfoBuilder WithCanGet(System.Func<bool> canGetDelegate)
        {
            _canGetDelegate = new (canGetDelegate);
            return this;
        }

        public FieldInfoBuilder WithCanSet(bool canSet = true)
        {
            CanSet = canSet;
            return this;
        }

        public FieldInfoBuilder WithCanSet(System.Func<bool> canSetDelegate)
        {
            _canSetDelegate = new (canSetDelegate);
            return this;
        }

        public FieldInfoBuilder WithDefaultValue(System.Func<object>? defaultValueDelegate)
        {
            _defaultValueDelegate = new (defaultValueDelegate);
            return this;
        }

        public FieldInfoBuilder WithDefaultValue(object? defaultValue)
        {
            DefaultValue = defaultValue;
            return this;
        }

        public FieldInfoBuilder WithDescription(System.Func<string>? descriptionDelegate)
        {
            _descriptionDelegate = new (descriptionDelegate);
            return this;
        }

        public FieldInfoBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public FieldInfoBuilder WithDisplayName(System.Func<string>? displayNameDelegate)
        {
            _displayNameDelegate = new (displayNameDelegate);
            return this;
        }

        public FieldInfoBuilder WithDisplayName(string? displayName)
        {
            DisplayName = displayName;
            return this;
        }

        public FieldInfoBuilder WithIsComputed(bool isComputed = true)
        {
            IsComputed = isComputed;
            return this;
        }

        public FieldInfoBuilder WithIsComputed(System.Func<bool> isComputedDelegate)
        {
            _isComputedDelegate = new (isComputedDelegate);
            return this;
        }

        public FieldInfoBuilder WithIsIdentityField(bool isIdentityField = true)
        {
            IsIdentityField = isIdentityField;
            return this;
        }

        public FieldInfoBuilder WithIsIdentityField(System.Func<bool> isIdentityFieldDelegate)
        {
            _isIdentityFieldDelegate = new (isIdentityFieldDelegate);
            return this;
        }

        public FieldInfoBuilder WithIsNullable(bool isNullable = true)
        {
            IsNullable = isNullable;
            return this;
        }

        public FieldInfoBuilder WithIsNullable(System.Func<bool> isNullableDelegate)
        {
            _isNullableDelegate = new (isNullableDelegate);
            return this;
        }

        public FieldInfoBuilder WithIsPersistable(bool isPersistable = true)
        {
            IsPersistable = isPersistable;
            return this;
        }

        public FieldInfoBuilder WithIsPersistable(System.Func<bool> isPersistableDelegate)
        {
            _isPersistableDelegate = new (isPersistableDelegate);
            return this;
        }

        public FieldInfoBuilder WithIsReadOnly(bool isReadOnly = true)
        {
            IsReadOnly = isReadOnly;
            return this;
        }

        public FieldInfoBuilder WithIsReadOnly(System.Func<bool> isReadOnlyDelegate)
        {
            _isReadOnlyDelegate = new (isReadOnlyDelegate);
            return this;
        }

        public FieldInfoBuilder WithIsVisible(bool isVisible = true)
        {
            IsVisible = isVisible;
            return this;
        }

        public FieldInfoBuilder WithIsVisible(System.Func<bool> isVisibleDelegate)
        {
            _isVisibleDelegate = new (isVisibleDelegate);
            return this;
        }

        public FieldInfoBuilder WithName(System.Func<string> nameDelegate)
        {
            _nameDelegate = new (nameDelegate);
            return this;
        }

        public FieldInfoBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public FieldInfoBuilder WithType(System.Type? type)
        {
            TypeName = type?.AssemblyQualifiedName;
            return this;
        }

        public FieldInfoBuilder WithTypeName(System.Func<string>? typeNameDelegate)
        {
            _typeNameDelegate = new (typeNameDelegate);
            return this;
        }

        public FieldInfoBuilder WithTypeName(string? typeName)
        {
            TypeName = typeName;
            return this;
        }

        public FieldInfoBuilder WithUseForConcurrencyCheck(bool useForConcurrencyCheck = true)
        {
            UseForConcurrencyCheck = useForConcurrencyCheck;
            return this;
        }

        public FieldInfoBuilder WithUseForConcurrencyCheck(System.Func<bool> useForConcurrencyCheckDelegate)
        {
            _useForConcurrencyCheckDelegate = new (useForConcurrencyCheckDelegate);
            return this;
        }

        public FieldInfoBuilder()
        {
            Metadata = new System.Collections.Generic.List<DataFramework.Core.Builders.MetadataBuilder>();
            _nameDelegate = new (() => string.Empty);
            _descriptionDelegate = new (() => default);
            _displayNameDelegate = new (() => default);
            _typeNameDelegate = new (() => default);
            _isNullableDelegate = new (() => default);
            _isVisibleDelegate = new (() => true);
            _isReadOnlyDelegate = new (() => default);
            _isIdentityFieldDelegate = new (() => default);
            _isComputedDelegate = new (() => default);
            _isPersistableDelegate = new (() => true);
            _canGetDelegate = new (() => true);
            _canSetDelegate = new (() => true);
            _useForConcurrencyCheckDelegate = new (() => default);
            _defaultValueDelegate = new (() => default);
        }

        public FieldInfoBuilder(DataFramework.Abstractions.IFieldInfo source)
        {
            Metadata = new System.Collections.Generic.List<DataFramework.Core.Builders.MetadataBuilder>();
            _nameDelegate = new (() => source.Name);
            _descriptionDelegate = new (() => source.Description);
            _displayNameDelegate = new (() => source.DisplayName);
            _typeNameDelegate = new (() => source.TypeName);
            _isNullableDelegate = new (() => source.IsNullable);
            _isVisibleDelegate = new (() => source.IsVisible);
            _isReadOnlyDelegate = new (() => source.IsReadOnly);
            _isIdentityFieldDelegate = new (() => source.IsIdentityField);
            _isComputedDelegate = new (() => source.IsComputed);
            _isPersistableDelegate = new (() => source.IsPersistable);
            _canGetDelegate = new (() => source.CanGet);
            _canSetDelegate = new (() => source.CanSet);
            _useForConcurrencyCheckDelegate = new (() => source.UseForConcurrencyCheck);
            _defaultValueDelegate = new (() => source.DefaultValue);
            Metadata.AddRange(source.Metadata.Select(x => new DataFramework.Core.Builders.MetadataBuilder(x)));
        }

        private System.Lazy<string> _nameDelegate;

        private System.Lazy<string?> _descriptionDelegate;

        private System.Lazy<string?> _displayNameDelegate;

        private System.Lazy<string?> _typeNameDelegate;

        private System.Lazy<bool> _isNullableDelegate;

        private System.Lazy<bool> _isVisibleDelegate;

        private System.Lazy<bool> _isReadOnlyDelegate;

        private System.Lazy<bool> _isIdentityFieldDelegate;

        private System.Lazy<bool> _isComputedDelegate;

        private System.Lazy<bool> _isPersistableDelegate;

        private System.Lazy<bool> _canGetDelegate;

        private System.Lazy<bool> _canSetDelegate;

        private System.Lazy<bool> _useForConcurrencyCheckDelegate;

        private System.Lazy<object?> _defaultValueDelegate;
    }
#nullable restore
}

