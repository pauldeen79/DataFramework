using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CrossCutting.Common;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;
using DataFramework.Core.Builders;
using FluentAssertions;
using ModelFramework.Common;
using ModelFramework.Common.Extensions;
using ModelFramework.Generators.Objects;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;
using ModelFramework.Objects.Extensions;
using ModelFramework.Objects.Settings;
using TextTemplateTransformationFramework.Runtime;
using Xunit;

namespace DataFramework.CodeGeneration.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTests
    {
        [Fact]
        public void CanGenerateImmutableBuilderClassesForContracts()
        {
            // Arrange
            var settings = new ImmutableClassSettings(newCollectionTypeName: typeof(ValueCollection<>).WithoutGenerics(),
                                                      validateArgumentsInConstructor: true);
            var model = new[]
            {
                typeof(IMetadata),
                typeof(IDataObjectInfo),
                typeof(IFieldInfo)
            }.Select(x => CreateBuilder(x.ToClassBuilder(new ClassSettings())
                           .WithName(x.Name.Substring(1))
                           .WithNamespace("DataFramework.Core")
                           .Chain(x => FixImmutableBuilderProperties(x))
                           .Build()
                           .ToImmutableClass(settings), "DataFramework.Core.Builders")
                           .Build()
                    ).ToArray();

            // Act
            var actual = CreateCode(model);

            // Assert
            actual.NormalizeLineEndings().Should().NotBeNullOrEmpty().And.NotStartWith("Error:");
        }

        [Fact]
        public void CanGenerateRecordsForContracts()
        {
            // Arrange
            var settings = new ImmutableClassSettings(newCollectionTypeName: typeof(ValueCollection<>).WithoutGenerics(),
                                                      validateArgumentsInConstructor: true);
            var model = new[]
            {
                typeof(IMetadata),
                typeof(IDataObjectInfo),
                typeof(IFieldInfo)
            }.Select(x => x.ToClassBuilder(new ClassSettings())
                           .WithName(x.Name.Substring(1))
                           .WithNamespace("DataFramework.Core")
                           .Chain(x => FixImmutableBuilderProperties(x))
                           .Build()
                           .ToImmutableClassBuilder(settings)
                           .WithRecord()
                           .WithPartial()
                           .AddInterfaces(x)
                           .Build()
                    ).ToArray();

            // Act
            var actual = CreateCode(model);

            // Assert
            actual.NormalizeLineEndings().Should().NotBeNullOrEmpty().And.NotStartWith("Error:");
        }

        private static void FixImmutableBuilderProperties(ClassBuilder model)
        {
            foreach (var property in model.Properties)
            {
                var typeName = property.TypeName.FixTypeName();
                if (typeName.StartsWith("DataFramework.Abstractions.I", StringComparison.InvariantCulture))
                {
                    property.ConvertSinglePropertyToBuilderOnBuilder
                    (
                        typeName.Replace("Abstractions.I", "Core.Builders.", StringComparison.InvariantCulture) + "Builder"
                    );
                }
                else if (typeName.Contains("Collection<DataFramework."))
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        "CrossCutting.Common.ValueCollection",
                        typeName.Replace("Abstractions.I", "Core.Builders.", StringComparison.InvariantCulture).ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture),
                        null
                    );
                }
                else if (typeName.IsBooleanTypeName() || typeName.IsNullableBooleanTypeName())
                {
                    property.SetDefaultArgumentValueForWithMethod(true);
                    if (_propertiesWithDefaultValueTrue.Contains(property.Name))
                    {
                        property.SetDefaultValueForBuilderClassConstructor(new Literal("true"));
                    }
                }
                else if (property.Name == "TypeName" && property.TypeName.IsStringTypeName())
                {
                    property.AddBuilderOverload("WithType", typeof(Type), "type", "{2} = type?.AssemblyQualifiedName;");
                }
            }
        }

        private static ClassBuilder CreateBuilder(IClass c, string @namespace)
            => c.ToImmutableBuilderClassBuilder(new ImmutableBuilderClassSettings(constructorSettings: new ImmutableBuilderClassConstructorSettings(addCopyConstructor: true),
                                                formatInstanceTypeNameDelegate: FormatInstanceTypeName))
                .WithNamespace(@namespace)
                .WithPartial()
                .AddMethods(CreateExtraOverloads(c));

        private static readonly string[] _propertiesWithDefaultValueTrue = new[]
        {
            nameof(IFieldInfo.IsVisible),
            nameof(IFieldInfo.IsPersistable),
            nameof(IFieldInfo.CanGet),
            nameof(IFieldInfo.CanSet),
            nameof(IDataObjectInfo.IsQueryable),
            nameof(IDataObjectInfo.IsVisible)
        };

        private static string FormatInstanceTypeName(ITypeBase instance, bool forCreate)
        {
            if (instance.Namespace == "DataFramework.Core")
            {
                return forCreate
                    ? "DataFramework.Core." + instance.Name
                    : "DataFramework.Abstractions.I" + instance.Name;
            }

            return string.Empty;
        }

        private static IEnumerable<ClassMethodBuilder> CreateExtraOverloads(IClass c)
        {
            if (c.Properties.Any(p => p.Name == "Metadata"))
            {
                yield return new ClassMethodBuilder()
                    .WithName("AddMetadata")
                    .WithTypeName($"{c.Name}Builder")
                    .AddParameter("name", typeof(string))
                    .AddParameters(new ParameterBuilder().WithName("value").WithType(typeof(object)).WithIsNullable())
                    .AddLiteralCodeStatements($"AddMetadata(new {typeof(MetadataBuilder).FullName}().WithName(name).WithValue(value));",
                                                "return this;");
            }
        }

        private static string CreateCode(ITypeBase[] builderModels)
            => TemplateRenderHelper.GetTemplateOutput(new CSharpClassGenerator(),
                                                      builderModels,
                                                      additionalParameters: new
                                                      {
                                                          EnableNullableContext = true,
                                                          CreateCodeGenerationHeader = true
                                                      });
    }
}
