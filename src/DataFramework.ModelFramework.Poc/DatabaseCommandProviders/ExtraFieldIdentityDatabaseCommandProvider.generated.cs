using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using CrossCutting.Data.Core.Builders;
using DataFramework.ModelFramework.Poc.Repositories;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldIdentityDatabaseCommandProvider : IDatabaseCommandProvider<ExtraFieldIdentity>
    {
        public IDatabaseCommand Create(ExtraFieldIdentity source, DatabaseOperation operation)
        {
            var settings = new ExtraFieldQueryProcessorSettings();

            //TODO: Add AppendParameters method to SelectCommandBuilder
            //return new SelectCommandBuilder().Select(settings.Fields).From(settings.TableName).Where("[EntityName] = @EntityName AND [Name] = @Name").AppendParameters(source).Build();
            return new SqlTextCommand(string.Format(@"SELECT TOP 1 {0} FROM {1} WHERE [EntityName] = @EntityName AND [Name] = @Name", settings.Fields, settings.TableName), source);
        }
    }
}
