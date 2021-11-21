using CrossCutting.Data.Abstractions;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Queries;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    public interface IRepository<TEntity, TQuery, TIdentity> : IDatabaseEntityRetriever<TEntity>, IQueryProcessor<TQuery, TEntity>
        where TEntity : class
        where TQuery : ISingleEntityQuery
    {
        TEntity Add(TEntity instance);
        TEntity Update(TEntity instance);
        TEntity Delete(TEntity instance);
        TEntity? Find(TIdentity identity);
    }
}
