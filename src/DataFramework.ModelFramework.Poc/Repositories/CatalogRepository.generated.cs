using System.CodeDom.Compiler;
using System.Collections.Generic;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Abstractions.Extensions;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using PDC.Net.Core.QueryBuilders;
using QueryFramework.Abstractions;
using QueryFramework.Core.Extensions;
using QueryFramework.Core.Queries.Builders.Extensions;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogRepository : ICatalogRepository
    {
        public Catalog Add(Catalog instance)
        {
            return _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Insert).HandleResult("TestEntity has not been added");
        }

        public Catalog Update(Catalog instance)
        {
            return _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Update).HandleResult("TestEntity has not been updated");
        }

        public Catalog Delete(Catalog instance)
        {
            return _databaseCommandProcessor.InvokeCommand(instance, DatabaseOperation.Delete).HandleResult("TestEntity has not been deleted");
        }

        public Catalog FindOne(IDatabaseCommand command)
        {
            return _databaseEntityRetriever.FindOne(command);
        }

        public IReadOnlyCollection<Catalog> FindMany(IDatabaseCommand command)
        {
            return _databaseEntityRetriever.FindMany(command);
        }

        public IPagedResult<Catalog> FindPaged(IPagedDatabaseCommand command)
        {
            return _databaseEntityRetriever.FindPaged(command);
        }

        public Catalog Find(CatalogIdentity identity)
        {

            /// old code: return FindOne(new SqlTextCommand(string.Format(@"SELECT TOP 1 {0} FROM {1} WHERE [Id] = @Id", SelectFields, TableAlias), identity));
            return FindOne(new CatalogQueryBuilder().Take(1).Where("Id".IsEqualTo(identity.Id)).Build());
        }

        public Catalog FindOne(CatalogQuery query)
        {
            return _queryProcessor.FindOne(query);
        }

        public IReadOnlyCollection<Catalog> FindMany(CatalogQuery query)
        {
            return _queryProcessor.FindMany(query);
        }

        public IPagedResult<Catalog> FindPaged(CatalogQuery query)
        {
            return _queryProcessor.FindPaged(query);
        }

        public CatalogRepository(IQueryProcessor<CatalogQuery, Catalog> queryProcessor,
                                 IDatabaseEntityRetriever<Catalog> databaseEntityRetriever,
                                 IDatabaseCommandProcessor<Catalog> databaseCommandProcessor)
        {
            _queryProcessor = queryProcessor;
            _databaseEntityRetriever = databaseEntityRetriever;
            _databaseCommandProcessor = databaseCommandProcessor;
        }

        private readonly IQueryProcessor<CatalogQuery, Catalog> _queryProcessor;
        private readonly IDatabaseEntityRetriever<Catalog> _databaseEntityRetriever;
        private readonly IDatabaseCommandProcessor<Catalog> _databaseCommandProcessor;
    }
}

