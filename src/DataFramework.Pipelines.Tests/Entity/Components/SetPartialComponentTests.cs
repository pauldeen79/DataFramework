namespace DataFramework.Pipelines.Tests.Entity.Components;

public class SetPartialComponentTests : TestBase<Pipelines.Entity.Components.SetPartialComponent>
{
    [Fact]
    public async Task Sets_Partial_Correctly()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Partial.Should().BeTrue();
    }
}
