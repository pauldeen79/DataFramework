using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogRepository : Repository<Catalog, CatalogIdentity>, ICatalogRepository
    {
        public CatalogRepository(IDatabaseCommandProcessor<Catalog> databaseCommandProcessor,
                                 IDatabaseEntityRetriever<Catalog> entityRetriever,
                                 IPagedDatabaseCommandProvider<CatalogIdentity> identityDatabaseCommandProvider,
                                 IPagedDatabaseCommandProvider genericDatabaseCommandProvider,
                                 IDatabaseCommandProvider<Catalog> entityDatabaseCommandProvider)
            : base(databaseCommandProcessor, entityRetriever, identityDatabaseCommandProvider, genericDatabaseCommandProvider, entityDatabaseCommandProvider)
        {
        }
    }
}
