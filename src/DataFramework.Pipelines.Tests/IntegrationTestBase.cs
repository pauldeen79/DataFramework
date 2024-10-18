using DatabaseFramework.Domain.Abstractions;

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
            .AddDatabaseFrameworkTemplates()
            .AddDataFrameworkPipelines()
            .AddScoped(_ => assemblyInfoContextService)
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        Scope = Provider.CreateScope();
    }

    protected ClassFramework.Pipelines.Abstractions.IPipelineService GetClassFrameworkPipelineService() => Scope!.ServiceProvider.GetRequiredService<ClassFramework.Pipelines.Abstractions.IPipelineService>();

    protected IPipeline<ClassContext> GetClassPipeline() => Scope!.ServiceProvider.GetRequiredService<IPipeline<ClassContext>>();

    protected ICodeGenerationEngine GetCodeGenerationEngine() => Scope!.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

    protected async Task<string> GenerateCode(ICodeGenerationProvider provider)
    {
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = GetCodeGenerationEngine();

        (await codeGenerationEngine.Generate(provider, generationEnvironment, codeGenerationSettings).ConfigureAwait(false)).ThrowIfInvalid();

        return generationEnvironment.Builder.ToString().ReplaceLineEndings();
    }
}

public abstract class IntegrationTestBase<T> : IntegrationTestBase
    where T : class
{
    protected T CreateSut() => Scope!.ServiceProvider.GetRequiredService<T>();
}
