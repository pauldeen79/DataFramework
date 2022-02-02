namespace DataFramework.ModelFramework.Tests.Extensions;

public class ListOfMetadataBuilderExtensionsTests
{
    [Fact]
    public void Replace_Adds_Metadata_When_Value_Is_Not_Null()
    {
        // Arrange
        var sut = new List<MetadataBuilder>();

        // Act
        var actual = sut.Replace("MyKey", "value");

        // Assert
        actual.Should().ContainSingle();
        actual.First().Value.Should().Be("value");
    }

    [Fact]
    public void Replace_Does_Not_Add_Metadata_When_Value_Is_Null()
    {
        // Arrange
        var sut = new List<MetadataBuilder>();

        // Act
        var actual = sut.Replace("MyKey", null);

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void Replace_Removes_Existing_Metadata()
    {
        // Arrange
        var sut = new List<MetadataBuilder>();
        sut.Add(new MetadataBuilder().WithName("MyKey").WithValue("OldValue"));

        // Act
        var actual = sut.Replace("MyKey", "NewValue");

        // Assert
        actual.Should().ContainSingle();
        actual.First().Value.Should().Be("NewValue");
    }
}
