﻿namespace DataFramework.ModelFramework.Tests.Extensions;

public class DataObjectInfoExtensionsTests
{
    [Fact]
    public void GetEntityClassType_Returns_DefaultValue_When_Not_Specified()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder().WithName("TestEntity").Build();

        // Act
        var actual = sut.GetEntityClassType(EntityClassType.ImmutableClass);

        // Assert
        actual.Should().Be(EntityClassType.ImmutableClass);
    }

    [Theory]
    [InlineData(EntityClassType.ImmutableClass)]
    [InlineData(EntityClassType.ObservablePoco)]
    [InlineData(EntityClassType.Poco)]
    [InlineData(EntityClassType.Record)]
    public void GetEntityClassType_Returns_Correct_Value_When_Specified(EntityClassType value)
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .WithEntityClassType(value)
            .Build();

        // Act
        var actual = sut.GetEntityClassType(default);

        // Assert
        actual.Should().Be(value);
    }

    [Fact]
    public void WithAdditionalDataObjectInfos_Returns_Unchanged_Enumerable_When_Not_Found()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .Build();

        // Act
        var actual = sut.WithAdditionalDataObjectInfos();

        // Assert
        actual.Should().BeEquivalentTo(new[] { sut });
    }

    [Fact]
    public void WithAdditionalDataObjectInfos_Returns_Correct_Enumerable_When_Found()
    {
        // Arrange
        var sut = new DataObjectInfoBuilder()
            .WithName("TestEntity")
            .AddAdditionalDataObjectInfos
            (
                new DataObjectInfoBuilder().WithName("FirstAdditional").Build(),
                new DataObjectInfoBuilder().WithName("SecondAdditional").Build()
            )
            .Build();

        // Act
        var actual = sut.WithAdditionalDataObjectInfos();

        // Assert
        actual.Should().HaveCount(3);
        actual.First().Should().Be(sut);
        actual.ElementAt(1).Name.Should().Be("FirstAdditional");
        actual.ElementAt(2).Name.Should().Be("SecondAdditional");
    }

    [Theory]
    [InlineData(true, true, true, ConcurrencyCheckBehavior.MarkedFields, true)]
    [InlineData(true, true, true, ConcurrencyCheckBehavior.AllFields, true)]
    [InlineData(false, false, false, ConcurrencyCheckBehavior.MarkedFields, false)]
    [InlineData(false, false, false, ConcurrencyCheckBehavior.AllFields, true)]
    [InlineData(true, false, false, ConcurrencyCheckBehavior.MarkedFields, false)]
    [InlineData(true, true, false, ConcurrencyCheckBehavior.MarkedFields, true)]
    [InlineData(true, false, true, ConcurrencyCheckBehavior.MarkedFields, true)]
    [InlineData(true, true, true, ConcurrencyCheckBehavior.NoFields, false)]
    [InlineData(false, true, true, ConcurrencyCheckBehavior.MarkedFields, true)]
    [InlineData(false, false, true, ConcurrencyCheckBehavior.MarkedFields, true)]
    [InlineData(false, true, false, ConcurrencyCheckBehavior.MarkedFields, false)]
    public void IsUpdateConcurrencyCheckField_Returns_Correct_Value(bool isPersistable,
                                                                    bool isIdentity,
                                                                    bool useForConcurrencyCheck,
                                                                    ConcurrencyCheckBehavior concurrencyCheckBehavior,
                                                                    bool expectedResult)
    {
        // Arrange
        var fieldInfo = new FieldInfoBuilder()
            .WithName("Test")
            .WithIsPersistable(isPersistable)
            .WithIsIdentityField(isIdentity)
            .WithType(typeof(string))
            .WithUseForConcurrencyCheck(useForConcurrencyCheck);
        var dataObjectInfo = new DataObjectInfoBuilder()
            .WithName("Test")
            .AddFields(fieldInfo)
            .Build();

        // Act
        var actual = dataObjectInfo.IsUpdateConcurrencyCheckField(fieldInfo.Build(), concurrencyCheckBehavior);

        // Asset
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public void GetUpdateConcurrencyCheckFields_Returns_Correct_Value()
    {
        // Arrange
        var dataObjectInfo = new DataObjectInfoBuilder()
            .WithName("Test")
            .AddFields(new FieldInfoBuilder().WithName("Id").WithIsIdentityField(),
                       new FieldInfoBuilder().WithName("Description").WithType(typeof(string)))
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.MarkedFields)
            .Build();

        // Act
        var actual = dataObjectInfo.GetUpdateConcurrencyCheckFields().ToArray();

        // Assert
        actual.Should().ContainSingle();
        actual.First().Name.Should().Be("Id");
    }

    [Fact]
    public void GetConcurrencyCheckBehavior_Returns_MarkedFields_When_No_Specific_Metadata_Is_Available()
    {
        // Arrange
        var dataObjectInfo = new DataObjectInfoBuilder()
            .WithName("Test")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();

        // Act
        var actual = dataObjectInfo.GetConcurrencyCheckBehavior();

        // Assert
        actual.Should().Be(ConcurrencyCheckBehavior.AllFields);
    }
}
