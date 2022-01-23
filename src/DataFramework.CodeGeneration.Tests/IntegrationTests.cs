using System.Diagnostics.CodeAnalysis;
using System.IO;
using CrossCutting.Common.Extensions;
using DataFramework.CodeGeneration.CodeGenerationProviders;
using FluentAssertions;
using TextTemplateTransformationFramework.Runtime.CodeGeneration;
using Xunit;

namespace DataFramework.CodeGeneration.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTests
    {
        private static readonly CodeGenerationSettings Settings = new CodeGenerationSettings
        (
            basePath: Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\"),
            generateMultipleFiles: false,
            dryRun: true
        );

        [Fact]
        public void CanGenerateAll()
        {
            Verify(GenerateCode.For<Builders>(Settings));
            Verify(GenerateCode.For<Records>(Settings));
        }

        private void Verify(GenerateCode generatedCode)
        {
            if (Settings.DryRun)
            {
                var actual = generatedCode.GenerationEnvironment.ToString();

                // Assert
                actual.NormalizeLineEndings().Should().NotBeNullOrEmpty().And.NotStartWith("Error:");
            }
        }
    }
}
