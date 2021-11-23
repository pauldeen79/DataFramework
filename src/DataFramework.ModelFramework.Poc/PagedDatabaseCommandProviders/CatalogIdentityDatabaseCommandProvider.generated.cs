using System;
using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.Builders;
using CrossCutting.Data.Core.Commands;
using CrossCutting.Data.Sql.Builders;
using CrossCutting.Data.Sql.CommandProviders;
using DataFramework.ModelFramework.Poc.QueryProcessorSettings;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.PagedDatabaseCommandProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogIdentityDatabaseCommandProvider : PagedSelectDatabaseCommandProviderBase<CatalogQueryProcessorSettings>, IPagedDatabaseCommandProvider<CatalogIdentity>
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

        public IPagedDatabaseCommand CreatePaged(CatalogIdentity source, DatabaseOperation operation, int offset, int pageSize)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Only Select operation is supported");
            }

            return new PagedDatabaseCommand(GenerateCommand(source, offset, pageSize, false),
                                            GenerateCommand(source, offset, pageSize, true),
                                            offset,
                                            pageSize);
        }

        private IDatabaseCommand GenerateCommand(CatalogIdentity source, int offset, int pageSize, bool countOnly)
        {
            return new PagedSelectCommandBuilder()
                .Select(Settings.Fields)
                .From(Settings.TableName)
                .Where("[Id] = @Id")
                .AppendParameters(source)
                .Offset(offset)
                .PageSize(pageSize)
                .Build(countOnly);
        }
    }
}
