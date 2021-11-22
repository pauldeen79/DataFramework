using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    public class ExtraFieldIdentityDatabaseCommandProvider : IDatabaseCommandProvider<ExtraFieldIdentity>
    {
        public IDatabaseCommand Create(ExtraFieldIdentity source, DatabaseOperation operation)
        {
            var settings = new ExtraFieldQueryProcessorSettings();
            return new SqlTextCommand(string.Format(@"SELECT TOP 1 {0} FROM {1} WHERE [EntityName] = @EntityName AND [Name] = @Name", settings.Fields, settings.TableName), source);
        }
    }
}
