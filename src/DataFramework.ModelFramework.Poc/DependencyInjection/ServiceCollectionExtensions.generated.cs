using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Sql;
using DataFramework.ModelFramework.Poc.DatabaseCommandEntityProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandProviders;
using DataFramework.ModelFramework.Poc.DatabaseEntityRetrieverProviders;
using DataFramework.ModelFramework.Poc.DatabaseEntityRetrieverSettingsProviders;
using DataFramework.ModelFramework.Poc.EntityMappers;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettingsProviders;
using DataFramework.ModelFramework.Poc.QueryFieldInfoProviders;
using DataFramework.ModelFramework.Poc.Repositories;
using Microsoft.Extensions.DependencyInjection;
using PDC.Net.Core.Entities;
using QueryFramework.SqlServer.Abstractions;
using QueryFramework.SqlServer.Extensions;

namespace DataFramework.ModelFramework.Poc.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
        public static IServiceCollection AddPdcNet(this IServiceCollection instance)
        {
            return instance.AddQueryFrameworkSqlServer(x =>
            {
                //findall/findallpaged:
                x.AddSingleton<IDatabaseEntityRetriever<Catalog>, DatabaseEntityRetriever<Catalog>>();

                //add/update/delete:
                x.AddScoped<IDatabaseCommandProcessor<Catalog>, DatabaseCommandProcessor<Catalog, CatalogBuilder>>();
                x.AddScoped<IDatabaseCommandEntityProvider<Catalog, CatalogBuilder>, CatalogDatabaseCommandEntityProvider>();
                x.AddSingleton<IDatabaseCommandProvider<Catalog>, CatalogCommandProvider>();

                //find:
                x.AddSingleton<IDatabaseCommandProvider<CatalogIdentity>, CatalogIdentityCommandProvider>();

                //query:
                x.AddSingleton<IQueryFieldInfoProvider, CatalogQueryFieldInfoProvider>();
                x.AddSingleton<IDatabaseEntityRetrieverProvider, CatalogDatabaseEntityRetrieverProvider>();
                x.AddSingleton<IDatabaseEntityRetrieverSettingsProvider, CatalogDatabaseEntityRetrieverSettingsProvider>();
                x.AddSingleton<IPagedDatabaseEntityRetrieverSettingsProvider, CatalogPagedDatabaseEntityRetrieverSettingsProvider>();

                //find/query:
                x.AddSingleton<IDatabaseEntityMapper<Catalog>, CatalogEntityMapper>();

                //repository:
                x.AddScoped<ICatalogRepository, CatalogRepository>();

                //findall/findallpaged:
                x.AddSingleton<IDatabaseEntityRetriever<ExtraField>, DatabaseEntityRetriever<ExtraField>>();

                //add/update/delete:
                x.AddScoped<IDatabaseCommandProcessor<ExtraField>, DatabaseCommandProcessor<ExtraField, ExtraFieldBuilder>>();
                x.AddScoped<IDatabaseCommandEntityProvider<ExtraField, ExtraFieldBuilder>, ExtraFieldDatabaseCommandEntityProvider>();
                x.AddSingleton<IDatabaseCommandProvider<ExtraField>, ExtraFieldCommandProvider>();

                //find:
                x.AddSingleton<IDatabaseCommandProvider<ExtraFieldIdentity>, ExtraFieldIdentityCommandProvider>();

                //query:
                x.AddSingleton<IQueryFieldInfoProvider, ExtraFieldQueryFieldInfoProvider>();
                x.AddSingleton<IDatabaseEntityRetrieverProvider, ExtraFieldDatabaseEntityRetrieverProvider>();
                x.AddSingleton<IDatabaseEntityRetrieverSettingsProvider, ExtraFieldDatabaseEntityRetrieverSettingsProvider>();
                x.AddSingleton<IPagedDatabaseEntityRetrieverSettingsProvider, ExtraFieldPagedDatabaseEntityRetrieverSettingsProvider>();

                //find/query:
                x.AddSingleton<IDatabaseEntityMapper<ExtraField>, ExtraFieldEntityMapper>();

                //repository:
                x.AddScoped<IExtraFieldRepository, ExtraFieldRepository>();
            });
        }
    }
}
