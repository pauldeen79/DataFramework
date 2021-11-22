using CrossCutting.Data.Abstractions;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    //TODO: Move to CrossCutting.Data.Abstractions
    public interface IDatabaseCommandProvider<in TSource>
    {
        IDatabaseCommand Create(TSource source, DatabaseOperation operation);
    }
}
