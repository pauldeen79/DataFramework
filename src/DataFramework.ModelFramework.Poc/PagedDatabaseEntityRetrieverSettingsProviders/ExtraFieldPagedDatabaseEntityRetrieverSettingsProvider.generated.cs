using CrossCutting.Data.Abstractions;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
using PDC.Net.Core.Queries;

namespace DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettingsProviders
{
    public class ExtraFieldPagedDatabaseEntityRetrieverSettingsProvider : IPagedDatabaseEntityRetrieverSettingsProvider
    {
        public bool TryGet<TSource>(out IPagedDatabaseEntityRetrieverSettings settings)
        {
            if (typeof(TSource) == typeof(ExtraFieldQuery))
            {
                settings = new ExtraFieldPagedEntityRetrieverSettings();
                return true;
            }

            settings = default;
            return false;
        }
    }
}
