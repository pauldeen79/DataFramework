using System;
using System.Diagnostics.CodeAnalysis;
using DataFramework.ModelFramework.Extensions;
using DataFramework.ModelFramework.Tests.TestFixtures;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class StringExtensionsTests
    {
        [Fact]
        public void FixGenericParameter_Replaces_T_With_Supplied_Name()
        {
            // Arrange
            var input = "System.Collections.Generic.List<T>";

            // Act
            var actual = input.FixGenericParameter("MyEntity");

            // Assert
            actual.Should().Be("System.Collections.Generic.List<MyEntity>");
        }

        [Fact]
        public void IsSupportedByMap_Returns_True_On_Required_Enum()
        {
            // Arrange
            var input = typeof(MyEnumeration).AssemblyQualifiedName;

            // Act
            var actual = input.IsSupportedByMap();

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsSupportedByMap_Returns_True_On_Optional_Enum()
        {
            // Arrange
            var input = typeof(MyEnumeration?).AssemblyQualifiedName;

            // Act
            var actual = input.IsSupportedByMap();

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsSupportedByMap_Returns_False_When_Type_Could_Not_Be_Resolved()
        {
            // Arrange
            var input = "some unknown type";

            // Act
            var actual = input.IsSupportedByMap();

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void IsSupportedByMap_Returns_True_When_Type_Could_Be_Resolved_And_Is_Found_In_Supported_List()
        {
            // Arrange
            var input = typeof(string).FullName;

            // Act
            var actual = input.IsSupportedByMap();

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsSupportedByMap_Returns_False_When_Type_Could_Be_Resolved_And_Is_Not_Found_In_Supported_List()
        {
            // Arrange
            var input = GetType().AssemblyQualifiedName;

            // Act
            var actual = input.IsSupportedByMap();

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetInt32_On_Required_Enum_With_IsNullable_False()
        {
            // Arrange
            var input = typeof(MyEnumeration).AssemblyQualifiedName;

            // Act
            var actual = input.GetSqlReaderMethodName(false);

            // Assert
            actual.Should().Be("GetInt32");
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetNullableInt32_On_Optional_Enum()
        {
            // Arrange
            var input = typeof(MyEnumeration?).AssemblyQualifiedName;

            // Act
            var actual = input.GetSqlReaderMethodName(false);

            // Assert
            actual.Should().Be("GetNullableInt32");
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetNullableInt32_On_Required_Enum_With_IsNullable_True()
        {
            // Arrange
            var input = typeof(MyEnumeration).AssemblyQualifiedName;

            // Act
            var actual = input.GetSqlReaderMethodName(true);

            // Assert
            actual.Should().Be("GetNullableInt32");
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetValue_On_Unknown_Type()
        {
            // Arrange
            var input = "some unknown type";

            // Act
            var actual = input.GetSqlReaderMethodName(false);

            // Assert
            actual.Should().Be("GetValue");
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetValue_On_Unsupported_Type()
        {
            // Arrange
            var input = GetType().AssemblyQualifiedName;

            // Act
            var actual = input.GetSqlReaderMethodName(false);

            // Assert
            actual.Should().Be("GetValue");
        }


        [Theory]
        [InlineData(typeof(string), false, "GetString")]
        [InlineData(typeof(int), false, "GetInt32")]
        [InlineData(typeof(long), false, "GetInt64")]
        [InlineData(typeof(bool), false, "GetBoolean")]
        [InlineData(typeof(string), true, "GetNullableString")]
        [InlineData(typeof(int), true, "GetNullableInt32")]
        [InlineData(typeof(long), true, "GetNullableInt64")]
        [InlineData(typeof(bool), true, "GetNullableBoolean")]
        public void GetSqlReaderMethodName_Returns_Correct_Value_On_Known_Type(Type type, bool isNullable, string expectedResult)
        {
            // Arrange
            var input = type.AssemblyQualifiedName;

            // Act
            var actual = input.GetSqlReaderMethodName(isNullable);

            // Assert
            actual.Should().Be(expectedResult);
        }

        //[Fact]
        //public void CreatePropertyName_Returns_Same_Value_When_Not_Equal_To_Entity()
        //{
        //}

        //[Fact]
        //public void CreatePropertyName_Returns_DeconflictionName_When_Equal_To_Entity()
        //{
        //}
    }
}
