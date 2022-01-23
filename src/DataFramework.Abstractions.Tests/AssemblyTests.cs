using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using CrossCutting.Common;
using CrossCutting.Common.Extensions;
using FluentAssertions;
using ModelFramework.Generators.Objects;
using ModelFramework.Objects.Contracts;
using ModelFramework.Objects.Extensions;
using ModelFramework.Objects.Settings;
using Xunit;

namespace DataFramework.Abstractions.Tests
{
    [ExcludeFromCodeCoverage]
    public class AssemblyTests
    {
        [Fact]
        public void CanGenerateEntitiesForContracts()
        {
            // Arrange
            var settings = new ImmutableClassSettings(newCollectionTypeName: typeof(ValueCollection<>).WithoutGenerics());
            var model = new[]
            {
                typeof(IDataObjectInfo),
                typeof(IFieldInfo),
                typeof(IMetadata)
            }.Select(x => x.ToClassBuilder(new ClassSettings())
                           .Chain(x => x.Attributes.Clear())
                           .WithName(x.Name.Substring(1))
                           .Build()
                           .ToImmutableClassBuilder(settings)
                           .WithRecord()
                           .Build()
                    ).Cast<ITypeBase>().ToArray();

            var generator = new CSharpClassGenerator();
            generator.Session = new Dictionary<string, object>
            {
                { "Model", model }
            };
            var builder = new StringBuilder();

            // Act
            generator.Initialize();
            generator.Render(builder);
            var output = builder.ToString();

            // Assert
            output.Should().NotBeEmpty();
        }

        [Fact]
        public void CanGenerateBuildersForContracts()
        {
            // Arrange
            var settings = new ImmutableBuilderClassSettings(newCollectionTypeName: typeof(ValueCollection<>).WithoutGenerics());
            var model = new[]
            {
                typeof(IDataObjectInfo),
                typeof(IFieldInfo),
                typeof(IMetadata)
            }.Select(x => x.ToClassBuilder(new ClassSettings())
                           .Chain(x => x.Attributes.Clear())
                           .WithName(x.Name.Substring(1))
                           .Build()
                           .ToImmutableBuilderClass(settings)
                    ).Cast<ITypeBase>().ToArray();

            var generator = new CSharpClassGenerator();
            generator.Session = new Dictionary<string, object>
            {
                { "Model", model }
            };
            var builder = new StringBuilder();

            // Act
            generator.Initialize();
            generator.Render(builder);
            var output = builder.ToString();

            // Assert
            output.Should().NotBeEmpty();
        }
    }
}
