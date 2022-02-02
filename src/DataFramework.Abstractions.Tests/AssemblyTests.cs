namespace DataFramework.Abstractions.Tests;

public class AssemblyTests
{
    [Fact]
    public void CanGenerateEntitiesForContracts()
    {
        // Arrange
        var settings = new ImmutableClassSettings(newCollectionTypeName: typeof(ValueCollection<>).WithoutGenerics());
        var model = new[]
        {
            typeof(IDataObjectInfo),
            typeof(IFieldInfo),
            typeof(IMetadata)
        }.Select(x => x.ToClassBuilder(new ClassSettings())
                       .Chain(x => x.Attributes.Clear())
                       .WithName(x.Name.Substring(1))
                       .Build()
                       .ToImmutableClassBuilder(settings)
                       .WithRecord()
                       .Build()
                ).Cast<ITypeBase>().ToArray();

        var generator = new CSharpClassGenerator();
        generator.Session = new Dictionary<string, object>
        {
            { "Model", model }
        };
        var builder = new StringBuilder();

        // Act
        generator.Initialize();
        generator.Render(builder);
        var output = builder.ToString();

        // Assert
        output.Should().NotBeEmpty();
    }

    [Fact]
    public void CanGenerateBuildersForContracts()
    {
        // Arrange
        var settings = new ImmutableBuilderClassSettings(newCollectionTypeName: typeof(ValueCollection<>).WithoutGenerics());
        var model = new[]
        {
            typeof(IDataObjectInfo),
            typeof(IFieldInfo),
            typeof(IMetadata)
        }.Select(x => x.ToClassBuilder(new ClassSettings())
                       .Chain(x => x.Attributes.Clear())
                       .WithName(x.Name.Substring(1))
                       .Build()
                       .ToImmutableBuilderClass(settings)
                ).Cast<ITypeBase>().ToArray();

        var generator = new CSharpClassGenerator();
        generator.Session = new Dictionary<string, object>
        {
            { "Model", model }
        };
        var builder = new StringBuilder();

        // Act
        generator.Initialize();
        generator.Render(builder);
        var output = builder.ToString();

        // Assert
        output.Should().NotBeEmpty();
    }
}
