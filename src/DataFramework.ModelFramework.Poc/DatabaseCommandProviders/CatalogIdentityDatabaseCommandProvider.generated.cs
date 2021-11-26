using System;
using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.Builders;
using CrossCutting.Data.Core.CommandProviders;
using DataFramework.ModelFramework.Poc.QueryProcessorSettings;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogIdentityDatabaseCommandProvider : SelectDatabaseCommandProviderBase<CatalogQueryProcessorSettings>, IDatabaseCommandProvider<CatalogIdentity>
    {
        public CatalogIdentityDatabaseCommandProvider() : base(new CatalogQueryProcessorSettings())
        {
        }

        public IDatabaseCommand Create(CatalogIdentity source, DatabaseOperation operation)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Only Select operation is supported");
            }

            return new SelectCommandBuilder()
                .Select(Settings.Fields)
                .From(Settings.TableName)
                .Where("[Id] = @Id")
                .AppendParameters(source)
                .Build();
        }
    }
}
