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
    public partial class ExtraFieldIdentityDatabaseCommandProvider : SelectDatabaseCommandProviderBase<ExtraFieldQueryProcessorSettings>, IDatabaseCommandProvider<ExtraFieldIdentity>
    {
        public ExtraFieldIdentityDatabaseCommandProvider() : base(new ExtraFieldQueryProcessorSettings())
        {
        }

        public IDatabaseCommand Create(ExtraFieldIdentity source, DatabaseOperation operation)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Only Select operation is supported");
            }

            //TODO: Add AppendParameters method to SelectCommandBuilder
            //return new SelectCommandBuilder().Select(Settings.Fields).From(Settings.TableName).Where("[EntityName] = @EntityName AND [Name] = @Name").AppendParameters(source).Build();
            return new SqlTextCommand(string.Format(@"SELECT TOP 1 {0} FROM {1} WHERE [EntityName] = @EntityName AND [Name] = @Name", Settings.Fields, Settings.TableName), source);
        }
    }
}
