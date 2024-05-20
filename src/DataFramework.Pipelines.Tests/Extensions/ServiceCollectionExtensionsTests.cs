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
                .AddDataFrameworkPipelines();

            // Act & Assert
            serviceCollection.Invoking(x =>
            {
                using var provder = x.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
            }).Should().NotThrow();
        }

        [Fact]
        public void Can_Resolve_EntityPipelineBuilder()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddDataFrameworkPipelines();
            using var provider = serviceCollection.BuildServiceProvider();
            using var scope = provider.CreateScope();

            // Act
            var builder = scope.ServiceProvider.GetRequiredService<IPipelineBuilder<EntityContext>>();

            // Assert
            builder.Should().BeOfType<Pipelines.Entity.PipelineBuilder>();
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
            var builder = scope.ServiceProvider.GetRequiredService<IPipeline<EntityContext>>();

            // Assert
            builder.Should().BeOfType<Pipeline<EntityContext>>();
        }
    }
}
