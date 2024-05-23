namespace DataFramework.Pipelines.Tests.Entity.Components;

public class SetVisibilityComponentTests : TestBase<Pipelines.Entity.Components.SetVisibilityComponent>
{
    [Theory]
    [InlineData(true, Visibility.Public)]
    [InlineData(false, Visibility.Internal)]
    public async Task Sets_VisibilityCorrectly(bool isVisible, Visibility expectedResult)
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").WithIsVisible(isVisible).Build();
        var settings = new PipelineSettingsBuilder().Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Visibility.Should().Be(expectedResult);
    }
}
