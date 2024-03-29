﻿namespace DataFramework.ModelFramework.Tests;

public partial class IntegrationTests
{
    [Fact]
    public void Can_Generate_QueryFieldInfo()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(default).ToQueryFieldInfoClass(settings);

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

namespace QueryFieldProviders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Queries.QueryFieldInfoGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityQueryFieldInfo : QueryFramework.SqlServer.Abstractions.IQueryFieldInfo
    {
        public System.Collections.Generic.IEnumerable<string> GetAllFields()
        {
            yield return @""Id"";
            yield return @""Name"";
            yield return @""Description"";
        }

        public string? GetDatabaseFieldName(string queryFieldName)
        {
            return GetAllFields().FirstOrDefault(x => x.Equals(queryFieldName, StringComparison.OrdinalIgnoreCase));
        }
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_QueryFieldInfo_With_Custom_Stuff()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfoWithCustomQueryFieldProviderStuff().ToQueryFieldInfoClass(settings);

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

namespace GeneratedNamespace
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Queries.QueryFieldInfoGenerator"", @""1.0.0.0"")]
    public partial class TestEntityQueryFieldInfo : QueryFramework.SqlServer.Abstractions.IQueryFieldInfo
    {
        public System.Collections.Generic.IEnumerable<string> GetAllFields()
        {
            yield return @""Id"";
            yield return @""Name"";
            yield return @""Description"";
            yield return @""CustomExtraField"";
            yield return @""AllFields"";
        }

        public string? GetDatabaseFieldName(string queryFieldName)
        {
            var extraField = _extraFields.FirstOrDefault(x => x.Name == queryFieldName);
            if (extraField != null)
            {
                return string.Format(""ExtraField{0}"", extraField.FieldNumber);
            }
            if (queryFieldName == ""AllFields"")
            {
                return ""[Name] + ' ' + COALESCE([Description], '')"";
            }
            return GetAllFields().FirstOrDefault(x => x.Equals(queryFieldName, StringComparison.OrdinalIgnoreCase));
        }

        public TestEntityQueryFieldInfo(IEnumerable<ExtraField> extraFields)
        {
            _extraFields = extraFields;
        }

        private readonly IEnumerable<ExtraField> _extraFields;
    }
#nullable restore
}
");
    }
}
