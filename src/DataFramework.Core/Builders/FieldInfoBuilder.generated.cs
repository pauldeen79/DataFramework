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

namespace DataFramework.Core.Builders
{
#nullable enable
    public partial class FieldInfoBuilder : System.ComponentModel.DataAnnotations.IValidatableObject
    {
        public System.Text.StringBuilder Name
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

        public System.Text.StringBuilder? Description
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

        public System.Text.StringBuilder? DisplayName
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

        public System.Text.StringBuilder? TypeName
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

        public DataFramework.Abstractions.IFieldInfo Build()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            return new DataFramework.Core.FieldInfo(Name?.ToString(), Description?.ToString(), DisplayName?.ToString(), TypeName?.ToString(), IsNullable, IsVisible, IsReadOnly, IsIdentityField, IsComputed, IsPersistable, CanGet, CanSet, UseForConcurrencyCheck, DefaultValue, Metadata.Select(x => x.Build()));
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var instance = new DataFramework.Core.FieldInfoBase(Name?.ToString(), Description?.ToString(), DisplayName?.ToString(), TypeName?.ToString(), IsNullable, IsVisible, IsReadOnly, IsIdentityField, IsComputed, IsPersistable, CanGet, CanSet, UseForConcurrencyCheck, DefaultValue, Metadata.Select(x => x.Build()));
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning restore CS8604 // Possible null reference argument.
            var results = new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>();
            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(instance, new System.ComponentModel.DataAnnotations.ValidationContext(instance), results, true);
            return results;
        }

        public FieldInfoBuilder WithName(System.Text.StringBuilder name)
        {
            Name = name;
            return this;
        }

        public FieldInfoBuilder WithName(System.Func<System.Text.StringBuilder> nameDelegate)
        {
            _nameDelegate = new (nameDelegate);
            return this;
        }

        public FieldInfoBuilder WithName(string value)
        {
            if (Name == null)
                Name = new System.Text.StringBuilder();
            Name.Clear().Append(value);
            return this;
        }

        public FieldInfoBuilder AppendToName(string value)
        {
            if (Name == null)
                Name = new System.Text.StringBuilder();
            Name.Append(value);
            return this;
        }

        public FieldInfoBuilder AppendLineToName(string value)
        {
            if (Name == null)
                Name = new System.Text.StringBuilder();
            Name.AppendLine(value);
            return this;
        }

        public FieldInfoBuilder WithDescription(System.Text.StringBuilder? description)
        {
            Description = description;
            return this;
        }

        public FieldInfoBuilder WithDescription(System.Func<System.Text.StringBuilder?> descriptionDelegate)
        {
            _descriptionDelegate = new (descriptionDelegate);
            return this;
        }

        public FieldInfoBuilder WithDescription(string value)
        {
            if (Description == null)
                Description = new System.Text.StringBuilder();
            Description.Clear().Append(value);
            return this;
        }

        public FieldInfoBuilder AppendToDescription(string value)
        {
            if (Description == null)
                Description = new System.Text.StringBuilder();
            Description.Append(value);
            return this;
        }

        public FieldInfoBuilder AppendLineToDescription(string value)
        {
            if (Description == null)
                Description = new System.Text.StringBuilder();
            Description.AppendLine(value);
            return this;
        }

        public FieldInfoBuilder WithDisplayName(System.Text.StringBuilder? displayName)
        {
            DisplayName = displayName;
            return this;
        }

        public FieldInfoBuilder WithDisplayName(System.Func<System.Text.StringBuilder?> displayNameDelegate)
        {
            _displayNameDelegate = new (displayNameDelegate);
            return this;
        }

        public FieldInfoBuilder WithDisplayName(string value)
        {
            if (DisplayName == null)
                DisplayName = new System.Text.StringBuilder();
            DisplayName.Clear().Append(value);
            return this;
        }

        public FieldInfoBuilder AppendToDisplayName(string value)
        {
            if (DisplayName == null)
                DisplayName = new System.Text.StringBuilder();
            DisplayName.Append(value);
            return this;
        }

        public FieldInfoBuilder AppendLineToDisplayName(string value)
        {
            if (DisplayName == null)
                DisplayName = new System.Text.StringBuilder();
            DisplayName.AppendLine(value);
            return this;
        }

        public FieldInfoBuilder WithTypeName(System.Text.StringBuilder? typeName)
        {
            TypeName = typeName;
            return this;
        }

