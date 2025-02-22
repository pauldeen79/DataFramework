namespace DataFramework.Pipelines.Tests.Class;

public class ClassContextTests : TestBase
{
    public class Constructor : ClassContextTests
    {
        [Fact]
        public void Throws_On_Null_SourceModel()
        {
            // Act & Assert
            Action a = () => _ = new ClassContext(sourceModel: null!, new PipelineSettingsBuilder().Build(), CultureInfo.InvariantCulture);
            a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("sourceModel");
        }

        [Fact]
        public void Throws_On_Null_Settings()
        {
            // Act & Assert
            Action a = () => _ = new ClassContext(sourceModel: CreateModel().Build(), settings: null!, CultureInfo.InvariantCulture);
            a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("settings");
        }
    }
}
