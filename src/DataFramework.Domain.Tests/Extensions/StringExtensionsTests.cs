namespace DataFramework.Domain.Tests.Extensions;

public class StringExtensionsTests
{
    public class IsSupportedByMap
    {
        [Fact]
        public void Returns_True_For_Required_Enum()
        {
            // Arrange
            var typeString = typeof(MyEnumeration).AssemblyQualifiedName!;

            // Act
            var result = typeString.IsSupportedByMap();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_True_For_Optional_Enum()
        {
            // Arrange
            var typeString = typeof(MyEnumeration?).AssemblyQualifiedName!;

            // Act
            var result = typeString.IsSupportedByMap();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_True_For_Type_In_TypeMappings()
        {
            // Arrange
            var typeString = typeof(string).AssemblyQualifiedName!;

            // Act
            var result = typeString.IsSupportedByMap();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_For_Type_Not_In_TypeMappings()
        {
            // Arrange
            var typeString = GetType().AssemblyQualifiedName!;

            // Act
            var result = typeString.IsSupportedByMap();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_For_Empty_Type()
        {
            // Arrange
            var typeString = string.Empty;

            // Act
            var result = typeString.IsSupportedByMap();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_For_Null_Type()
        {
            // Arrange
            var typeString = default(string?)!;

            // Act
            var result = typeString.IsSupportedByMap();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_For_Unknown_Type()
        {
            // Arrange
            var typeString = "some type that cannot be resolved";

            // Act
            var result = typeString.IsSupportedByMap();

            // Assert
            result.Should().BeFalse();
        }
    }

    public class GetSqlReaderMethodName
    {
        [Fact]
        public void Returns_GetInt32_On_Non_Nullable_Required_Enum()
        {
            // Arrange
            var typeString = typeof(MyEnumeration).AssemblyQualifiedName!;

            // Act
            var result = typeString.GetSqlReaderMethodName(false);

            // Assert
            result.Should().Be("GetInt32");
        }

        [Fact]
        public void Returns_GetNullableInt32_On_Nullable_Required_Enum()
        {
            // Arrange
            var typeString = typeof(MyEnumeration).AssemblyQualifiedName!;

            // Act
            var result = typeString.GetSqlReaderMethodName(true);

            // Assert
            result.Should().Be("GetNullableInt32");
        }

        [Fact]
        public void Returns_GetNullableInt32_On_Optional_Enum()
        {
            // Arrange
            var typeString = typeof(MyEnumeration?).AssemblyQualifiedName!;

            // Act
            var result = typeString.GetSqlReaderMethodName(false);

            // Assert
            result.Should().Be("GetNullableInt32");
        }

        [Fact]
        public void Returns_GetValue_On_Unknown_Type()
        {
            // Arrange
            var typeString = "some unknown type";

            // Act
            var result = typeString.GetSqlReaderMethodName(false);

            // Assert
            result.Should().Be("GetValue");
        }

        [Fact]
        public void Returns_GetValue_On_Type_Not_In_TypeMappings()
        {
            // Arrange
            var typeString = typeof(Version).AssemblyQualifiedName!;

            // Act
            var result = typeString.GetSqlReaderMethodName(false);

            // Assert
            result.Should().Be("GetValue");
        }

        [Fact]
        public void Returns_Nullable_MethodName_On_Nullable_Type()
        {
            // Arrange
            var typeString = typeof(bool).AssemblyQualifiedName!;

            // Act
            var result = typeString.GetSqlReaderMethodName(true);

            // Assert
            result.Should().Be("GetNullableBoolean");
        }

        [Fact]
        public void Returns_Non_Nullable_MethodName_On_Non_Nullable_Type()
        {
            // Arrange
            var typeString = typeof(bool).AssemblyQualifiedName!;

            // Act
            var result = typeString.GetSqlReaderMethodName(false);

            // Assert
            result.Should().Be("GetBoolean");
        }
    }
}
