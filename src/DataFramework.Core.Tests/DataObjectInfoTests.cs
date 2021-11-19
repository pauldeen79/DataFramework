﻿using System;
using System.Diagnostics.CodeAnalysis;
using DataFramework.Core.Builders;
using FluentAssertions;
using Xunit;

namespace DataFramework.Core.Tests
{
    [ExcludeFromCodeCoverage]
    public class DataObjectInfoTests
    {
        [Fact]
        public void Ctor_Throws_On_Empty_Name()
        {
            // Arrange
            var action = new Action(() => new DataObjectInfoBuilder().WithName(string.Empty).Build());

            // Act & Assert
            action.Should().ThrowExactly<ArgumentNullException>().WithParameterName("name");
        }
    }
}
