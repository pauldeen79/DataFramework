﻿using System.Diagnostics.CodeAnalysis;
using CrossCutting.Common.Extensions;
using DataFramework.Core;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using ModelFramework.Generators.Objects;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.CodeStatements.Builders;
using ModelFramework.Objects.Contracts;
using TextTemplateTransformationFramework.Runtime;
using Xunit;

namespace DataFramework.ModelFramework.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTests
    {
        [Theory]
        [InlineData(EntityClassType.ImmutableClass)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void Can_Generate_Entity(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(entityClassType).ToEntityClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(EntityClassType.ImmutableClass)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void Can_Generate_EntityBuilder(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var dataObjectInfo = CreateDataObjectInfo(entityClassType);
            var input = dataObjectInfo.ToEntityBuilderClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(EntityClassType.ImmutableClass)]
        [InlineData(EntityClassType.ObservablePoco)]
        [InlineData(EntityClassType.Poco)]
        [InlineData(EntityClassType.Record)]
        public void Can_Generate_EntityIdentity(EntityClassType entityClassType)
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(entityClassType).ToEntityIdentityClass(settings);

            // Act
            var actual = GenerateCode(input, settings);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Fact]
        public void Can_Generate_Query()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(default(EntityClassType)).ToQueryClass(settings);

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

namespace Queries
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCode(@""DataFramework.ModelFramework.Generators.Queries.QueryGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public partial record TestEntityQuery : QueryFramework.Core.Queries.SingleEntityQuery, IMyQuery
    {
        public override System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            foreach (var validationResult in base.Validate(validationContext))
            {
                yield return validationResult;
            }
            if (Limit.HasValue && Limit.Value > MaxLimit)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Limit exceeds the maximum of "" + MaxLimit, new[] { nameof(Limit), nameof(Limit) });
            }
            foreach (var condition in Conditions)
            {
                if (!IsValidFieldName(condition.Field))
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Invalid field name in conditions: "" + condition.Field.FieldName, new[] { nameof(Conditions), nameof(Conditions) });
                }
                if (!IsValidExpression(condition.Field))
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Invalid expression in conditions: "" + condition.Field, new[] { nameof(Conditions), nameof(Conditions) });
                }
            }
            foreach (var querySortOrder in OrderByFields)
            {
                if (!IsValidFieldName(querySortOrder.Field))
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Invalid field name in order by fields: "" + querySortOrder.Field.FieldName, new[] { nameof(OrderByFields), nameof(OrderByFields) });
                }
                if (!IsValidExpression(querySortOrder.Field))
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Invalid expression in order by fields: "" + querySortOrder.Field, new[] { nameof(OrderByFields), nameof(OrderByFields) });
                }
            }
        }

        private bool IsValidExpression()
        {
            return true;
        }

        private bool IsValidFieldName()
        {
            return expression.FieldName.StartsWith(""ExtraField"") || ValidFieldNames.Any(s => s.Equals(expression.FieldName, StringComparison.OrdinalIgnoreCase));
        }

        public TestEntityQuery(): this(null, null, Enumerable.Empty<QueryFramework.Abstractions.IQueryCondition>(), Enumerable.Empty<QueryFramework.Abstractions.IQuerySortOrder>())
        {
        }

        public TestEntityQuery(System.Nullable<int> limit, System.Nullable<int> offset, System.Collections.Generic.IEnumerable<QueryFramework.Abstractions.IQueryCondition> conditions, System.Collections.Generic.IEnumerable<QueryFramework.Abstractions.IQuerySortOrder> orderByFields): base(limit, offset, conditions, orderByFields)
        {
        }

        public TestEntityQuery(QueryFramework.Abstractions.Queries.ISingleEntityQuery simpleEntityQuery): this(simpleEntityQuery.Limit, simpleEntityQuery.Offset, simpleEntityQuery.Conditions, simpleEntityQuery.OrderByFields
        {
        }

        private static readonly string[] ValidFieldNames = new[] { ""Id"", ""Name"", ""Description"", ""IsExistingEntity"", ""AdditionalValidFieldName"" };

        private const int MaxLimit = int.MaxValue;
    }
#nullable restore
}
");
        }

        [Fact]
        public void Can_Generate_Repository()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(default(EntityClassType)).ToRepositoryClass(settings);

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
    [System.CodeDom.Compiler.GeneratedCode(@""DataFramework.ModelFramework.Generators.Entities.RepositoryGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityRepository : CrossCutting.Data.Core.Repository<Entities.TestEntity, EntityIdentities.TestEntityIdentity>, IMyRepository, Contracts.Repositories.ITestEntityRepository
    {
        public TestEntityRepository(CrossCutting.Data.Abstractions.IDatabaseCommandProcessor<object> commandProcessor, CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<object> entityRetriever, CrossCutting.Data.Abstractions.IDatabaseCommandProvider<object> identitySelectCommandProvider, CrossCutting.Data.Abstractions.IPagedDatabaseCommandProvider pagedEntitySelectCommandProvider, CrossCutting.Data.Abstractions.IDatabaseCommandProvider entitySelectCommandProvider, CrossCutting.Data.Abstractions.IDatabaseCommandProvider<object> entityCommandProvider): base(commandProcessor, entityRetriever, identitySelectCommandProvider, pagedEntitySelectCommandProvider, entitySelectCommandProvider, entityCommandProvider)
        {
        }
    }
#nullable restore
}
");
        }

        [Fact]
        public void Can_Generate_RepositoryInterface()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(default(EntityClassType)).ToRepositoryInterface(settings);

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

