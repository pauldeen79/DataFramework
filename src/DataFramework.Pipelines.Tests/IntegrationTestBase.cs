namespace DataFramework.Pipelines.Tests;

public abstract class IntegrationTestBase : TestBase
{
    protected IntegrationTestBase()
    {
        var assemblyInfoContextService = Fixture.Freeze<IAssemblyInfoContextService>();
        Provider = new ServiceCollection()
            .AddTemplateFramework()
            .AddTemplateFrameworkChildTemplateProvider()
            .AddTemplateFrameworkCodeGeneration()
            .AddTemplateFrameworkRuntime()
            .AddCsharpExpressionDumper()
            .AddParsers()
            .AddClassFrameworkTemplates()
            .AddClassFrameworkPipelines()
            .AddDataFrameworkPipelines()
            .AddScoped(_ => assemblyInfoContextService)
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        Scope = Provider.CreateScope();
    }
}

public abstract class IntegrationTestBase<T> : IntegrationTestBase
    where T : class
{
    protected T CreateSut() => Scope!.ServiceProvider.GetRequiredService<T>();
}
