using CrossCutting.Data.Abstractions;
using PDC.Net.Core.Entities;
using QueryFramework.Abstractions;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.DatabaseEntityRetrieverProviders
{
    public class CatalogDatabaseEntityRetrieverProvider : IDatabaseEntityRetrieverProvider
    {
        private readonly IDatabaseEntityRetriever<Catalog> _databaseEntityRetriever;

        public CatalogDatabaseEntityRetrieverProvider(IDatabaseEntityRetriever<Catalog> databaseEntityRetriever)
            => _databaseEntityRetriever = databaseEntityRetriever;

        public bool TryCreate<TResult>(IQuery query, out IDatabaseEntityRetriever<TResult> result) where TResult : class
        {
            if (typeof(TResult) == typeof(Catalog))
            {
                result = (IDatabaseEntityRetriever<TResult>)_databaseEntityRetriever;
                return true;
            }

            result = default;
            return false;
        }
    }
}
