using System;
using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using CrossCutting.Data.Core.Builders;
using DataFramework.ModelFramework.Poc.Repositories;
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

            //TODO: Add AppendParameters method to SelectCommandBuilder
            //return new SelectCommandBuilder().Select(_settings.Fields).From(_settings.TableName).Where("[Id] = @Id").AppendParameters(source).Build();
            return new SqlTextCommand(string.Format(@"SELECT TOP 1 {0} FROM {1} WHERE [Id] = @Id", Settings.Fields, Settings.TableName), operation, source);
        }
    }
}
