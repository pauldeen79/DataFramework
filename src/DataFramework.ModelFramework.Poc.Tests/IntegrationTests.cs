using System;
using System.Data;
using System.Data.Stub;
using System.Data.Stub.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CrossCutting.Data.Abstractions;
using DataFramework.ModelFramework.Poc.DependencyInjection;
using DataFramework.ModelFramework.Poc.Repositories;
using Microsoft.Extensions.DependencyInjection;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using QueryFramework.Abstractions;

namespace DataFramework.ModelFramework.Poc.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed partial class IntegrationTests : IDisposable
    {
        private ICatalogRepository Repository { get; }
        private IDatabaseEntityRetriever<Catalog> Retriever { get; }
        private IQueryProcessor<CatalogQuery, Catalog> QueryProcessor { get; }
        private DbConnection Connection { get; }
        private ServiceProvider ServiceProvider { get; }

        public IntegrationTests()
        {
            Connection = new DbConnection();
            Connection.AddResultForDataReader(cmd => cmd.CommandText.StartsWith("SELECT") && cmd.CommandText.Contains(" FROM [ExtraField]"),
                                              () => new[] { new ExtraField("Catalog", "MyField", null, 1, typeof(string).FullName, true) });

            ServiceProvider = new ServiceCollection()
                .AddPdcNet()
                .AddSingleton<IDbConnection>(Connection)
                .BuildServiceProvider();
            Repository = ServiceProvider.GetRequiredService<ICatalogRepository>();
            Retriever = ServiceProvider.GetRequiredService<IDatabaseEntityRetriever<Catalog>>();
            QueryProcessor = ServiceProvider.GetRequiredService<IQueryProcessor<CatalogQuery, Catalog>>();
        }

        public void Dispose()
        {
            ServiceProvider.Dispose();
            Connection.Dispose();
        }
    }
}
