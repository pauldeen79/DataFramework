using System.Collections.Generic;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Abstractions.Extensions;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Queries;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    public class Repository<TEntity, TQuery, TIdentity> : IRepository<TEntity, TQuery, TIdentity>
        where TEntity : class
        where TQuery : ISingleEntityQuery
    {
        public TEntity Add(TEntity instance)
            => _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Insert).HandleResult($"{typeof(TEntity).Name} has not been added");

        public TEntity Update(TEntity instance)
            => _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Update).HandleResult($"{typeof(TEntity).Name} has not been updated");

        public TEntity Delete(TEntity instance)
            => _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Delete).HandleResult($"{typeof(TEntity).Name} has not been deleted");

        public TEntity? FindOne(IDatabaseCommand command)
            => _databaseEntityRetriever.FindOne(command);

        public TEntity? FindOne(TQuery query)
            => _queryProcessor.FindOne(query);

        public IReadOnlyCollection<TEntity> FindMany(IDatabaseCommand command)
            => _databaseEntityRetriever.FindMany(command);

        public IReadOnlyCollection<TEntity> FindMany(TQuery query)
            => _queryProcessor.FindMany(query);

        public IPagedResult<TEntity> FindPaged(IPagedDatabaseCommand command)
            => _databaseEntityRetriever.FindPaged(command);

        public IPagedResult<TEntity> FindPaged(TQuery query)
            => _queryProcessor.FindPaged(query);

        public TEntity? Find(TIdentity identity)
            => FindOne(_queryProvider.Create(identity));

        public Repository(IQueryProcessor<TQuery, TEntity> queryProcessor,
                          IQueryProvider<TIdentity, TQuery> queryProvider,
                          IDatabaseEntityRetriever<TEntity> databaseEntityRetriever,
                          IDatabaseCommandProcessor<TEntity> databaseCommandProcessor)
        {
            _queryProcessor = queryProcessor;
            _queryProvider = queryProvider;
            _databaseEntityRetriever = databaseEntityRetriever;
            _databaseCommandProcessor = databaseCommandProcessor;
        }

        private readonly IQueryProcessor<TQuery, TEntity> _queryProcessor;
        private readonly IDatabaseEntityRetriever<TEntity> _databaseEntityRetriever;
        private readonly IDatabaseCommandProcessor<TEntity> _databaseCommandProcessor;
        private readonly IQueryProvider<TIdentity, TQuery> _queryProvider;
    }
}
