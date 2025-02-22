namespace DataFramework.Domain.Tests.Builders;

public class FieldInfoBuilderTests : TestBase<FieldInfoBuilder>
{
    public class WithType : FieldInfoBuilderTests
    {
        [Fact]
        public void Throws_On_Null_Type()
        {
            // Arrange
            var sut = CreateSut();

            // Act & Assert
            Action a = () => sut.WithType(type: null!);
            a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("type");
        }

        [Fact]
        public void Sets_AssemblyQualifiedName_Correctly_When_Type_Is_Not_Null()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            sut.WithType(typeof(string));

            // Assert
            sut.TypeName.ShouldBe(typeof(string).AssemblyQualifiedName);
        }
    }
}
