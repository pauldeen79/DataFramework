using CrossCutting.Data.Abstractions;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandProviders
{
    //TODO: Move to CrossCutting.Data.Abstractions
    public interface IDatabaseCommandProvider
    {
        IDatabaseCommand Create(DatabaseOperation operation);
    }
    //TODO: Move to CrossCutting.Data.Abstractions
    public interface IDatabaseCommandProvider<in TSource> : IDatabaseCommandProvider
    {
        IDatabaseCommand Create(TSource source, DatabaseOperation operation);
    }
}
