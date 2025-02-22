namespace DataFramework.Pipelines.Tests.Extensions;

public class FieldInfoExtensionsTests
{
    public class IsUpdateConcurrencyCheckField : FieldInfoExtensionsTests
    {
        [Fact]
        public void Returns_False_When_ConcurrencyCheckBehavior_Is_NoFields()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)).Build();

            // Act
            var result = sut.IsUpdateConcurrencyCheckField(ConcurrencyCheckBehavior.NoFields);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_True_When_ConcurrencyCheckBehavior_Is_AllFields()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)).Build();

            // Act
            var result = sut.IsUpdateConcurrencyCheckField(ConcurrencyCheckBehavior.AllFields);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_ConcurrencyCheckBehavior_Is_MarkedFields_And_UseForConcurrencyCheck_Is_False()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)).WithUseForConcurrencyCheck(false).Build();

            // Act
            var result = sut.IsUpdateConcurrencyCheckField(ConcurrencyCheckBehavior.MarkedFields);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Returns_True_When_ConcurrencyCheckBehavior_Is_MarkedFields_And_UseForConcurrencyCheck_Is_True()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)).WithUseForConcurrencyCheck(true).Build();

            // Act
            var result = sut.IsUpdateConcurrencyCheckField(ConcurrencyCheckBehavior.MarkedFields);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_True_When_ConcurrencyCheckBehavior_Is_MarkedFields_And_UseForConcurrencyCheck_Is_False_But_Field_Is_NonComputed_And_Persistable_And_Identity()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)).WithUseForConcurrencyCheck(false).WithIsComputed(false).WithIsPersistable(true).WithIsIdentityField(true).Build();

            // Act
            var result = sut.IsUpdateConcurrencyCheckField(ConcurrencyCheckBehavior.MarkedFields);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_True_When_ConcurrencyCheckBehavior_Is_MarkedFields_And_UseForConcurrencyCheck_Is_False_But_Field_Is_NonComputed_And_Persistable_And_DatabaseIdentity()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)).WithUseForConcurrencyCheck(false).WithIsComputed(false).WithIsPersistable(true).WithIsDatabaseIdentityField(true).Build();

            // Act
            var result = sut.IsUpdateConcurrencyCheckField(ConcurrencyCheckBehavior.MarkedFields);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_ConcurrencyCheckBehavior_Is_MarkedFields_And_UseForConcurrencyCheck_Is_False_But_Field_Is_Computed_And_Persistable_And_Identity()
        {
            // Arrange
            var sut = new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)).WithUseForConcurrencyCheck(false).WithIsComputed(true).WithIsPersistable(true).WithIsIdentityField(true).Build();

            // Act
            var result = sut.IsUpdateConcurrencyCheckField(ConcurrencyCheckBehavior.MarkedFields);

            // Assert
            result.ShouldBeFalse();
        }
    }
}
