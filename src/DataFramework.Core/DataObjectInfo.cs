using System;
using CrossCutting.Common;
using DataFramework.Abstractions;

namespace DataFramework.Core
{
    public record DataObjectInfo : IDataObjectInfo
    {
        public ValueCollection<IFieldInfo> Fields
        {
            get;
        }

        public string? AssemblyName
        {
            get;
        }

        public string? TypeName
        {
            get;
        }

        public string Name
        {
            get;
        }

        public string? Description
        {
            get;
        }

        public string? DisplayName
        {
            get;
        }

        public bool IsVisible
        {
            get;
        }

        public bool IsReadOnly
        {
            get;
        }

        public bool IsQueryable
        {
            get;
        }

        public ValueCollection<IMetadata> Metadata
        {
            get;
        }

        public DataObjectInfo(string name, 
                              string? typeName,
                              string? assemblyName,
                              string? description,
                              string? displayName,
                              bool isVisible,
                              bool isReadOnly,
                              bool isQueryable,
                              ValueCollection<IFieldInfo> fields,
                              ValueCollection<IMetadata> metadata)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Fields = fields;
            AssemblyName = assemblyName;
            TypeName = typeName;
            Name = name;
            Description = description;
            DisplayName = displayName;
            IsVisible = isVisible;
            IsReadOnly = isReadOnly;
            IsQueryable = isQueryable;
            Metadata = metadata;
        }

        public override string ToString() => Name;
    }
}
