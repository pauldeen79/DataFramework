using CrossCutting.Data.Abstractions;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;

namespace DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettingsProviders
{
    public class CatalogPagedDatabaseEntityRetrieverSettingsProvider : IPagedDatabaseEntityRetrieverSettingsProvider
    {
        public bool TryGet<TSource>(out IPagedDatabaseEntityRetrieverSettings settings)
        {
            if (typeof(TSource) == typeof(CatalogIdentity) || typeof(TSource) == typeof(CatalogQuery))
            {
                settings = new CatalogPagedEntityRetrieverSettings();
                return true;
            }

            settings = default;
            return false;
        }
    }
}
