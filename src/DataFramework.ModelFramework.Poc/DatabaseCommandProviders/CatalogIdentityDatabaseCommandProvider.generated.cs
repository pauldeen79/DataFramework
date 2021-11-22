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
    public partial class CatalogIdentityDatabaseCommandProvider : IDatabaseCommandProvider<CatalogIdentity>
    {
        public IDatabaseCommand Create(CatalogIdentity source, DatabaseOperation operation)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Only Select operation is supported");
            }

            var settings = new CatalogQueryProcessorSettings();
            //TODO: Add AppendParameters method to SelectCommandBuilder
            //return new SelectCommandBuilder().Select(settings.Fields).From(settings.TableName).Where("[Id] = @Id").AppendParameters(source).Build();
            return new SqlTextCommand(string.Format(@"SELECT TOP 1 {0} FROM {1} WHERE [Id] = @Id", settings.Fields, settings.TableName), source);
        }
    }
}
