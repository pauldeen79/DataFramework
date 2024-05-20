namespace DataFramework.Pipelines.Tests.Entity.Components;

public class ObservableComponentTests : TestBase<Pipelines.Entity.Components.ObservableComponent>
{
    [Theory]
    [InlineData(EntityClassType.ImmutableClass)]
    [InlineData(EntityClassType.ImmutableRecord)]
    [InlineData(EntityClassType.Poco)]
    public async Task Does_Not_Add_ObservableMembers_When_EntityType_Is(EntityClassType entityClassType)
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(entityClassType).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Interfaces.Should().NotContain(typeof(INotifyPropertyChanged).FullName!);
        context.Request.Builder.Fields.Should().NotContain(x => x.Name == "PropertyChanged");
        context.Request.Builder.Methods.Should().NotContain(x => x.Name == "HandlePropertyChanged");
    }

    [Fact]
    public async Task Adds_Observable_Members_When_EntityType_Is_ObservablePoco()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Interfaces.Should().ContainSingle(typeof(INotifyPropertyChanged).FullName!);
        context.Request.Builder.Fields.Should().ContainSingle(x => x.Name == "PropertyChanged");
        context.Request.Builder.Methods.Should().ContainSingle(x => x.Name == "HandlePropertyChanged");
    }
}
