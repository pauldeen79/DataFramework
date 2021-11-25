using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Sql.CommandProviders;
using DataFramework.ModelFramework.Poc.QueryProcessorSettings;

namespace DataFramework.ModelFramework.Poc.PagedDatabaseCommandProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogEntityDatabaseCommandProvider : PagedSelectDatabaseCommandProviderBase<CatalogQueryProcessorSettings>, IPagedDatabaseCommandProvider
    {
        public CatalogEntityDatabaseCommandProvider() : base(new CatalogQueryProcessorSettings())
        {
        }
    }
}
