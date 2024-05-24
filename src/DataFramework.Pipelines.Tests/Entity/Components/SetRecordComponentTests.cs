namespace DataFramework.Pipelines.Tests.Entity.Components;

public class SetRecordComponentTests : TestBase<Pipelines.Entity.Components.SetRecordComponent>
{
    [Theory]
    [InlineData(EntityClassType.ImmutableClass, false)]
    [InlineData(EntityClassType.ImmutableRecord, true)]
    [InlineData(EntityClassType.ObservablePoco, false)]
    [InlineData(EntityClassType.Poco, false)]
    public async Task Sets_Record_Correctly(EntityClassType entityClassType, bool expectedResult)
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(entityClassType).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Record.Should().Be(expectedResult);
    }
}
