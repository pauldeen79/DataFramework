namespace DataFramework.ModelFramework.Poc.Tests;

public sealed partial class IntegrationTests : IDisposable
{
    private ICatalogRepository Repository { get; }
    private IDatabaseEntityRetriever<Catalog> Retriever { get; }
    private IQueryProcessor QueryProcessor { get; }
    private DbConnection Connection { get; }
    private ServiceProvider ServiceProvider { get; }

    public IntegrationTests()
    {
        Connection = new DbConnection();
        Connection.AddResultForDataReader(cmd => cmd.CommandText.StartsWith("SELECT") && cmd.CommandText.Contains(" FROM [ExtraField]"),
                                          () => new[] { new ExtraField("Catalog", "MyField", null, 1, typeof(string).FullName, true) });

        ServiceProvider = new ServiceCollection()
            .AddExpressionFramework()
            .AddQueryFrameworkSqlServer(x => x.AddPdcNet())
            .AddSingleton<IDbConnection>(Connection)
            .BuildServiceProvider();
        Repository = ServiceProvider.GetRequiredService<ICatalogRepository>();
        Retriever = ServiceProvider.GetRequiredService<IDatabaseEntityRetriever<Catalog>>();
        QueryProcessor = ServiceProvider.GetRequiredService<IQueryProcessor>();
    }

    public void Dispose()
    {
        ServiceProvider.Dispose();
        Connection.Dispose();
    }
}
