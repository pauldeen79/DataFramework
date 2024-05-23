namespace DataFramework.Pipelines.Tests.Entity.Components;

public class EquatableComponentTests : TestBase<Pipelines.Entity.Components.EquatableComponent>
{
    [Theory]
    [InlineData(EntityClassType.ImmutableRecord)]
    [InlineData(EntityClassType.ObservablePoco)]
    [InlineData(EntityClassType.Poco)]
    public async Task Does_Not_Add_Methods_When_EntityClass_Type_Is(EntityClassType entityClassType)
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(entityClassType).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Methods.Should().BeEmpty();
    }

    [Fact]
    public async Task Adds_Methods_When_EntityClass_Type_Is_ImmutableClass()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ImmutableClass).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Methods.Select(x => x.Name).Should().BeEquivalentTo("Equals", "Equals", "GetHashCode", "==", "!=");
    }
}
