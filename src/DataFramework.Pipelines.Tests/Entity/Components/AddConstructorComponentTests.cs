namespace DataFramework.Pipelines.Tests.Entity.Components;

public class AddConstructorComponentTests : TestBase<Pipelines.Entity.Components.AddConstructorComponent>
{
    [Theory]
    [InlineData(EntityClassType.Poco)]
    [InlineData(EntityClassType.ObservablePoco)]
    public async Task Does_Not_Add_Constructor_When_EntityClassType_Is(EntityClassType entityClassType)
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = CreateSourceModel();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(entityClassType).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Constructors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(EntityClassType.ImmutableClass)]
    [InlineData(EntityClassType.ImmutableRecord)]
    public async Task Adds_Constructor_When_EntityClassType_Is(EntityClassType entityClassType)
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = CreateSourceModel();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(entityClassType).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Constructors.Should().ContainSingle();
        context.Request.Builder.Constructors.Single().Parameters.Select(x => x.Name).Should().BeEquivalentTo("myField");
        context.Request.Builder.Constructors.Single().CodeStatements.Should().AllBeOfType<StringCodeStatementBuilder>();
        context.Request.Builder.Constructors.Single().CodeStatements.OfType<StringCodeStatementBuilder>().Select(x => x.Statement).Should().BeEquivalentTo(
            "this.MyField = myField;",
            "System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);");
    }

    [Fact]
    public async Task Adds_OriginalFields_To_Constructor_Arguments_When_ConcurrencyCheck_Is_Enabled()
    {
        // Arrange
        var sut = CreateSut();
        var sourceModel = CreateSourceModel();
        var settings = new PipelineSettingsBuilder().WithEntityClassType(EntityClassType.ImmutableClass).WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields).Build();
        PipelineContext<EntityContext> context = new PipelineContext<EntityContext>(new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture));

        // Act
        _ = await sut.Process(context, CancellationToken.None);

        // Assert
        context.Request.Builder.Constructors.Should().ContainSingle();
        context.Request.Builder.Constructors.Single().Parameters.Select(x => x.Name).Should().BeEquivalentTo("myField", "myFieldOriginal");
        context.Request.Builder.Constructors.Single().CodeStatements.OfType<StringCodeStatementBuilder>().Select(x => x.Statement).Should().BeEquivalentTo(
            "this.MyField = myField;",
            "this.MyFieldOriginal = myFieldOriginal;",
            "System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);");
    }

    private static DataObjectInfo CreateSourceModel()
        => new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!))
            .Build();
}
