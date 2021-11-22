using System;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    public class CatalogIdentityDatabaseCommandProvider : IDatabaseCommandProvider<CatalogIdentity>
    {
        public IDatabaseCommand Create(CatalogIdentity source, DatabaseOperation operation)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Only Select operation is supported");
            }

            var settings = new CatalogQueryProcessorSettings();
            return new SqlTextCommand(string.Format(@"SELECT TOP 1 {0} FROM {1} WHERE [Id] = @Id", settings.Fields, settings.TableName), source);
        }
    }
}
