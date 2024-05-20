namespace DataFramework.Pipelines.Tests.Entity.Components;

public class SetNameComponentTests : TestBase<Pipelines.Entity.Components.SetNameComponent>
{
    [Fact]
    public async Task Sets_Name_Correctly()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Name.Should().Be("MyEntity");
    }

    [Fact]
    public async Task Leaves_Namespace_Empty_When_TypeName_Is_Not_Available()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Namespace.Should().BeEmpty();
    }


    [Fact]
    public async Task Uses_Default_Entities_Namespace_From_Settings_When_TypeName_Is_Not_Available()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).WithDefaultEntityNamespace("MyDefaultNamespace").Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Namespace.Should().Be("MyDefaultNamespace");
    }
    [Fact]
    public async Task Sets_Namespace_Correctly_When_TypeName_Is_Available()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").WithTypeName("MyNamespace.MyEntity").Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Namespace.Should().Be("MyNamespace");
    }
}
