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
        private readonly ExtraFieldPagedEntityRetrieverSettings _settings;

        public ExtraFieldIdentityCommandProvider()
        {
            _settings = new ExtraFieldPagedEntityRetrieverSettings();
        }

        public IDatabaseCommand Create(ExtraFieldIdentity source, DatabaseOperation operation)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Only Select operation is supported");
            }

            return new SelectCommandBuilder()
                .Select(_settings.Fields)
                .From(_settings.TableName)
                .Where("[EntityName] = @EntityName AND [Name] = @Name")
                .AppendParameters(source)
                .Build();
        }
    }
}
