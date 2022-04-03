﻿using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;

namespace DataFramework.ModelFramework.Poc.DatabaseEntityRetrieverSettingsProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public class CatalogDatabaseEntityRetrieverSettingsProvider : IDatabaseEntityRetrieverSettingsProvider
    {
        public bool TryGet<TSource>(out IDatabaseEntityRetrieverSettings settings)
        {
            if (typeof(TSource) == typeof(Catalog) || typeof(TSource) == typeof(CatalogQuery))
            {
                settings = new CatalogPagedEntityRetrieverSettings();
                return true;
            }

            settings = default;
            return false;
        }
    }
}
