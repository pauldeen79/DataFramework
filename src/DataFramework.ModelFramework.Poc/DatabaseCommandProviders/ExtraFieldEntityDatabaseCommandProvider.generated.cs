using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.CommandProviders;
using DataFramework.ModelFramework.Poc.QueryProcessorSettings;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldEntityDatabaseCommandProvider : SelectDatabaseCommandProviderBase<ExtraFieldQueryProcessorSettings>, IDatabaseCommandProvider
    {
        public ExtraFieldEntityDatabaseCommandProvider() : base(new ExtraFieldQueryProcessorSettings())
        {
        }
    }
}
