using DataFramework.Abstractions;

namespace DataFramework.Core.Builders
{
    public class MetadataBuilder
    {
        public string Name { get; set; }

        public object? Value { get; set; }

        public IMetadata Build()
        {
            return new Metadata(Name, Value);
        }

        public MetadataBuilder Clear()
        {
            Name = string.Empty;
            Value = default;
            return this;
        }

        public MetadataBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public MetadataBuilder WithValue(object? value)
        {
            Value = value;
            return this;
        }

        public MetadataBuilder()
        {
            Name = string.Empty;
        }

        public MetadataBuilder(IMetadata source)
        {
            Name = source.Name;
            Value = source.Value;
        }
    }
}
