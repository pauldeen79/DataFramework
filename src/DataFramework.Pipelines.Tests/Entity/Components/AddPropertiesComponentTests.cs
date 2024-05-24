namespace DataFramework.Pipelines.Tests.Entity.Components;

public class AddPropertiesComponentTests : TestBase<Pipelines.Entity.Components.AddPropertiesComponent>
{
    [Fact]
    public async Task Adds_Properties_Correctly()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!)).Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ImmutableRecord).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Properties.Select(x => x.Name).Should().BeEquivalentTo("MyField");
        //TODO: Assert getter and setter statements
    }

    [Fact]
    public async Task Adds_Properties_Correctly_Using_ObservableEntity()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!)).Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Properties.Select(x => x.Name).Should().BeEquivalentTo("MyField");
        //TODO: Assert getter and setter statements
    }

    [Fact]
    public async Task Adds_Original_Properties_When_Using_ConcurrencyChecks()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!)).Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ImmutableRecord).WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Properties.Select(x => x.Name).Should().BeEquivalentTo("MyField", "MyFieldOriginal");
        //TODO: Assert getter and setter statements
    }

    [Fact]
    public async Task Adds_Original_Properties_When_Using_ConcurrencyChecks_Using_ObservableEntity()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!)).Build();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ObservablePoco).WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Properties.Select(x => x.Name).Should().BeEquivalentTo("MyField", "MyFieldOriginal");
        //TODO: Assert getter and setter statements
    }
}
