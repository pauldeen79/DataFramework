using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CrossCutting.Data.Abstractions;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial interface ICatalogRepository : IRepository<Catalog, CatalogIdentity>
    {
        IReadOnlyCollection<Catalog> FindSomething();
        Task<IReadOnlyCollection<Catalog>> FindSomethingAsync(CancellationToken token);
    }
}

