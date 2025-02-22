namespace DataFramework.Pipelines.Tests.Extensions;

public class ServiceCollectionExtensionsTests : TestBase
{
    public class AddDataFrameworkPipelines : ServiceCollectionExtensionsTests
    {
        [Fact]
        public void Registers_All_Required_Dependencies()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddParsers()
                .AddCsharpExpressionDumper() // needed by AddEntityCommandProviderMembersComponent
                .AddDataFrameworkPipelines();

            // Act & Assert
            Action a = () =>
            {
                using var provider = serviceCollection.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
            };
            a.ShouldNotThrow();
        }

        [Fact]
        public void Can_Resolve_EntityPipeline()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddDataFrameworkPipelines();
            using var provider = serviceCollection.BuildServiceProvider();
            using var scope = provider.CreateScope();

            // Act
            var builder = scope.ServiceProvider.GetRequiredService<IPipeline<ClassContext>>();

            // Assert
            builder.ShouldBeOfType<Pipeline<ClassContext>>();
        }
    }
}
