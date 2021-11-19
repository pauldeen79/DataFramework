using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using QueryFramework.Abstractions;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial interface ICatalogRepository : IQueryProcessor<CatalogQuery, Catalog>, IDatabaseEntityRetriever<Catalog>
    {
        Catalog Add(Catalog instance);

        Catalog Update(Catalog instance);

        Catalog Delete(Catalog instance);

        Catalog Find(CatalogIdentity identity);
    }
}

