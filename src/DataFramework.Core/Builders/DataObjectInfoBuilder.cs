using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common;
using DataFramework.Abstractions;

namespace DataFramework.Core.Builders
{
    public class DataObjectInfoBuilder
    {
        public List<FieldInfoBuilder> Fields { get; set; }
        public string? AssemblyName { get; set; }
        public string? TypeName { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? DisplayName { get; set; }
        public bool IsVisible { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsQueryable { get; set; }
        public List<MetadataBuilder> Metadata { get; set; }

        public DataObjectInfo Build()
        {
            return new DataObjectInfo(Name,
                                      TypeName,
                                      AssemblyName,
                                      Description,
                                      DisplayName,
                                      IsVisible,
                                      IsReadOnly,
                                      IsQueryable,
                                      new ValueCollection<IFieldInfo>(Fields.Select(x => x.Build())),
                                      new ValueCollection<IMetadata>(Metadata.Select(x => x.Build())));
        }

        public DataObjectInfoBuilder Clear()
        {
            Fields.Clear();
            AssemblyName = default;
            TypeName = default;
            Name = string.Empty;
            Description = default;
            DisplayName = default;
            IsQueryable = true;
            IsVisible = true;
            IsReadOnly = false;
            Metadata.Clear();
            return this;
        }

        public DataObjectInfoBuilder ClearFields()
        {
            Fields.Clear();
            return this;
        }

        public DataObjectInfoBuilder AddFields(IEnumerable<IFieldInfo> fields)
        {
            return AddFields(fields.ToArray());
        }

        public DataObjectInfoBuilder AddFields(params IFieldInfo[] fields)
        {
            if (fields != null)
            {
                foreach (var itemToAdd in fields)
                {
                    Fields.Add(new FieldInfoBuilder(itemToAdd));
                }
            }
            return this;
        }

        public DataObjectInfoBuilder AddFields(IEnumerable<FieldInfoBuilder> fields)
        {
            return AddFields(fields.ToArray());
        }

        public DataObjectInfoBuilder AddFields(params FieldInfoBuilder[] fields)
        {
            if (fields != null)
            {
                foreach (var itemToAdd in fields)
                {
                    Fields.Add(itemToAdd);
                }
            }
            return this;
        }

        public DataObjectInfoBuilder WithAssemblyName(string? assemblyName)
        {
            AssemblyName = assemblyName;
            return this;
        }

        public DataObjectInfoBuilder WithTypeName(string? typeName)
        {
            TypeName = typeName;
            return this;
        }

        public DataObjectInfoBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public DataObjectInfoBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public DataObjectInfoBuilder WithDisplayName(string? displayName)
        {
            DisplayName = displayName;
            return this;
        }

        public DataObjectInfoBuilder WithIsVisible(bool isVisible = true)
        {
            IsVisible = isVisible;
            return this;
        }

        public DataObjectInfoBuilder WithIsReadOnly(bool isReadOnly = true)
        {
            IsReadOnly = isReadOnly;
            return this;
        }

        public DataObjectInfoBuilder WithIsQueryable(bool isQueryable = true)
        {
            IsQueryable = isQueryable;
            return this;
        }

        public DataObjectInfoBuilder ClearMetadata()
        {
            Metadata.Clear();
            return this;
        }

        public DataObjectInfoBuilder AddMetadata(IEnumerable<IMetadata> metadata)
        {
            return AddMetadata(metadata.ToArray());
        }

        public DataObjectInfoBuilder AddMetadata(params IMetadata[] metadata)
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

        public DataObjectInfoBuilder AddMetadata(IEnumerable<MetadataBuilder> metadata)
        {
            return AddMetadata(metadata.ToArray());
        }

        public DataObjectInfoBuilder AddMetadata(params MetadataBuilder[] metadata)
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

        public DataObjectInfoBuilder AddMetadata(string name, object? value)
        {
            Metadata.Add(new MetadataBuilder { Name = name, Value = value });
            return this;
        }

        public DataObjectInfoBuilder()
        {
            Fields = new List<FieldInfoBuilder>();
            Metadata = new List<MetadataBuilder>();
            Name = string.Empty;
            IsQueryable = true;
            IsVisible = true;
            IsReadOnly = false;
        }

        public DataObjectInfoBuilder(IDataObjectInfo source)
        {
            Fields = new List<FieldInfoBuilder>();
            Metadata = new List<MetadataBuilder>();
            foreach (var x in source.Fields) Fields.Add(new FieldInfoBuilder(x));
            AssemblyName = source.AssemblyName;
            TypeName = source.TypeName;
            Name = source.Name;
            Description = source.Description;
            DisplayName = source.DisplayName;
            IsVisible = source.IsVisible;
            IsReadOnly = source.IsReadOnly;
            IsQueryable = source.IsQueryable;
            foreach (var x in source.Metadata) Metadata.Add(new MetadataBuilder(x));
        }
    }
}
