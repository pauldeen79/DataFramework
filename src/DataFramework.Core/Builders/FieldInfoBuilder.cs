using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common;
using DataFramework.Abstractions;

namespace DataFramework.Core.Builders
{
    public class FieldInfoBuilder
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? DisplayName { get; set; }
        public string? TypeName { get; set; }
        public bool IsVisible { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsIdentityField { get; set; }
        public bool IsComputed { get; set; }
        public bool IsPersistable { get; set; }
        public bool CanGet { get; set; }
        public bool CanSet { get; set; }
        public bool UseForCheckOnOriginalValues { get; set; }
        public object? DefaultValue { get; set; }
        public List<MetadataBuilder> Metadata { get; set; }

        public IFieldInfo Build()
        {
            return new FieldInfo(Name,
                                 TypeName,
                                 Description,
                                 DisplayName,
                                 IsVisible,
                                 IsReadOnly,
                                 IsIdentityField,
                                 IsComputed,
                                 IsPersistable,
                                 CanGet,
                                 CanSet,
                                 UseForCheckOnOriginalValues,
                                 DefaultValue,
                                 new ValueCollection<IMetadata>(Metadata.Select(x => x.Build())));
        }

        public FieldInfoBuilder Clear()
        {
            Name = string.Empty;
            Description = default;
            DisplayName = default;
            TypeName = default;
            IsVisible = default;
            IsReadOnly = default;
            IsIdentityField = default;
            IsComputed = default;
            IsPersistable = default;
            CanGet = default;
            CanSet = default;
            UseForCheckOnOriginalValues = default;
            DefaultValue = default;
            Metadata.Clear();
            return this;
        }

        public FieldInfoBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public FieldInfoBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public FieldInfoBuilder WithDisplayName(string? displayName)
        {
            DisplayName = displayName;
            return this;
        }

        public FieldInfoBuilder WithTypeName(string? typeName)
        {
            TypeName = typeName;
            return this;
        }

        public FieldInfoBuilder WithIsVisible(bool isVisible = true)
        {
            IsVisible = isVisible;
            return this;
        }

        public FieldInfoBuilder WithIsReadOnly(bool isReadOnly = true)
        {
            IsReadOnly = isReadOnly;
            return this;
        }

        public FieldInfoBuilder WithIsIdentityField(bool isIdentityField = true)
        {
            IsIdentityField = isIdentityField;
            return this;
        }

        public FieldInfoBuilder WithIsComputed(bool isComputed = true)
        {
            IsComputed = isComputed;
            return this;
        }

        public FieldInfoBuilder WithIsPersistable(bool isPersistable = true)
        {
            IsPersistable = isPersistable;
            return this;
        }

        public FieldInfoBuilder WithCanGet(bool canGet = true)
        {
            CanGet = canGet;
            return this;
        }

        public FieldInfoBuilder WithCanSet(bool canSet = true)
        {
            CanSet = canSet;
            return this;
        }

        public FieldInfoBuilder WithUseForCheckOnOriginalValues(bool useForCheckOnOriginalValues = true)
        {
            UseForCheckOnOriginalValues = useForCheckOnOriginalValues;
            return this;
        }

        public FieldInfoBuilder WithDefaultValue(object? defaultValue)
        {
            DefaultValue = defaultValue;
            return this;
        }

        public FieldInfoBuilder ClearMetadata()
        {
            Metadata.Clear();
            return this;
        }

        public FieldInfoBuilder AddMetadata(IEnumerable<IMetadata> metadata)
        {
            return AddMetadata(metadata.ToArray());
        }

        public FieldInfoBuilder AddMetadata(params IMetadata[] metadata)
        {
            if (metadata != null)
            {
                foreach (var itemToAdd in metadata)
                {
                    Metadata.Add(new MetadataBuilder(itemToAdd));
                }
            }
            return this;
        }

        public FieldInfoBuilder AddMetadata(IEnumerable<MetadataBuilder> metadata)
        {
            return AddMetadata(metadata.ToArray());
        }

        public FieldInfoBuilder AddMetadata(params MetadataBuilder[] metadata)
        {
            if (metadata != null)
            {
                foreach (var itemToAdd in metadata)
                {
                    Metadata.Add(itemToAdd);
                }
            }
            return this;
        }

        public FieldInfoBuilder()
        {
            Metadata = new List<MetadataBuilder>();
            Name = string.Empty;
        }

        public FieldInfoBuilder(IFieldInfo source)
        {
            Metadata = new List<MetadataBuilder>();
            Name = source.Name;
            Description = source.Description;
            DisplayName = source.DisplayName;
            TypeName = source.TypeName;
            IsVisible = source.IsVisible;
            IsReadOnly = source.IsReadOnly;
            IsIdentityField = source.IsIdentityField;
            IsComputed = source.IsComputed;
            IsPersistable = source.IsPersistable;
            CanGet = source.CanGet;
            CanSet = source.CanSet;
            UseForCheckOnOriginalValues = source.UseForCheckOnOriginalValues;
            DefaultValue = source.DefaultValue;
            foreach (var x in source.Metadata) Metadata.Add(new MetadataBuilder(x));
        }
    }
}
