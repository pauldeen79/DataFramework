namespace DataFramework.Pipelines.Tests.Class;

public class ClassContextTests : TestBase
{
    public class Constructor : ClassContextTests
    {
        [Fact]
        public void Throws_On_Null_SourceModel()
        {
            // Act & Assert
            this.Invoking(_ => new ClassContext(sourceModel: null!, new PipelineSettingsBuilder().Build(), CultureInfo.InvariantCulture))
                .Should().Throw<ArgumentNullException>().WithParameterName("sourceModel");
        }

        [Fact]
        public void Throws_On_Null_Settings()
        {
            // Act & Assert
            this.Invoking(_ => new ClassContext(sourceModel: CreateModel().Build(), settings: null!, CultureInfo.InvariantCulture))
                .Should().Throw<ArgumentNullException>().WithParameterName("settings");
        }
    }
}
