using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using DataFramework.ModelFramework.Poc.DatabaseCommandProviders;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogRepository : Repository<Catalog, CatalogIdentity>, ICatalogRepository
    {
        public CatalogRepository(IDatabaseCommandProcessor<Catalog> databaseCommandProcessor, IDatabaseEntityRetriever<Catalog> entityRetriever, IDatabaseCommandProvider<CatalogIdentity> databaseCommandProvider) : base(databaseCommandProcessor, entityRetriever, databaseCommandProvider)
        {
        }
    }
}
