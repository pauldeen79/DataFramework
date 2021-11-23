using System;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.Builders;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandProviders
{
    //TODO: Move to CrosCutting.Data.Core
    public abstract class SelectDatabaseCommandProviderBase<TSettings> : IDatabaseCommandProvider
        where TSettings : IQueryProcessorSettings
    {
        protected TSettings Settings { get; }

        protected SelectDatabaseCommandProviderBase(TSettings settings)
        {
            Settings = settings;
        }

        public IDatabaseCommand Create(DatabaseOperation operation)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Only Select operation is supported");
            }

            return new SelectCommandBuilder().Select(Settings.Fields).From(Settings.TableName).Where(Settings.DefaultWhere).OrderBy(Settings.DefaultOrderBy).Build();
        }
    }
}
