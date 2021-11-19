﻿using System;
using System.Diagnostics.CodeAnalysis;
using DataFramework.Core.Builders;
using FluentAssertions;
using Xunit;

namespace DataFramework.Core.Tests
{
    [ExcludeFromCodeCoverage]
    public class MetadataTests
    {
        [Fact]
        public void Ctor_Throws_On_Empty_Name()
        {
            // Arrange
            var action = new Action(() => new MetadataBuilder().WithName(string.Empty).Build());

            // Act & Assert
            action.Should().ThrowExactly<ArgumentNullException>().WithParameterName("name");
        }
    }
}
