﻿namespace DataFramework.Domain.Tests;

public class FieldInfoTests
{
    private static readonly string StringTypeName = typeof(string).AssemblyQualifiedName!;

    protected static FieldInfo CreateSut(
        string? typeName = null,
        bool isNullable = false,
        bool isIdentityField = false,
        bool? overrideUseOnInsert = null,
        bool? overrideUseOnUpdate = null,
        bool? overrideUseOnDelete = null,
        bool? overrideUseOnSelect = null,
        bool isPersistable = true,
        bool isDatabaseIdentityField = false,
        bool isComputed = false,
        bool? isRequiredInDatabase = null,
        string? databaseReaderMethodName = null) =>
        new FieldInfoBuilder()
            .WithName("MyField")
            .WithTypeName(typeName ?? StringTypeName)
            .WithIsNullable(isNullable)
            .WithIsIdentityField(isIdentityField)
            .WithOverrideUseOnInsert(overrideUseOnInsert)
            .WithOverrideUseOnUpdate(overrideUseOnUpdate)
            .WithOverrideUseOnDelete(overrideUseOnDelete)
            .WithOverrideUseOnSelect(overrideUseOnSelect)
            .WithIsPersistable(isPersistable)
            .WithIsDatabaseIdentityField(isDatabaseIdentityField)
            .WithIsComputed(isComputed)
            .WithIsRequiredInDatabase(isRequiredInDatabase)
            .WithDatabaseReaderMethodName(databaseReaderMethodName)
            .Build();

    public class CreatePropertyName : FieldInfoTests
    {
        [Fact]
        public void Throws_On_Null_DataObjectInfo_Parameter()
        {
            // Arrange
            var sut = CreateSut();

            // Act & Assert
            Action a = () => sut.CreatePropertyName(dataObjectInfo: null!);
            a.ShouldThrow<ArgumentNullException>()
             .ParamName.ShouldBe("dataObjectInfo");
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
            result.ShouldBe("MyFieldProperty");
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
            result.ShouldBe("MyField");
        }
    }

    public class PropertyTypeName : FieldInfoTests
    {
        [Fact]
        public void Returns_Object_On_TypeName_Null()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("MyField").Build();

            // Act
            var result = sut.PropertyTypeName;

            // Assert
            result.ShouldBe("System.Object");
        }

        [Fact]
        public void Returns_Object_On_TypeName_Empty()
        {
            // Arrange
            var sut = CreateSut(typeName: string.Empty);

            // Act
            var result = sut.PropertyTypeName;

            // Assert
            result.ShouldBe("System.Object");
        }

