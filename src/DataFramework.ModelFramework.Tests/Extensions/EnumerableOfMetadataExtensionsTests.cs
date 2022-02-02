namespace DataFramework.ModelFramework.Tests.Extensions;

public class EnumerableOfMetadataExtensionsTests
{
    [Fact]
    public void GetMetadataValue_With_Enum_Returns_DefaultValue_When_Not_Specified()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder().WithName("TestEntity").Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.Record);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetMetadataValue_With_Enum_Returns_DefaultValue_When_Specified_As_Empty(string value)
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue(value))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.Record);
    }

    [Fact]
    public void GetMetadataValue_With_Enum_Returns_DefaultValue_When_Specified_Unknown_Value()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue("some unknown value"))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.Record);
    }

    [Fact]
    public void GetMetadataValue_With_Enum_Returns_Correct_Value_When_Specified_As_String()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue(EntityClassType.ObservablePoco.ToString()))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.ObservablePoco);
    }

    [Fact]
    public void GetMetadataValue_With_Enum_Returns_Correct_Value_When_Specified_As_Enum()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue(EntityClassType.ObservablePoco))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.ObservablePoco);
    }

    [Fact]
    public void GetMetadataValue_With_Enum_Returns_Correct_Value_When_Specified_As_Int()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue((int)EntityClassType.ObservablePoco))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.ObservablePoco);
    }

    [Fact]
    public void GetMetadataValue_With_NullableEnum_Returns_DefaultValue_When_Not_Specified()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder().WithName("TestEntity").Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => default(EntityClassType?));

        // Assert
        actual.Should().BeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetMetadataValue_With_NullableEnum_Returns_DefaultValue_When_Specified_As_Empty(string value)
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue(value))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => default(EntityClassType?));

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public void GetMetadataValue_With_NullableEnum_Returns_DefaultValue_When_Specified_Unknown_Value()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue("some unknown value"))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => default(EntityClassType?));

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public void GetMetadataValue_With_NullableEnum_Returns_Correct_Value_When_Specified_As_String()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue(EntityClassType.ObservablePoco.ToString()))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue<EntityClassType?>("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.ObservablePoco);
    }

    [Fact]
    public void GetMetadataValue_With_NullableEnum_Returns_Correct_Value_When_Specified_As_Enum()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue(EntityClassType.ObservablePoco))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue<EntityClassType?>("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.ObservablePoco);
    }

    [Fact]
    public void GetMetadataValue_With_NullableEnum_Returns_Correct_Value_When_Specified_As_Int()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue((int)EntityClassType.ObservablePoco))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue<EntityClassType?>("MyName", () => EntityClassType.Record);

        // Assert
        actual.Should().Be(EntityClassType.ObservablePoco);
    }

    [Fact]
    public void GetMetadataValue_No_Enum_Converts_Value_From_Int_To_String()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue(1))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => default(string));

        // Assert
        actual.Should().Be("1");
    }

    [Fact]
    public void GetMetadataValue_No_Enum_Converts_Value_From_String_To_Int()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddMetadata(new MetadataBuilder().WithName("MyName").WithValue("1"))
            .Build();

        // Act
        var actual = sut.Metadata.GetValue("MyName", () => int.MinValue);

        // Assert
        actual.Should().Be(1);
    }

    [Fact]
    public void GetStringValue_Returns_StringEmpty_When_No_Value_Is_Found_And_DefaultValue_Is_Not_Supplied()
    {
        // Arrange
        var sut = new[] { new MetadataBuilder().WithName("WrongName").WithValue("Value").Build() };

        // Act
        var actual = sut.GetStringValue("Name");

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void GetStringValue_Returns_DefaultValueDelegate_Result_When_No_Value_Is_Found()
    {
        // Arrange
        var sut = new[] { new MetadataBuilder().WithName("WrongName").WithValue("Value").Build() };

        // Act
        var actual = sut.GetStringValue("Name", () => "DelegateResult");

        // Assert
        actual.Should().Be("DelegateResult");
    }

    [Fact]
    public void GetStringValue_Returns_String_When_Found_Value_Is_String()
    {
        // Arrange
        var sut = new[] { new MetadataBuilder().WithName("Name").WithValue("Value").Build() };

        // Act
        var actual = sut.GetStringValue("Name");

        // Assert
        actual.Should().Be("Value");
    }

    [Fact]
    public void GetStringValue_Returns_String_When_Found_Value_Is_Not_String()
    {
        // Arrange
        var sut = new[] { new MetadataBuilder().WithName("Name").WithValue(false).Build() };

        // Act
        var actual = sut.GetStringValue("Name");

        // Assert
        actual.Should().Be(false.ToString());
    }

    [Fact]
    public void GetStringValue_Returns_String_When_Found_Value_Is_Null()
    {
        // Arrange
        var sut = new[] { new MetadataBuilder().WithName("Name").WithValue(null).Build() };

        // Act
        var actual = sut.GetStringValue("Name", "DefaultValue");

        // Assert
        actual.Should().Be("DefaultValue");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GetBooleanValue_Returns_Value_When_Found(bool value)
    {
        // Arrange
        var sut = new[] { new MetadataBuilder().WithName("Name").WithValue(value).Build() };

        // Act
        var actual = sut.GetBooleanValue("Name");

        // Assert
        actual.Should().Be(value);
    }

    [Fact]
    public void GetBooleanValue_Returns_DefaultValue_When_Not_Found()
    {
        // Arrange
        var sut = new[] { new MetadataBuilder().WithName("WrongName").WithValue(false).Build() };

        // Act
        var actual = sut.GetBooleanValue("Name", true);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void GetBooleanValue_Returns_DefaultValueDelegate_Result_When_Not_Found()
    {
        // Arrange
        var sut = new[] { new MetadataBuilder().WithName("WrongName").WithValue(false).Build() };

        // Act
        var actual = sut.GetBooleanValue("Name", () => true);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void GetStringValues_Returns_All_Data_When_Name_Is_Found()
    {
        // Arrange
        var sut = new[]
        {
            new MetadataBuilder().WithName("Name").WithValue("Value1").Build(),
            new MetadataBuilder().WithName("Name").WithValue("Value2").Build(),
            new MetadataBuilder().WithName("Name").WithValue(3).Build()
        };

        // Act
        var actual = sut.GetStringValues("Name");

        // Assert
        actual.Should().BeEquivalentTo(new[] { "Value1", "Value2", "3" });
    }

    [Fact]
    public void GetValues_Returns_All_Data_Of_Correct_Type_When_Name_Is_Found()
    {
        // Arrange
        var sut = new[]
        {
            new MetadataBuilder().WithName("Name").WithValue("Value1").Build(),
            new MetadataBuilder().WithName("Name").WithValue("Value2").Build(),
            new MetadataBuilder().WithName("Name").WithValue(3).Build()
        };

        // Act
        var actual = sut.GetValues<string>("Name");

        // Assert
        actual.Should().BeEquivalentTo(new[] { "Value1", "Value2" });
    }

    [Fact]
    public void GetValues_Returns_Empty_Sequence_When_Name_Is_Not_Foudn()
    {
        // Arrange
        var sut = new[]
        {
            new MetadataBuilder().WithName("WrongName").WithValue("Value1").Build(),
            new MetadataBuilder().WithName("WrongName").WithValue("Value2").Build(),
            new MetadataBuilder().WithName("WrongName").WithValue(3).Build()
        };

        // Act
        var actual = sut.GetValues<string>("Name");

        // Assert
        actual.Should().BeEmpty();
    }
}
