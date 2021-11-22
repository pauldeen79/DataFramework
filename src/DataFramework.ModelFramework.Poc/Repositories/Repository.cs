using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Abstractions.Extensions;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    //TODO: Move to CrossCutting.Data.Core
    public class Repository<TEntity, TIdentity> : IRepository<TEntity, TIdentity>
        where TEntity : class
    {
        public TEntity Add(TEntity instance)
            => _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Insert).HandleResult($"{typeof(TEntity).Name} has not been added");

        public TEntity Update(TEntity instance)
            => _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Update).HandleResult($"{typeof(TEntity).Name} has not been updated");

        public TEntity Delete(TEntity instance)
            => _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Delete).HandleResult($"{typeof(TEntity).Name} has not been deleted");

        public TEntity? Find(TIdentity identity)
            => _entityRetriever.FindOne(_databaseCommandProvider.Create(identity, DatabaseOperation.Select));

        public Repository(IDatabaseCommandProcessor<TEntity> databaseCommandProcessor,
                          IDatabaseEntityRetriever<TEntity> entityRetriever,
                          IDatabaseCommandProvider<TIdentity> databaseCommandProvider)
        {
            _databaseCommandProcessor = databaseCommandProcessor;
            _entityRetriever = entityRetriever;
            _databaseCommandProvider = databaseCommandProvider;
        }

        protected readonly IDatabaseCommandProcessor<TEntity> _databaseCommandProcessor;
        protected readonly IDatabaseEntityRetriever<TEntity> _entityRetriever;
        protected readonly IDatabaseCommandProvider<TIdentity> _databaseCommandProvider;
    }
}
