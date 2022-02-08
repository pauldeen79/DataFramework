using CrossCutting.Data.Abstractions;
using PDC.Net.Core.Entities;
using QueryFramework.Abstractions.Queries;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.DatabaseEntityRetrieverProviders
{
    public class ExtraFieldDatabaseEntityRetrieverProvider : IDatabaseEntityRetrieverProvider
    {
        private readonly IDatabaseEntityRetriever<ExtraField> _databaseEntityRetriever;

        public ExtraFieldDatabaseEntityRetrieverProvider(IDatabaseEntityRetriever<ExtraField> databaseEntityRetriever)
            => _databaseEntityRetriever = databaseEntityRetriever;

        public bool TryCreate<TResult>(ISingleEntityQuery query, out IDatabaseEntityRetriever<TResult> result) where TResult : class
        {
            if (typeof(TResult) == typeof(ExtraField))
            {
                result = (IDatabaseEntityRetriever<TResult>)_databaseEntityRetriever;
                return true;
            }

            result = default;
            return false;
        }
    }
}
