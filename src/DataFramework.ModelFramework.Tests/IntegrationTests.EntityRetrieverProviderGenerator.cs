﻿namespace DataFramework.ModelFramework.Tests;

public partial class IntegrationTests
{
    [Fact]
    public void Can_Generate_EntityRetrieverProvider()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(default).ToDatabaseEntityRetrieverProviderClass(settings);

        // Act
        var actual = GenerateCode(input, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandEntityProviders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.EntityRetrieverProviderGenerator"", @""1.0.0.0"")]
    public partial class TestEntityEntityRetrieverProvider : QueryFramework.SqlServer.Abstractions.IDatabaseEntityRetrieverProvider
    {
        public bool TryCreate<TResult>(QueryFramework.Abstractions.IQuery query, out CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<TResult>? result)
            where TResult : class
        {
            if (typeof(TResult) == typeof(Entities.TestEntity)
            {
                result = (CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<Entities.TestEntity>)_databaseEntityRetriever;
                return true;
            }
            result = default;
            return false;
        }

        public TestEntityEntityRetrieverProvider(CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<Entities.TestEntity> databaseEntityRetriever)
        {
            _databaseEntityRetriever = databaseEntityRetriever;
        }

        private readonly CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<Entities.TestEntity> _databaseEntityRetriever;
    }
#nullable restore
}
");
    }
}
