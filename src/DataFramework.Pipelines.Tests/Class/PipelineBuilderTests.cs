namespace DataFramework.Pipelines.Tests.Class;

public class PipelineBuilderTests : IntegrationTestBase<IPipeline<ClassContext>>
{
    public class Process : PipelineBuilderTests
    {
        private ClassContext CreateContext() => new ClassContext
        (
            CreateModel().Build(),
            new PipelineSettingsBuilder().WithDefaultEntityNamespace("MyNamespace").Build(),
            CultureInfo.InvariantCulture
        );

        [Fact]
        public async Task Sets_Partial()
        {
            // Arrange
            var sut = CreateSut();
            var context = CreateContext();

            // Act
            var result = await sut.ProcessAsync(context);

            // Assert
            result.Status.Should().Be(ResultStatus.Ok);
            context.Builder.Partial.Should().BeTrue();
        }

        [Fact]
        public async Task Sets_Namespace_And_Name_According_To_Settings()
        {
            // Arrange
            var sut = CreateSut();
            var context = CreateContext();

            // Act
            var result = await sut.ProcessAsync(context);

            // Assert
            result.Status.Should().Be(ResultStatus.Ok);
            context.Builder.Name.Should().Be("MyEntity");
            context.Builder.Namespace.Should().Be("MyNamespace");
        }
    }

    public class IntegrationTests : PipelineBuilderTests
    {
        [Fact]
        public async Task Creates_Observable_Entity_With_NamespaceMapping()
        {
            // Arrange
            var model = CreateModel();
            var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).WithDefaultEntityNamespace("MyNamespace");
            var context = CreateContext(model, settings);

            var sut = CreateSut();

            // Act
            var result = await sut.ProcessAsync(context);
            result.ThrowIfInvalid();
            var cls = context.Builder;
            
            var svc = Scope!.ServiceProvider.GetRequiredService<ClassFramework.Pipelines.Abstractions.IPipelineService>();
            var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
                .WithAddBackingFields()
                .WithCreateAsObservable()
                ;
            var classFrameworkContext = new ClassFramework.Pipelines.Entity.EntityContext(cls.Build(), classFrameworkSettings, context.FormatProvider);
            result = await svc.ProcessAsync(classFrameworkContext);

            // Assert
            result.IsSuccessful().Should().BeTrue();

            classFrameworkContext.Builder.Name.Should().Be("MyEntity");
            classFrameworkContext.Builder.Namespace.Should().Be("MyNamespace");
            classFrameworkContext.Builder.Interfaces.Should().BeEquivalentTo("System.ComponentModel.INotifyPropertyChanged");
        }
        
        private static ClassContext CreateContext(DataObjectInfoBuilder model, PipelineSettingsBuilder settings)
            => new(model.Build(), settings.Build(), CultureInfo.InvariantCulture);
    }
}
