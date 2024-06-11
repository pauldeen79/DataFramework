namespace DataFramework.Domain.Tests;

public class FieldInfoTests
{
    protected FieldInfo CreateSut(string? typeName = null, bool isNullable = false, bool isIdentityField = false) =>
        new FieldInfoBuilder()
            .WithName("MyField")
            .WithTypeName(typeName)
            .WithIsNullable(isNullable)
            .WithIsIdentityField(isIdentityField)
            .Build();

    public class CreatePropertyName : FieldInfoTests
    {
        [Fact]
        public void Throws_On_Null_DataObjectInfo_Parameter()
        {
            // Arrange
            var sut = CreateSut();

            // Act & Assert
            sut.Invoking(x => x.CreatePropertyName(dataObjectInfo: null!))
               .Should().Throw<ArgumentNullException>()
               .WithParameterName("dataObjectInfo");
        }

        [Fact]
        public void Uses_Deconfliction_When_Property_Name_Is_Equal_To_DataObjectInfo_Name()
        {
            // Arrange
            var sut = CreateSut();
            var dataObjectInfo = new DataObjectInfoBuilder().WithName("MyField").Build();

            // Act
            var result = sut.CreatePropertyName(dataObjectInfo);

            // Assert
            result.Should().Be("MyFieldProperty");
        }

        [Fact]
        public void Returns_Property_Name_When_Not_Equal_To_DataObjectInfo_Name()
        {
            // Arrange
            var sut = CreateSut();
            var dataObjectInfo = new DataObjectInfoBuilder().WithName("SomethingElse").Build();

            // Act
            var result = sut.CreatePropertyName(dataObjectInfo);

            // Assert
            result.Should().Be("MyField");
        }
    }

    public class PropertyTypeName : FieldInfoTests
    {
        [Fact]
        public void Returns_Object_On_TypeName_Null()
        {
            // Arrange
            var sut = CreateSut(typeName: null);

            // Act
            var result = sut.PropertyTypeName;

            // Assert
            result.Should().Be("System.Object");
        }

        [Fact]
        public void Returns_Object_On_TypeName_Empty()
        {
            // Arrange
            var sut = CreateSut(typeName: string.Empty);

            // Act
            var result = sut.PropertyTypeName;

            // Assert
            result.Should().Be("System.Object");
        }

        [Fact]
        public void Returns_TypeName_When_Filled()
        {
            // Arrange
            var sut = CreateSut(typeName: "Filled");

            // Act
            var result = sut.PropertyTypeName;

            // Assert
            result.Should().Be("Filled");
        }
    }

    public class IsRequired : FieldInfoTests
    {
        [Theory]
        [InlineData(false, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, false)]
        [InlineData(true, true, true)]
        public void Returns_Correct_Result(bool isNullable, bool isIdentityField, bool expectedResult)
        {
            // Arrange
            var sut = CreateSut(isNullable: isNullable, isIdentityField: isIdentityField);

            // Act
            var result = sut.IsRequired();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
