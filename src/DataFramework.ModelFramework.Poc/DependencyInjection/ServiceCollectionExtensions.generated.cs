using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.CommandProviders;
using CrossCutting.Data.Sql;
using CrossCutting.Data.Sql.CommandProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandEntityProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandProviders;
using DataFramework.ModelFramework.Poc.DatabaseEntityRetrieverProviders;
using DataFramework.ModelFramework.Poc.EntityMappers;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettingsProviders;
using DataFramework.ModelFramework.Poc.QueryFieldProviders;
using DataFramework.ModelFramework.Poc.Repositories;
using Microsoft.Extensions.DependencyInjection;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using QueryFramework.SqlServer.Abstractions;
using QueryFramework.SqlServer.CrossCutting.Data;

namespace DataFramework.ModelFramework.Poc.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
        public static IServiceCollection AddPdcNet(this IServiceCollection instance)
        {
            //crosscutting.core:
            instance.AddSingleton<IDatabaseCommandProvider, SelectDatabaseCommandProvider>();
            instance.AddSingleton<IPagedDatabaseCommandProvider, PagedSelectDatabaseCommandProvider>();

            //findall/findallpaged:
            instance.AddScoped<IDatabaseEntityRetriever<Catalog>, DatabaseEntityRetriever<Catalog>>();
            
            //add/update/delete:
            instance.AddScoped<IDatabaseCommandProcessor<Catalog>, DatabaseCommandProcessor<Catalog, CatalogBuilder>>();
            instance.AddScoped<IDatabaseCommandEntityProvider<Catalog, CatalogBuilder>, CatalogDatabaseCommandEntityProvider>();
            instance.AddSingleton<IDatabaseCommandProvider<Catalog>, CatalogCommandProvider>();

            //find:
            instance.AddSingleton<IDatabaseCommandProvider<CatalogIdentity>, CatalogIdentityCommandProvider>();

            //query:
            instance.AddSingleton<IQueryFieldInfoProvider, CatalogQueryFieldInfoProvider>();
            instance.AddSingleton<IPagedDatabaseCommandProvider<CatalogQuery>, QueryPagedDatabaseCommandProvider>();
            instance.AddSingleton<IDatabaseEntityRetrieverProvider, CatalogDatabaseEntityRetrieverProvider>();
            instance.AddSingleton<IDatabaseEntityRetrieverSettingsProvider, CatalogPagedDatabaseEntityRetrieverSettingsProvider>();
            instance.AddSingleton<IPagedDatabaseEntityRetrieverSettingsProvider, CatalogPagedDatabaseEntityRetrieverSettingsProvider>();

            //find/query:
            instance.AddSingleton<IDatabaseEntityMapper<Catalog>, CatalogEntityMapper>();

            //repository:
            instance.AddScoped<ICatalogRepository, CatalogRepository>();

            //findall/findallpaged:
            instance.AddScoped<IDatabaseEntityRetriever<ExtraField>, DatabaseEntityRetriever<ExtraField>>();

            //add/update/delete:
            instance.AddScoped<IDatabaseCommandProcessor<ExtraField>, DatabaseCommandProcessor<ExtraField, ExtraFieldBuilder>>();
            instance.AddScoped<IDatabaseCommandEntityProvider<ExtraField, ExtraFieldBuilder>, ExtraFieldDatabaseCommandEntityProvider>();
            instance.AddSingleton<IDatabaseCommandProvider<ExtraField>, ExtraFieldCommandProvider>();

            //find:
            instance.AddSingleton<IDatabaseCommandProvider<ExtraFieldIdentity>, ExtraFieldIdentityCommandProvider>();

            //query:
            instance.AddSingleton<IQueryFieldInfoProvider, ExtraFieldQueryFieldInfoProvider>();
            instance.AddSingleton<IPagedDatabaseCommandProvider<ExtraFieldQuery>, QueryPagedDatabaseCommandProvider>();
            instance.AddSingleton<IDatabaseEntityRetrieverProvider, ExtraFieldDatabaseEntityRetrieverProvider>();
            instance.AddSingleton<IDatabaseEntityRetrieverSettingsProvider, ExtraFieldPagedDatabaseEntityRetrieverSettingsProvider>();
            instance.AddSingleton<IPagedDatabaseEntityRetrieverSettingsProvider, ExtraFieldPagedDatabaseEntityRetrieverSettingsProvider>();

            //find/query:
            instance.AddSingleton<IDatabaseEntityMapper<ExtraField>, ExtraFieldEntityMapper>();

            //repository:
            instance.AddScoped<IExtraFieldRepository, ExtraFieldRepository>();

            return instance;
        }
    }
}
