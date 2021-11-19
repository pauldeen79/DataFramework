namespace DataFramework.Abstractions
{
    public interface IMetadata
    {
        string Name { get; }
        object? Value { get; }
    }
}
