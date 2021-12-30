using System;
using DataFramework.Abstractions;

namespace DataFramework.Core
{
    public record Metadata : IMetadata
    {
        public string Name { get; }
        public object? Value { get; }

        public Metadata(string name, object? value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Value = value;
        }

        public override string ToString() => Value == null
            ? $"[{Name}] = NULL"
            : $"[{Name}] = [{Value}]";
    }
}