namespace Contracts.Repositories
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCode(@""DataFramework.ModelFramework.Generators.Entities.RepositoryGenerator"", @""1.0.0.0"")]
    internal partial interface ITestEntityRepository : CrossCutting.Data.Abstractions.IRepository<Entities.TestEntity, EntityIdentities.TestEntityIdentity>
    {
    }
#nullable restore
}
");
        }

        [Fact]
        public void Can_Generate_IdentityCommandProvider()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(default(EntityClassType)).ToIdentityCommandProviderClass(settings);

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
using CrossCutting.Data.Core.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseCommandProviders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCode(@""DataFramework.ModelFramework.Generators.IdentityCommandProviderGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityIdentityCommandProvider : CrossCutting.Data.Abstractions.IDatabaseCommandProvider<object>
    {
        public void Create(EntityIdentities.TestEntityIdentity source, CrossCutting.Data.Abstractions.DatabaseOperation operation)
        {
            if (operation != DatabaseOperation.Select)
            {
                throw new ArgumentOutOfRangeException(""operation"", ""Only Select operation is supported"");
            }
            return new SelectCommandBuilder()
                .Select(_settings.Fields)
                .From(_settings.TableName)
                .Where(""[Id] = @Id"")
                .AppendParameters(source)
                .Build();
        }

        public TestEntityIdentityCommandProvider()
        {
            _settings = new EntityRetrieverSettings.TestEntityPagedEntityRetrieverSettings();
        }

        private readonly EntityRetrieverSettings.TestEntityPagedEntityRetrieverSettings _settings;
    }
#nullable restore
}
");
        }

        private static string GenerateCode(ITypeBase input, GeneratorSettings settings)
            => TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(),
                                                      new[] { input },
                                                      additionalParameters: new
                                                      {
                                                          EnableNullableContext = settings.EnableNullableContext,
                                                          CreateCodeGenerationHeader = settings.CreateCodeGenerationHeaders,
                                                          EnvironmentVersion = "1.0.0"
                                                      });

        private static DataObjectInfo CreateDataObjectInfo(EntityClassType entityClassType)
            => new DataObjectInfoBuilder()
                .WithName("TestEntity")
                .WithDescription("Description goes here")
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsRequired().WithPropertyType(typeof(long)),
                    new FieldInfoBuilder().WithName("Name").WithType(typeof(string)).WithStringLength(30).WithIsRequired(),
                    new FieldInfoBuilder().WithName("Description").WithType(typeof(string)).WithStringLength(255).WithIsNullable(),
                    new FieldInfoBuilder().WithName("IsExistingEntity").WithType(typeof(bool)).WithIsComputed().AddComputedFieldStatements(new LiteralCodeStatementBuilder().WithStatement("return Id > 0;"))
                )
                .WithEntityClassType(entityClassType)
                .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)

                .WithEntityNamespace("Entities")
                .WithEntityVisibility(Visibility.Internal)
                .AddEntityInterfaces("ITestEntity")
                .AddEntityAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithEntityBuilderNamespace("EntityBuilders")
                .AddEntityBuilderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithEntityIdentityNamespace("EntityIdentities")
                .AddEntityIdentityAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithQueryNamespace("Queries")
                .AddQueryAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .AddQueryInterfaces("IMyQuery")
                .AddQueryValidFieldNames("AdditionalValidFieldName")
                .AddQueryValidFieldNameStatements(new LiteralCodeStatementBuilder().WithStatement(@"return expression.FieldName.StartsWith(""ExtraField"") || ValidFieldNames.Any(s => s.Equals(expression.FieldName, StringComparison.OrdinalIgnoreCase));"))

                .WithRepositoryNamespace("Repositories")
                .WithRepositoryInterfaceNamespace("Contracts.Repositories")
                .WithRepositoryVisibility(Visibility.Internal)
                .AddRepositoryAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))
                .AddRepositoryInterfaces("IMyRepository")

                .WithCommandProviderNamespace("DatabaseCommandProviders")
                .WithCommandProviderVisibility(Visibility.Internal)
                .AddCommandProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithQueryFieldProviderNamespace("QueryFieldProviders")
                .WithQueryFieldProviderVisibility(Visibility.Internal)
                .AddQueryFieldProviderAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .WithEntityRetrieverSettingsNamespace("EntityRetrieverSettings")
                .WithEntityRetrieverSettingsVisibility(Visibility.Internal)
                .AddEntityRetrieverSettingsAttributes(new AttributeBuilder().WithName(typeof(ExcludeFromCodeCoverageAttribute).FullName))

                .Build();
    }
}
