using System;
using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.Builders;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldIdentityCommandProvider : IDatabaseCommandProvider<ExtraFieldIdentity>
    {
        private ExtraFieldQueryProcessorSettings Settings { get; }

        public ExtraFieldIdentityCommandProvider()
        {
            Settings = new ExtraFieldQueryProcessorSettings();
        }

        public IDatabaseCommand Create(ExtraFieldIdentity source, DatabaseOperation operation)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Only Select operation is supported");
            }

            return new SelectCommandBuilder()
                .Select(Settings.Fields)
                .From(Settings.TableName)
                .Where("[EntityName] = @EntityName AND [Name] = @Name")
                .AppendParameters(source)
                .Build();
        }
    }
}
