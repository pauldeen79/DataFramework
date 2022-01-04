using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;
using DataFramework.Core;
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
        public void CanGenerateImmutableBuilderClassesForEntities()
        {
            // Arrange
            var models = typeof(DataObjectInfo).Assembly.GetExportedTypes()
                .Where(t => t.FullName?.StartsWith("DataFramework.Core.") == true && t.FullName?.Contains("Builders.") != true)
                .Select(t => t.ToClassBuilder(new ClassSettings(createConstructors: true)).WithName(t.Name).WithNamespace(GetNamespace(t)))
                .ToArray();

            FixImmutableBuilderProperties(models);
            var settings = new ImmutableBuilderClassSettings(constructorSettings: new ImmutableBuilderClassConstructorSettings(addCopyConstructor: true),
                                                             formatInstanceTypeNameDelegate: FormatInstanceTypeName);

            // Act
            var builderModels = models.Select(c => c.Build()
                                                    .ToImmutableBuilderClassBuilder(settings)
                                                    .WithNamespace("DataFramework.Core.Builders")
                                                    .WithPartial()
                                                    .AddExcludeFromCodeCoverageAttribute()
                                                    .Chain(x => { if (x.Name != "MetadataBuilder") { x.AddMethods(CreateAddMetadataOverload(x)); } })
                                                    .Build()).ToArray();
            var sut = new CSharpClassGenerator();
            var actual = TemplateRenderHelper.GetTemplateOutput(sut, builderModels, additionalParameters: new { EnableNullableContext = true, CreateCodeGenerationHeader = true });

            actual.NormalizeLineEndings().Should().NotBeNullOrEmpty().And.NotStartWith("Error:");
        }

        private static void FixImmutableBuilderProperties(ClassBuilder[] models)
        {
            foreach (var classBuilder in models)
            {
                foreach (var property in classBuilder.Properties)
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
                        property.ConvertCollectionPropertyToBuilderOnBuider
                        (
                            typeName.Replace("Abstractions.I", "Core.Builders.", StringComparison.InvariantCulture).ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture),
                            null
                        );
                    }
                    else if (typeName == typeof(bool).FullName || typeName == typeof(bool?).FullName)
                    {
                        property.SetDefaultArgumentValueForWithMethod(true);
                        if (_propertiesWithDefaultValueTrue.Contains(property.Name))
                        {
                            property.SetDefaultValueForBuilderClassConstructor(new Literal("true"));
                        }
                    }
                    else if (property.Name == "TypeName" && property.TypeName == typeof(string).FullName)
                    {
                        property.AddBuilderOverload("WithType", typeof(Type), "type", "{2} = type?.AssemblyQualifiedName;");
                    }
                }
            }
        }

        private static readonly string[] _propertiesWithDefaultValueTrue = new[]
        {
            nameof(IFieldInfo.IsVisible),
            nameof(IFieldInfo.IsPersistable),
            nameof(IFieldInfo.CanGet),
            nameof(IFieldInfo.CanSet),
            nameof(IDataObjectInfo.IsQueryable),
            nameof(IDataObjectInfo.IsVisible)
        };
        private static string GetNamespace(Type t)
            => t.FullName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty;

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

        private static ClassMethodBuilder CreateAddMetadataOverload(ClassBuilder c)
            => new ClassMethodBuilder()
                .WithName("AddMetadata")
                .WithTypeName($"{c.Name}")
                .AddParameter("name", typeof(string))
                .AddParameters(new ParameterBuilder().WithName("value").WithType(typeof(object)).WithIsNullable())
                .AddLiteralCodeStatements($"AddMetadata(new {typeof(MetadataBuilder).FullName}().WithName(name).WithValue(value));",
                                          "return this;");

    }
}
