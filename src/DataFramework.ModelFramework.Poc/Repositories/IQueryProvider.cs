using QueryFramework.Abstractions.Queries;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    public interface IQueryProvider<in TSource, out TQuery>
        where TQuery : ISingleEntityQuery
    {
        TQuery Create(TSource source);
    }
}
