using CrossCutting.Data.Abstractions;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
using PDC.Net.Core.Queries;
using QueryFramework.Abstractions.Queries;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettingsProviders
{
    public class CatalogPagedDatabaseEntityRetrieverSettingsProvider : IPagedDatabaseEntityRetrieverSettingsProvider
    {
        public bool TryCreate(ISingleEntityQuery query, out IPagedDatabaseEntityRetrieverSettings result)
        {
            if (query is CatalogQuery)
            {
                result = new CatalogPagedEntityRetrieverSettings();
                return true;
            }

            result = default;
            return false;
        }
    }
}
