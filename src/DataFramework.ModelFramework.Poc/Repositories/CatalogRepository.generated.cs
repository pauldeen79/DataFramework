using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogRepository : Repository<Catalog, CatalogIdentity>, ICatalogRepository
    {
        public CatalogRepository(IDatabaseCommandProcessor<Catalog> commandProcessor,
                                 IDatabaseEntityRetriever<Catalog> entityRetriever,
                                 IDatabaseCommandProvider<CatalogIdentity> identitySelectCommandProvider,
                                 IPagedDatabaseCommandProvider pagedEntitySelectCommandProvider,
                                 IDatabaseCommandProvider entitySelectCommandProvider,
                                 IDatabaseCommandProvider<Catalog> entityCommandProvider)
            : base(commandProcessor, entityRetriever, identitySelectCommandProvider, pagedEntitySelectCommandProvider, entitySelectCommandProvider, entityCommandProvider)
        {
        }
    }
}
