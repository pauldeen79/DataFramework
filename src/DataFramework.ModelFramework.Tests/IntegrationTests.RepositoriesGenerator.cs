﻿namespace DataFramework.ModelFramework.Tests;

public partial class IntegrationTests
{
    [Fact]
    public void Can_Generate_Repository()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(default).ToRepositoryClass(settings);

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

namespace Repositories
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.RepositoryGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityRepository : CrossCutting.Data.Core.Repository<Entities.TestEntity,EntityIdentities.TestEntityIdentity>, IMyRepository, Contracts.Repositories.ITestEntityRepository
    {
        public TestEntityRepository(CrossCutting.Data.Abstractions.IDatabaseCommandProcessor<Entities.TestEntity> commandProcessor, CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<Entities.TestEntity> entityRetriever, CrossCutting.Data.Abstractions.IDatabaseCommandProvider<Entities.TestEntity> identitySelectCommandProvider, CrossCutting.Data.Abstractions.IPagedDatabaseCommandProvider pagedEntitySelectCommandProvider, CrossCutting.Data.Abstractions.IDatabaseCommandProvider entitySelectCommandProvider, CrossCutting.Data.Abstractions.IDatabaseCommandProvider<Entities.TestEntity> entityCommandProvider) : base(commandProcessor, entityRetriever, identitySelectCommandProvider, pagedEntitySelectCommandProvider, entitySelectCommandProvider, entityCommandProvider)
        {
        }
    }
#nullable restore
}
");
    }
}
