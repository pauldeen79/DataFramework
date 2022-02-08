using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.CommandProviders;
using CrossCutting.Data.Sql;
using CrossCutting.Data.Sql.CommandProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandEntityProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandProviders;
using DataFramework.ModelFramework.Poc.DatabaseEntityRetrieverProviders;
using DataFramework.ModelFramework.Poc.EntityMappers;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
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
            instance.AddSingleton<IPagedDatabaseEntityRetrieverSettingsProvider, CatalogPagedDatabaseEntityRetrieverSettingsProvider>();

            //find/query:
            instance.AddSingleton<IDatabaseEntityMapper<Catalog>, CatalogEntityMapper>();

            //repository:
            instance.AddScoped<ICatalogRepository>(serviceProvider
                => new CatalogRepository(serviceProvider.GetRequiredService<IDatabaseCommandProcessor<Catalog>>(),
                                         serviceProvider.GetRequiredService<IDatabaseEntityRetriever<Catalog>>(),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<CatalogIdentity>>(),
                                         new PagedSelectDatabaseCommandProvider(new CatalogPagedEntityRetrieverSettings()),
                                         new SelectDatabaseCommandProvider(new CatalogPagedEntityRetrieverSettings()),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<Catalog>>()));

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
            instance.AddSingleton<IPagedDatabaseEntityRetrieverSettingsProvider, ExtraFieldPagedDatabaseEntityRetrieverSettingsProvider>();

            //find/query:
            instance.AddSingleton<IDatabaseEntityMapper<ExtraField>, ExtraFieldEntityMapper>();

            //repository:
            instance.AddScoped<IExtraFieldRepository>(serviceProvider =>
                new ExtraFieldRepository(serviceProvider.GetRequiredService<IDatabaseCommandProcessor<ExtraField>>(),
                                         serviceProvider.GetRequiredService<IDatabaseEntityRetriever<ExtraField>>(),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<ExtraFieldIdentity>>(),
                                         new PagedSelectDatabaseCommandProvider(new ExtraFieldPagedEntityRetrieverSettings()),
                                         new SelectDatabaseCommandProvider(new ExtraFieldPagedEntityRetrieverSettings()),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<ExtraField>>()));

            return instance;
        }
    }
}