        public FieldInfoBuilder WithTypeName(System.Func<System.Text.StringBuilder?> typeNameDelegate)
        {
            _typeNameDelegate = new (typeNameDelegate);
            return this;
        }

        public FieldInfoBuilder WithTypeName(string value)
        {
            if (TypeName == null)
                TypeName = new System.Text.StringBuilder();
            TypeName.Clear().Append(value);
            return this;
        }

        public FieldInfoBuilder AppendToTypeName(string value)
        {
            if (TypeName == null)
                TypeName = new System.Text.StringBuilder();
            TypeName.Append(value);
            return this;
        }

        public FieldInfoBuilder AppendLineToTypeName(string value)
        {
            if (TypeName == null)
                TypeName = new System.Text.StringBuilder();
            TypeName.AppendLine(value);
            return this;
        }

        public FieldInfoBuilder WithType(System.Type type)
        {
            WithTypeName(type?.AssemblyQualifiedName!);
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

        public FieldInfoBuilder WithDefaultValue(object? defaultValue)
        {
            DefaultValue = defaultValue;
            return this;
        }

        public FieldInfoBuilder WithDefaultValue(System.Func<object?> defaultValueDelegate)
        {
            _defaultValueDelegate = new (defaultValueDelegate);
            return this;
        }

        public FieldInfoBuilder AddMetadata(System.Collections.Generic.IEnumerable<DataFramework.Core.Builders.MetadataBuilder> metadata)
        {
            return AddMetadata(metadata.ToArray());
        }

        public FieldInfoBuilder AddMetadata(params DataFramework.Core.Builders.MetadataBuilder[] metadata)
        {
            Metadata.AddRange(metadata);
            return this;
        }

        public FieldInfoBuilder AddMetadata(string name, object? value)
        {
            AddMetadata(new MetadataBuilder().WithName(name).WithValue(value));
            return this;
        }

        public FieldInfoBuilder()
        {
            Metadata = new System.Collections.Generic.List<DataFramework.Core.Builders.MetadataBuilder>();
            _nameDelegate = new (() => new System.Text.StringBuilder());
            _descriptionDelegate = new (() => default);
            _displayNameDelegate = new (() => default);
            _typeNameDelegate = new (() => default);
            _isNullableDelegate = new (() => default(bool));
            _isVisibleDelegate = new (() => true);
            _isReadOnlyDelegate = new (() => default(bool));
            _isIdentityFieldDelegate = new (() => default(bool));
            _isComputedDelegate = new (() => default(bool));
            _isPersistableDelegate = new (() => true);
            _canGetDelegate = new (() => true);
            _canSetDelegate = new (() => true);
            _useForConcurrencyCheckDelegate = new (() => default(bool));
            _defaultValueDelegate = new (() => default(object?));
        }

        public FieldInfoBuilder(DataFramework.Abstractions.IFieldInfo source)
        {
            Metadata = new System.Collections.Generic.List<DataFramework.Core.Builders.MetadataBuilder>();
            _nameDelegate = new (() => new System.Text.StringBuilder(source.Name));
            _descriptionDelegate = new (() => new System.Text.StringBuilder(source.Description));
            _displayNameDelegate = new (() => new System.Text.StringBuilder(source.DisplayName));
            _typeNameDelegate = new (() => new System.Text.StringBuilder(source.TypeName));
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

        protected System.Lazy<System.Text.StringBuilder> _nameDelegate;

        protected System.Lazy<System.Text.StringBuilder?> _descriptionDelegate;

        protected System.Lazy<System.Text.StringBuilder?> _displayNameDelegate;

        protected System.Lazy<System.Text.StringBuilder?> _typeNameDelegate;

        protected System.Lazy<bool> _isNullableDelegate;

        protected System.Lazy<bool> _isVisibleDelegate;

        protected System.Lazy<bool> _isReadOnlyDelegate;

        protected System.Lazy<bool> _isIdentityFieldDelegate;

        protected System.Lazy<bool> _isComputedDelegate;

        protected System.Lazy<bool> _isPersistableDelegate;

        protected System.Lazy<bool> _canGetDelegate;

        protected System.Lazy<bool> _canSetDelegate;

        protected System.Lazy<bool> _useForConcurrencyCheckDelegate;

        protected System.Lazy<object?> _defaultValueDelegate;
    }
#nullable restore
}

