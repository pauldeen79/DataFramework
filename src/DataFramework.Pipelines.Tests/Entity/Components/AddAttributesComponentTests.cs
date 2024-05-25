namespace DataFramework.Pipelines.Tests.Entity.Components;

public class AddAttributesComponentTests : TestBase<Pipelines.Entity.Components.AddAttributesComponent>
{
    [Fact]
    public async Task Does_Not_Add_Attributes_When_AddValidationAttributes_Is_Set_To_False()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = CreateSourceModel();
        var settings = new PipelineSettingsBuilder().WithAddComponentModelAttributes(false).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Attributes.Should().BeEmpty();
    }

    [Fact]
    public async Task Adds_VisibleAttribute_When_AddValidationAttributes_Is_Set_To_True()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = CreateSourceModel(isVisible: false);
        var settings = new PipelineSettingsBuilder().WithAddComponentModelAttributes(true).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Attributes.Select(x => x.Name).Should().BeEquivalentTo("System.ComponentModel.Browsable");
    }

    [Fact]
    public async Task Adds_ReadOnlyAttribute_When_AddValidationAttributes_Is_Set_To_True()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = CreateSourceModel(isReadOnly: true);
        var settings = new PipelineSettingsBuilder().WithAddComponentModelAttributes(true).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Attributes.Select(x => x.Name).Should().BeEquivalentTo("System.ComponentModel.ReadOnly");
    }

    [Fact]
    public async Task Adds_DisplayNameAttribute_When_AddValidationAttributes_Is_Set_To_True()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = CreateSourceModel(displayName: "My display name");
        var settings = new PipelineSettingsBuilder().WithAddComponentModelAttributes(true).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Attributes.Select(x => x.Name).Should().BeEquivalentTo("System.ComponentModel.DisplayName");
    }

    [Fact]
    public async Task Adds_DescriptionAttribute_When_AddValidationAttributes_Is_Set_To_True()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = CreateSourceModel(description: "My description");
        var settings = new PipelineSettingsBuilder().WithAddComponentModelAttributes(true).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Attributes.Select(x => x.Name).Should().BeEquivalentTo("System.ComponentModel.Description");
    }

    private static DataObjectInfo CreateSourceModel(
        bool isVisible = true,
        bool isReadOnly = false,
        string displayName = "",
        string description = "")
        => new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .WithIsVisible(isVisible)
            .WithIsReadOnly(isReadOnly)
            .WithDisplayName(displayName)
            .WithDescription(description)
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!))
            .Build();
}
