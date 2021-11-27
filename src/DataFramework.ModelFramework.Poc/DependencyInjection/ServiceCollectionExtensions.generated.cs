using System.CodeDom.Compiler;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Sql;
using DataFramework.ModelFramework.Poc.DatabaseCommandEntityProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandProviders;
using DataFramework.ModelFramework.Poc.EntityMappers;
using DataFramework.ModelFramework.Poc.QueryFieldProviders;
using DataFramework.ModelFramework.Poc.QueryProcessorSettings;
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
            instance.AddSingleton<IDatabaseEntityRetriever<Catalog>, DatabaseEntityRetriever<Catalog>>();
            instance.AddSingleton<IDatabaseCommandProcessor<Catalog>, DatabaseCommandProcessor<Catalog, CatalogBuilder>>();
            instance.AddSingleton<IPagedDatabaseCommandProvider<CatalogQuery>, QueryPagedDatabaseCommandProvider<CatalogQuery>>();
            instance.AddSingleton<IDatabaseCommandEntityProvider<Catalog, CatalogBuilder>, CatalogDatabaseCommandEntityProvider>();
            instance.AddSingleton<IDatabaseCommandProvider<Catalog>, CatalogDatabaseCommandProvider>();
            instance.AddSingleton<IDatabaseCommandProvider<CatalogIdentity>, CatalogIdentityDatabaseCommandProvider>();
            instance.AddSingleton<IDatabaseEntityMapper<Catalog>, CatalogEntityMapper>();
            instance.AddSingleton<ICatalogRepository>(serviceProvider
                => new CatalogRepository(serviceProvider.GetRequiredService<IDatabaseCommandProcessor<Catalog>>(),
                                         serviceProvider.GetRequiredService<IDatabaseEntityRetriever<Catalog>>(),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<CatalogIdentity>>(),
                                         new CatalogPagedEntitySelectDatabaseCommandProvider(),
                                         new CatalogEntitySelectDatabaseCommandProvider(),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<Catalog>>()));
            instance.AddSingleton<IPagedDatabaseCommandProvider<CatalogQuery>>(serviceProvider =>
                new QueryPagedDatabaseCommandProvider<CatalogQuery>(new CatalogQueryFieldProvider(serviceProvider.GetRequiredService<IExtraFieldRepository>().FindExtraFieldsByEntityName("Catalog")),
                                                                    new CatalogQueryProcessorSettings()));
            instance.AddSingleton<IQueryProcessor<CatalogQuery, Catalog>>(serviceProvider =>
                new QueryProcessor<CatalogQuery, Catalog>(serviceProvider.GetRequiredService<IDatabaseEntityRetriever<Catalog>>(),
                                                          new CatalogQueryProcessorSettings(),
                                                          serviceProvider.GetRequiredService<IPagedDatabaseCommandProvider<CatalogQuery>>()));

            instance.AddSingleton<IDatabaseEntityRetriever<ExtraField>, DatabaseEntityRetriever<ExtraField>>();
            instance.AddSingleton<IDatabaseCommandProcessor<ExtraField>, DatabaseCommandProcessor<ExtraField, ExtraFieldBuilder>>();
            // only add if the entity is queryable
            //instance.AddSingleton<IPagedDatabaseCommandProvider<ExtraFieldQuery>, QueryPagedDatabaseCommandProvider<ExtraFieldQuery>>();
            instance.AddSingleton<IDatabaseCommandEntityProvider<ExtraField, ExtraFieldBuilder>, ExtraFieldDatabaseCommandEntityProvider>();
            instance.AddSingleton<IDatabaseCommandProvider<ExtraField>, ExtraFieldDatabaseCommandProvider>();
            instance.AddSingleton<IDatabaseCommandProvider<ExtraFieldIdentity>, ExtraFieldIdentityDatabaseCommandProvider>();
            instance.AddSingleton<IDatabaseEntityMapper<ExtraField>, ExtraFieldEntityMapper>();
            instance.AddSingleton<IExtraFieldRepository>(serviceProvider =>
                new ExtraFieldRepository(serviceProvider.GetRequiredService<IDatabaseCommandProcessor<ExtraField>>(),
                                         serviceProvider.GetRequiredService<IDatabaseEntityRetriever<ExtraField>>(),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<ExtraFieldIdentity>>(),
                                         new ExtraFieldPagedEntitySelectDatabaseCommandProvider(),
                                         new ExtraFieldEntitySelectDatabaseCommandProvider(),
                                         serviceProvider.GetRequiredService<IDatabaseCommandProvider<ExtraField>>()));

            // only add if the entity is queryable
            //instance.AddSingleton<IPagedDatabaseCommandProvider<ExtraFieldQuery>>(serviceProvider =>
            //    new QueryPagedDatabaseCommandProvider<ExtraFieldQuery>(new ExtraFieldQueryFieldProvider(serviceProvider.GetRequiredService<IExtraFieldRepository>().FindExtraFieldsByEntityName("ExtraField")),
            //                                                           new ExtraFieldQueryProcessorSettings()));
            //instance.AddSingleton<IQueryProcessor<ExtraFieldQuery, ExtraField>>(serviceProvider =>
            //    new QueryProcessor<ExtraFieldQuery, ExtraField>(serviceProvider.GetRequiredService<IDatabaseEntityRetriever<ExtraField>>(),
            //                                                    new ExtraFieldQueryProcessorSettings(),
            //                                                    serviceProvider.GetRequiredService<IPagedDatabaseCommandProvider<ExtraFieldQuery>>()));


            return instance;
        }
    }
}
