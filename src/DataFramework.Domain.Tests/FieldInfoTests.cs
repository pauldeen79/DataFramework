namespace DataFramework.Domain.Tests;

public class FieldInfoTests
{
    private static readonly string StringTypeName = typeof(string).AssemblyQualifiedName!;

    protected FieldInfo CreateSut(
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
        bool? isRequiredInDatabase = null) =>
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
            var sut = new FieldInfoBuilder().WithName("MyField").Build();

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
            var result = sut.IsRequired;

            // Assert
            result.Should().Be(expectedResult);
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
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_False()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: false, isPersistable: false);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True_And_IsIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true, isIdentityField: true);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True_And_IsDatabaseIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true, isDatabaseIdentityField: true);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True_And_IsComputed_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true, isComputed: true);

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnInsert_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnInsert: null, isPersistable: true, typeName: "unknown type");

            // Act
            var result = sut.UseOnInsert;

            // Assert
            result.Should().BeFalse();
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
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_False()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: false, isPersistable: false);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True_And_IsIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true, isIdentityField: true);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True_And_IsDatabaseIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true, isDatabaseIdentityField: true);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True_And_IsComputed_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true, isComputed: true);

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnUpdate_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnUpdate: null, isPersistable: true, typeName: "unknown type");

            // Act
            var result = sut.UseOnUpdate;

            // Assert
            result.Should().BeFalse();
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
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_False()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: false, isPersistable: false);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True_And_IsIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true, isIdentityField: true);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True_And_IsDatabaseIdentityField_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true, isDatabaseIdentityField: true);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True_And_IsComputed_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true, isComputed: true);

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnDelete_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnDelete: null, isPersistable: true, typeName: "unknown type");

            // Act
            var result = sut.UseOnDelete;

            // Assert
            result.Should().BeFalse();
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
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_False()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: false, isPersistable: false);

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_True()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: null, isPersistable: true);

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: null, isPersistable: true, typeName: "unknown type");

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_False_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_False_And_PropertyTypeName_Is_Not_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: null, isPersistable: false, typeName: "unknown type");

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Returns_True_When_OverrideUseOnSelect_Is_Not_Filled_And_IsPersistable_Is_True_And_PropertyTypeName_Is_Supported()
        {
            // Arrange
            var sut = CreateSut(overrideUseOnSelect: null, isPersistable: true, typeName: StringTypeName);

            // Act
            var result = sut.UseOnSelect;

            // Assert
            result.Should().BeTrue();
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
            parameterBuilder.Name.Should().Be("myField");
        }

        [Fact]
        public void Sets_TypeName_Correctly()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var parameterBuilder = sut.ToParameterBuilder(CultureInfo.InvariantCulture);

            // Assert
            parameterBuilder.TypeName.Should().Be("System.String");
        }

        [Fact]
        public void Sets_DefaultValue_Correctly()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var parameterBuilder = sut.ToParameterBuilder(CultureInfo.InvariantCulture);

            // Assert
            parameterBuilder.DefaultValue.Should().Be(sut.DefaultValue);
        }

        [Fact]
        public void Sets_IsNullable_Correctly()
        {
            // Arrange
            var sut = CreateSut(isNullable: true);

            // Act
            var parameterBuilder = sut.ToParameterBuilder(CultureInfo.InvariantCulture);

            // Assert
            parameterBuilder.IsNullable.Should().BeTrue();
        }
    }

    /*    public bool IsSqlRequired()
        => IsRequiredInDatabase ?? IsNullable || IsRequired();
    */

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
            result.Should().BeTrue();
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
            result.Should().Be(expectedResult);
        }
    }
}
