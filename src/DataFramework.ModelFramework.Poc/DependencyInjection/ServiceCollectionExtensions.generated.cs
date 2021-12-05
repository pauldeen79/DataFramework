using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.CommandProviders;
using CrossCutting.Data.Sql;
using CrossCutting.Data.Sql.CommandProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandEntityProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandProviders;
using DataFramework.ModelFramework.Poc.EntityMappers;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
using DataFramework.ModelFramework.Poc.QueryFieldProviders;
using DataFramework.ModelFramework.Poc.Repositories;
using Microsoft.Extensions.DependencyInjection;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using QueryFramework.Abstractions;
using QueryFramework.SqlServer;

namespace DataFramework.ModelFramework.Poc.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
        public static IServiceCollection AddPdcNet(this IServiceCollection instance)
        {
            //findall/findallpaged:
            instance.AddSingleton<IDatabaseEntityRetriever<Catalog>, DatabaseEntityRetriever<Catalog>>();
            
            //add/update/delete:
            instance.AddSingleton<IDatabaseCommandProcessor<Catalog>, DatabaseCommandProcessor<Catalog, CatalogBuilder>>();
            instance.AddSingleton<IDatabaseCommandEntityProvider<Catalog, CatalogBuilder>, CatalogDatabaseCommandEntityProvider>();
            instance.AddSingleton<IDatabaseCommandProvider<Catalog>, CatalogCommandProvider>();

            //find:
            instance.AddSingleton<IDatabaseCommandProvider<CatalogIdentity>, CatalogIdentityCommandProvider>();

            //query:
            instance.AddSingleton<IPagedDatabaseCommandProvider<CatalogQuery>>(serviceProvider =>
                new QueryPagedDatabaseCommandProvider<CatalogQuery>(new CatalogQueryFieldProvider(serviceProvider.GetRequiredService<IExtraFieldRepository>().FindExtraFieldsByEntityName("Catalog")),
                                                                    new CatalogPagedEntityRetrieverSettings()));
            instance.AddSingleton<IQueryProcessor<CatalogQuery, Catalog>>(serviceProvider =>
                new QueryProcessor<CatalogQuery, Catalog>(serviceProvider.GetRequiredService<IDatabaseEntityRetriever<Catalog>>(),
                                                          serviceProvider.GetRequiredService<IPagedDatabaseCommandProvider<CatalogQuery>>()));

            //find/query:
            instance.AddSingleton<IDatabaseEntityMapper<Catalog>, CatalogEntityMapper>();

            //repository:
            instance.AddSingleton<ICatalogRepository>(serviceProvider
                => new CatalogRepository(serviceProvider.GetRequiredService<IDatabaseCommandProcessor<Catalog>>(),
                                         serviceProvider.GetRequiredService<IDatabaseEntityRetriever<Catalog>>(),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<CatalogIdentity>>(),
                                         new PagedSelectDatabaseCommandProvider(new CatalogPagedEntityRetrieverSettings()),
                                         new SelectDatabaseCommandProvider(new CatalogPagedEntityRetrieverSettings()),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<Catalog>>()));

            //findall/findallpaged:
            instance.AddSingleton<IDatabaseEntityRetriever<ExtraField>, DatabaseEntityRetriever<ExtraField>>();

            //add/update/delete:
            instance.AddSingleton<IDatabaseCommandProcessor<ExtraField>, DatabaseCommandProcessor<ExtraField, ExtraFieldBuilder>>();
            instance.AddSingleton<IDatabaseCommandEntityProvider<ExtraField, ExtraFieldBuilder>, ExtraFieldDatabaseCommandEntityProvider>();
            instance.AddSingleton<IDatabaseCommandProvider<ExtraField>, ExtraFieldCommandProvider>();

            //find:
            instance.AddSingleton<IDatabaseCommandProvider<ExtraFieldIdentity>, ExtraFieldIdentityCommandProvider>();
            
            //find/query:
            instance.AddSingleton<IDatabaseEntityMapper<ExtraField>, ExtraFieldEntityMapper>();

            //repository:
            instance.AddSingleton<IExtraFieldRepository>(serviceProvider =>
                new ExtraFieldRepository(serviceProvider.GetRequiredService<IDatabaseCommandProcessor<ExtraField>>(),
                                         serviceProvider.GetRequiredService<IDatabaseEntityRetriever<ExtraField>>(),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<ExtraFieldIdentity>>(),
                                         new PagedSelectDatabaseCommandProvider(new ExtraFieldPagedEntityRetrieverSettings()),
                                         new SelectDatabaseCommandProvider(new ExtraFieldPagedEntityRetrieverSettings()),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<ExtraField>>()));

            //query:
            //instance.AddSingleton<IPagedDatabaseCommandProvider<ExtraFieldQuery>>(serviceProvider =>
            //    new QueryPagedDatabaseCommandProvider<ExtraFieldQuery>(new ExtraFieldQueryFieldProvider(serviceProvider.GetRequiredService<IExtraFieldRepository>().FindExtraFieldsByEntityName("ExtraField")),
            //                                                           new ExtraFieldQueryProcessorSettings()));
            //instance.AddSingleton<IQueryProcessor<ExtraFieldQuery, ExtraField>>(serviceProvider =>
            //    new QueryProcessor<ExtraFieldQuery, ExtraField>(serviceProvider.GetRequiredService<IDatabaseEntityRetriever<ExtraField>>(),
            //                                                    serviceProvider.GetRequiredService<IPagedDatabaseCommandProvider<ExtraFieldQuery>>()));


            return instance;
        }
    }
}
