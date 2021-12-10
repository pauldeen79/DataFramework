using System;
using System.Diagnostics.CodeAnalysis;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using DataFramework.ModelFramework.MetadataNames;
using DataFramework.ModelFramework.Tests.TestFixtures;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class FieldInfoExtensionsTests
    {
        [Fact]
        public void CreatePropertyName_Returns_Default_DeconflictionName_When_Name_Is_Equal_To_Entity_And_No_Custom_DeconflictionName_Is_Found()
        {
            // Arrange
            var fieldInfo = new FieldInfoBuilder().WithName("Name").Build();
            var dataObjectInfo = new DataObjectInfoBuilder().WithName("Name").Build();

            // Act
            var actual = fieldInfo.CreatePropertyName(dataObjectInfo);

            // Assert
            actual.Should().Be("NameProperty");
        }

        [Fact]
        public void CreatePropertyName_Returns_Custom_DeconflictionName_When_Name_Is_Equal_To_Entity_And_Custom_DeconflictionName_Is_Found()
        {
            // Arrange
            var fieldInfo = new FieldInfoBuilder().WithName("Name").Build();
            var dataObjectInfo = new DataObjectInfoBuilder().WithName("Name").AddMetadata(Shared.PropertyNameDeconflictionFormatString, "{0}Custom").Build();

            // Act
            var actual = fieldInfo.CreatePropertyName(dataObjectInfo);

            // Assert
            actual.Should().Be("NameCustom");
        }

        [Fact]
        public void CreatePropertyName_Returns_Original_Name_When_Name_Is_Not_Equal_To_Entity()
        {
            // Arrange
            var fieldInfo = new FieldInfoBuilder().WithName("Unchanged").Build();
            var dataObjectInfo = new DataObjectInfoBuilder().WithName("Entity").Build();

            // Act
            var actual = fieldInfo.CreatePropertyName(dataObjectInfo);

            // Assert
            actual.Should().Be("Unchanged");
        }

        [Fact]
        public void IsRequired_Returns_True_When_RequiredAttribute_Is_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Field").WithIsRequired().Build();

            // Act
            var actual = sut.IsRequired();

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsRequired_Returns_False_When_RequiredAttribute_Is_Not_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Field").Build();

            // Act
            var actual = sut.IsRequired();

            // Assert
            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void IsRowVersion_Returns_Correct_Value_When_Metadata_Is_Found(bool? metadataValue)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsRowVersion(metadataValue).Build();

            // Act
            var actual = sut.IsRowVersion();

            // Assert
            actual.Should().Be(metadataValue.GetValueOrDefault());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void IsSqlRequired_Returns_Correct_Value_When_Metadata_Is_Found(bool? metadataValue)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsSqlRequired(metadataValue).Build();

            // Act
            var actual = sut.IsSqlRequired();

            // Assert
            actual.Should().Be(metadataValue.GetValueOrDefault());
        }

        [Fact]
        public void IsSqlRequired_Returns_Result_From_IsRequired_When_Metadata_Is_Not_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsRequired().Build();

            // Act
            var actual = sut.IsSqlRequired();

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsSqlRequired_Returns_Result_From_IsNullable_When_Metadata_Is_Not_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsNullable().Build();

            // Act
            var actual = sut.IsSqlRequired();

            // Assert
            actual.Should().BeTrue();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void SkipFieldOnFind_Returns_Correct_Value_When_Metadata_Is_Found(bool? metadataValue)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithSkipFieldOnFind(metadataValue).Build();

            // Act
            var actual = sut.SkipFieldOnFind();

            // Assert
            actual.Should().Be(metadataValue.GetValueOrDefault());
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_MetadataValue_When_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test").WithType(typeof(int)).WithSqlReaderMethodName("MyMethod").Build();

            // Act
            var actual = sut.GetSqlReaderMethodName();

            // Assert
            actual.Should().Be("MyMethod");
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetValue_When_No_Metadata_Is_Available_And_Type_Is_Unknown()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test").WithTypeName("MyUnknownType").Build();

            // Act
            var actual = sut.GetSqlReaderMethodName();

            // Assert
            actual.Should().Be("GetValue");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetSqlReaderMethodName_Returns_GetValue_When_No_Metadata_Is_Available_And_TypeName_Is_(string typeName)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test").WithTypeName(typeName).Build();

            // Act
            var actual = sut.GetSqlReaderMethodName();

            // Assert
            actual.Should().Be("GetValue");
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetInt32_On_Required_Enum_And_IsNullable_Is_False()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test").WithType(typeof(MyEnumeration)).Build();

            // Act
            var actual = sut.GetSqlReaderMethodName();

            // Assert
            actual.Should().Be("GetInt32");
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetNullableInt32_On_Required_Enum_And_IsNullable_Is_True()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test").WithType(typeof(MyEnumeration)).WithIsNullable().Build();

            // Act
            var actual = sut.GetSqlReaderMethodName();

            // Assert
            actual.Should().Be("GetNullableInt32");
        }

        [Fact]
        public void GetSqlReaderMethodName_Returns_GetNullableInt32_On_Optional_Enum()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test").WithType(typeof(MyEnumeration?)).Build();

            // Act
            var actual = sut.GetSqlReaderMethodName();

            // Assert
            actual.Should().Be("GetNullableInt32");
        }

        [Theory]
        [InlineData(typeof(int), false, "GetInt32")]
        [InlineData(typeof(int), true, "GetNullableInt32")]
        [InlineData(typeof(int?), false, "GetNullableInt32")]
        [InlineData(typeof(int?), true, "GetNullableInt32")]
        [InlineData(typeof(string), false, "GetString")]
        [InlineData(typeof(string), true, "GetNullableString")]
        public void GetSqlReaderMethodName_Returns_Correct_Value_When_Type_Is_Known(Type type, bool isNullable, string expectedResult)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Test").WithType(type).WithIsNullable(isNullable).Build();

            // Act
            var actual = sut.GetSqlReaderMethodName();

            // Assert
            actual.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void IsSelectField_Returns_Correct_Value_When_Metadata_Is_Found(bool? isSelectField)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsSelectField(isSelectField).WithIsPersistable(false).Build();

            // Act
            var actual = sut.IsSelectField();

            // Assert
            actual.Should().Be(isSelectField.GetValueOrDefault());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSelectField_Returns_Value_From_IsPersistable_WHen_No_Metadata_Is_Found(bool persistable)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsPersistable(persistable).Build();

            // Act
            var actual = sut.IsSelectField();

            // Assert
            actual.Should().Be(persistable);
        }

        [Fact]
        public void GetDatabaseFieldName_Returns_Value_From_Metadata_When_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithDatabaseFieldName("DatabaseName").Build();

            // Act
            var actual = sut.GetDatabaseFieldName();

            // Assert
            actual.Should().Be("DatabaseName");
        }

        [Fact]
        public void GetDatabaseFieldName_Returns_Name_When_Metadata_Is_Not_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithDatabaseFieldName(null).Build();

            // Act
            var actual = sut.GetDatabaseFieldName();

            // Assert
            actual.Should().Be("Name");
        }

        [Fact]
        public void GetDatabaseFieldAlias_Returns_Value_From_Metadata_When_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithDatabaseFieldAlias("MyAlias").Build();

            // Act
            var actual = sut.GetDatabaseFieldAlias();

            // Assert
            actual.Should().Be("MyAlias");
        }

        [Fact]
        public void GetDatabaseFieldAlias_Returns_FieldName_Metadata_When_FieldAlias_Metadata_Is_Not_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithDatabaseFieldName("DatabaseFieldName").Build();

            // Act
            var actual = sut.GetDatabaseFieldAlias();

            // Assert
            actual.Should().Be("DatabaseFieldName");
        }

        [Fact]
        public void GetDatabaseFieldAlias_Returns_Name_When_FieldAlias_And_FieldName_Metadata_Is_Not_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithDatabaseFieldAlias(null).WithDatabaseFieldName(null).Build();

            // Act
            var actual = sut.GetDatabaseFieldAlias();

            // Assert
            actual.Should().Be("Name");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Metadata_Value_With_Details_When_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithSqlFieldType("varbinary(18)").Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.Should().Be("varbinary(18)");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Metadata_Value_Without_Details_When_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithSqlFieldType("varbinary(18)").Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: false);

            // Assert
            actual.Should().Be("varbinary");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Varchar_With_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true, fieldLength: 10);

            // Assert
            actual.Should().Be("varchar(10)");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Varchar_Without_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: false, fieldLength: 10);

            // Assert
            actual.Should().Be("varchar");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Decimal_With_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal)).WithSqlNumericPrecision(4).WithSqlNumericScale(2).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.Should().Be("decimal(4,2)");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Decimal_Without_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal)).WithSqlNumericPrecision(4).WithSqlNumericScale(2).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: false);

            // Assert
            actual.Should().Be("decimal");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Nullable_Decimal_With_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal?)).WithSqlNumericPrecision(4).WithSqlNumericScale(2).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.Should().Be("decimal(4,2)");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Nullable_Decimal_Without_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal?)).WithSqlNumericPrecision(4).WithSqlNumericScale(2).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: false);

            // Assert
            actual.Should().Be("decimal");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Required_Enum()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(MyEnumeration)).Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.Should().Be("int");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Optional_Enum()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(MyEnumeration?)).Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.Should().Be("int");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Correct_Result_For_Known_Type()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(bool)).Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.Should().Be("bit");
        }

        [Fact]
        public void GetSqlFieldType_Returns_Empty_Result_For_Unknown_Type()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithTypeName("UnknownType").Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.Should().BeEmpty();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetSqlIsStringMaxLength_Returns_Correct_Result_When_Metadata_Available(bool maxLength)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithSqlIsStringMaxLength(maxLength).Build();

            // Act
            var actual = sut.GetSqlIsStringMaxLength();

            // Assert
            actual.Should().Be(maxLength);
        }

        [Fact]
        public void GetSqlIsStringMaxLength_Returns_Correct_Result_When_Metadata_Not_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).Build();

            // Act
            var actual = sut.GetSqlIsStringMaxLength();

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void GetSqlNumericPrecision_Returns_Metadata_Value_When_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal?)).WithSqlNumericPrecision(6).Build();

            // Act
            var actual = sut.GetSqlNumericPrecision();

            // Assert
            actual.Should().Be(6);
        }

        [Fact]
        public void GetSqlNumericPrecision_Returns_Null_When_No_Metadata_Value_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal)).WithSqlNumericPrecision(null).Build();

            // Act
            var actual = sut.GetSqlNumericPrecision();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void GetSqlNumericScale_Returns_Metadata_Value_When_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal)).WithSqlNumericScale(6).Build();

            // Act
            var actual = sut.GetSqlNumericScale();

            // Assert
            actual.Should().Be(6);
        }

        [Fact]
        public void GetSqlNumericScale_Returns_Null_When_No_Metadata_Value_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal?)).WithSqlNumericScale(null).Build();

            // Act
            var actual = sut.GetSqlNumericScale();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void GetSqlStringCollation_Returns_Value_From_Metadata_When_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithSqlStringCollation("Latin1_General_CI_AS").Build();

            // Act
            var actual = sut.GetSqlStringCollation();

            // Assert
            actual.Should().Be("Latin1_General_CI_AS");
        }

        [Fact]
        public void GetSqlStringCollation_Returns_EmptyResult_When_No_Metadata_Is_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).Build();

            // Act
            var actual = sut.GetSqlStringCollation();

            // Assert
            actual.Should().BeEmpty();
        }

        [Fact]
        public void GetCheckConstraintExpression_Returns_Value_From_Metadata_When_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("salary").WithType(typeof(string)).WithCheckConstraintExpression("(salary > 0)").Build();

            // Act
            var actual = sut.GetCheckConstraintExpression();

            // Assert
            actual.Should().Be("(salary > 0)");
        }

        [Fact]
        public void GetCheckConstraintExpression_Returns_EmptyResult_When_No_Metadata_Is_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).Build();

            // Act
            var actual = sut.GetCheckConstraintExpression();

            // Assert
            actual.Should().BeEmpty();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void UseOnInsert_Returns_Correct_Value_When_Metadata_Is_Found(bool? metadataValue)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithUseOnInsert(metadataValue).Build();

            // Act
            var actual = sut.UseOnInsert();

            // Assert
            actual.Should().Be(metadataValue.GetValueOrDefault(sut.IsPersistable));
        }

        [Fact]
        public void UseOnInsert_Returns_False_On_Identity_Column_When_No_Metadata_Is_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsIdentityField().Build();

            // Act
            var actual = sut.UseOnInsert();

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void UseOnInsert_Returns_False_On_Computed_Column_When_No_Metadata_Is_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsComputed().Build();

            // Act
            var actual = sut.UseOnInsert();

            // Assert
            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void UseOnUpdate_Returns_Correct_Value_When_Metadata_Is_Found(bool? metadataValue)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithUseOnUpdate(metadataValue).Build();

            // Act
            var actual = sut.UseOnUpdate();

            // Assert
            actual.Should().Be(metadataValue.GetValueOrDefault(sut.IsPersistable));
        }

        [Fact]
        public void UseOnUpdate_Returns_False_On_Identity_Column_When_No_Metadata_Is_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsIdentityField().Build();

            // Act
            var actual = sut.UseOnUpdate();

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void UseOnUpdate_Returns_False_On_Computed_Column_When_No_Metadata_Is_Found()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithIsComputed().Build();

            // Act
            var actual = sut.UseOnUpdate();

            // Assert
            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void UseOnSelect_Returns_Correct_Value_When_Metadata_Is_Found(bool? metadataValue)
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithUseOnSelect(metadataValue).Build();

            // Act
            var actual = sut.UseOnSelect();

            // Assert
            actual.Should().Be(metadataValue.GetValueOrDefault(sut.IsPersistable));
        }
    }
}