        [Fact]
        public void Returns_TypeName_When_Filled()
        {
            // Arrange
            var sut = CreateSut(typeName: "Filled");

            // Act
            var result = sut.PropertyTypeName;

            // Assert
            result.ShouldBe("Filled");
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
            var result = sut.IsRequired;

            // Assert
            result.ShouldBe(expectedResult);
        }
    }

    public class UseOnInsert : FieldInfoTests
    {
        [Fact]
        public void Returns_OverrideUseOnInsert_When_Filled()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: true, isPersistable: false);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_False()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: false, isPersistable: false);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True_And_IsIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true, isIdentityField: true);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True_And_IsDatabaseIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true, isDatabaseIdentityField: true);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True_And_IsComputed_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true, isComputed: true);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true, typeName: "unknown type");

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.ShouldBeFalse();
        }
    }

    public class UseOnUpdate : FieldInfoTests
    {
        [Fact]
        public void Returns_OverrideUseOnUpdate_When_Filled()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: true, isPersistable: false);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_False()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: false, isPersistable: false);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True_And_IsIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true, isIdentityField: true);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True_And_IsDatabaseIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true, isDatabaseIdentityField: true);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True_And_IsComputed_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true, isComputed: true);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true, typeName: "unknown type");

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.ShouldBeFalse();
        }
    }

    public class UseOnDelete : FieldInfoTests
    {
        [Fact]
        public void Returns_OverrideUseOnDelete_When_Filled()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: true, isPersistable: false);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_False()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: false, isPersistable: false);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True_And_IsIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true, isIdentityField: true);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True_And_IsDatabaseIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true, isDatabaseIdentityField: true);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True_And_IsComputed_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true, isComputed: true);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true, typeName: "unknown type");

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.ShouldBeFalse();
        }
    }

    public class UseOnSelect : FieldInfoTests
    {
        [Fact]
        public void Returns_OverrideUseOnSelect_When_Filled()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: true, isPersistable: false);

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_False()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: false, isPersistable: false);

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: null, isPersistable: true);

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: null, isPersistable: true, typeName: "unknown type");

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_False_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: null, isPersistable: false, typeName: "unknown type");

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: null, isPersistable: true, typeName: StringTypeName);

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.ShouldBeTrue();
        }
    }

    public class ToParameterBuilder : FieldInfoTests
    {
        [Fact]
        public void Sets_Name_Correctly()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var parameterBuilder = sut.ToParameterBuilder(CultureInfo.InvariantCulture);

            // Assert
            parameterBuilder.Name.ShouldBe("myField");
        }

        [Fact]
        public void Sets_TypeName_Correctly()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var parameterBuilder = sut.ToParameterBuilder(CultureInfo.InvariantCulture);

            // Assert
            parameterBuilder.TypeName.ShouldBe("System.String");
        }

        [Fact]
        public void Sets_DefaultValue_Correctly()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var parameterBuilder = sut.ToParameterBuilder(CultureInfo.InvariantCulture);

            // Assert
            parameterBuilder.DefaultValue.ShouldBe(sut.DefaultValue);
        }

        [Fact]
        public void Sets_IsNullable_Correctly()
        {
            // Arrange
            var sut = CreateSut(isNullable: true);

            // Act
            var parameterBuilder = sut.ToParameterBuilder(CultureInfo.InvariantCulture);

            // Assert
            parameterBuilder.IsNullable.ShouldBeTrue();
        }
    }

    public class IsSqlRequired : FieldInfoTests
    {
        [Fact]
        public void Returns_IsRequiredInDatabase_When_Filled()
        {
            // Arrange
            var sut = CreateSut(isRequiredInDatabase: true);

            // Act
            var result = sut.IsSqlRequired;

            // Assert
            result.ShouldBeTrue();
        }

        [Theory]
        [InlineData(false, false, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, true, true)]
        public void Returns_Correct_Result_When_IsRequiredInDatabase_Is_Not_Filled(bool isNullable, bool isIdentityField, bool expectedResult)
        {
            // Arrange
            var sut = CreateSut(isNullable: isNullable, isIdentityField: isIdentityField);

            // Act
            var result = sut.IsSqlRequired;

            // Assert
            result.ShouldBe(expectedResult);
        }
    }

    public class SqlReaderMethodName : FieldInfoTests
    {
        [Fact]
        public void Returns_DatabaseReaderMethodName_When_Filled()
        {
            // Arrange
            var sut = CreateSut(databaseReaderMethodName: "GetMyStuff");

            // Act
            var result = sut.SqlReaderMethodName;

            // Assert
            result.ShouldBe("GetMyStuff");
        }
        
        [Fact]
        public void Returns_SqlReaderMethodName_From_PropertyTypeName_When_DatabaseReaderMethodName_Is_Empty()
        {
            // Arrange
            var sut = CreateSut(databaseReaderMethodName: string.Empty);

            // Act
            var result = sut.SqlReaderMethodName;

            // Assert
            result.ShouldBe("GetString");
        }
    }

    public class GetSqlFieldType : FieldInfoTests
    {
        [Fact]
        public void Returns_Metadata_Value_With_Details_When_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithDatabaseFieldType("varbinary(18)").Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.ShouldBe("varbinary(18)");
        }

        [Fact]
        public void Returns_Metadata_Value_Without_Details_When_Available()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithDatabaseFieldType("varbinary(18)").Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: false);

            // Assert
            actual.ShouldBe("varbinary");
        }

        [Fact]
        public void Returns_Correct_Result_For_Varchar_With_Specific_Details_No_MaxLength()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).Build(); //this uses AssemblyQualifiedName underneaths

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.ShouldBe("varchar(32)");
        }

        [Fact]
        public void Returns_Correct_Result_For_Varchar_With_Specific_Details_No_MaxLength_TypeNameOnly()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithTypeName(typeof(string).FullName).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.ShouldBe("varchar(32)");
        }

        [Fact]
        public void Returns_Correct_Result_For_Varchar_With_Specific_Details_MaxLength_From_Metadata_SqlStringLength()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringMaxLength(16).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.ShouldBe("varchar(16)");
        }

        [Fact]
        public void Returns_Correct_Result_For_Varchar_Without_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: false);

            // Assert
            actual.ShouldBe("varchar");
        }

        [Fact]
        public void Returns_Correct_Result_For_Decimal_With_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal)).WithDatabaseNumericPrecision(4).WithDatabaseNumericScale(2).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.ShouldBe("decimal(4,2)");
        }

        [Fact]
        public void Returns_Correct_Result_For_Decimal_Without_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal)).WithDatabaseNumericPrecision(4).WithDatabaseNumericScale(2).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: false);

            // Assert
            actual.ShouldBe("decimal");
        }

        [Fact]
        public void Returns_Correct_Result_For_Nullable_Decimal_With_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal?)).WithIsNullable().WithDatabaseNumericPrecision(4).WithDatabaseNumericScale(2).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: true);

            // Assert
            actual.ShouldBe("decimal(4,2)");
        }

        [Fact]
        public void Returns_Correct_Result_For_Nullable_Decimal_Without_Specific_Details()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(decimal?)).WithIsNullable().WithDatabaseNumericPrecision(4).WithDatabaseNumericScale(2).Build();

            // Act
            var actual = sut.GetSqlFieldType(includeSpecificProperties: false);

            // Assert
            actual.ShouldBe("decimal");
        }

        [Fact]
        public void Returns_Correct_Result_For_Required_Enum()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(MyEnumeration)).Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.ShouldBe("int");
        }

        [Fact]
        public void Returns_Correct_Result_For_Optional_Enum()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(MyEnumeration?)).WithIsNullable().Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.ShouldBe("int");
        }

        [Fact]
        public void Returns_Correct_Result_For_Known_Type()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithType(typeof(bool)).Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.ShouldBe("bit");
        }

        [Fact]
        public void Returns_Empty_Result_For_Unknown_Type()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithTypeName("UnknownType").Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.ShouldBeEmpty();
        }

        [Fact]
        public void Returns_Empty_Result_For_Empty_Type()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("Name").WithTypeName(string.Empty).Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.ShouldBeEmpty();
        }

        [Fact]
        public void Returns_Empty_Result_For_Null_Type()
        {
            // Arrange
            string? val = null;
            var sut = new FieldInfoBuilder().WithName("Name").WithTypeName(val).Build();

            // Act
            var actual = sut.GetSqlFieldType();

            // Assert
            actual.ShouldBeEmpty();
        }
    }
}
