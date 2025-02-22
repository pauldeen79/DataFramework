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
            result.Status.ShouldBe(ResultStatus.Ok);
            context.Builder.Partial.ShouldBeTrue();
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
            result.Status.ShouldBe(ResultStatus.Ok);
            context.Builder.Name.ShouldBe("MyEntity");
            context.Builder.Namespace.ShouldBe("MyNamespace");
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
            result.IsSuccessful().ShouldBeTrue();

            classFrameworkContext.Builder.Name.ShouldBe("MyEntity");
            classFrameworkContext.Builder.Namespace.ShouldBe("MyNamespace");
            classFrameworkContext.Builder.Interfaces.ToArray().ShouldBeEquivalentTo(new[] { "System.ComponentModel.INotifyPropertyChanged" });
        }
        
        private static ClassContext CreateContext(DataObjectInfoBuilder model, PipelineSettingsBuilder settings)
            => new(model.Build(), settings.Build(), CultureInfo.InvariantCulture);
    }
}
