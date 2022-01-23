using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Common;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;
using DataFramework.Core.Builders;
using ModelFramework.CodeGeneration.CodeGenerationProviders;
using ModelFramework.Common;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;
using ModelFramework.Objects.Extensions;

namespace ModelFramework.CodeGeneration.Tests.CodeGenerationProviders
{
    public abstract class DataFrameworkCSharpClassBase : CSharpClassBase
    {
        protected override bool CreateCodeGenerationHeader => true;
        protected override bool EnableNullableContext => true;
        protected override Type RecordCollectionType => typeof(ValueCollection<>);

        protected IClass[] GetCodeStatementBuilderClasses(Type codeStatementType, Type codeStatementInterfaceType, Type codeStatementBuilderInterfaceType, string buildersNamespace)
            => GetClassesFromSameNamespace(codeStatementType)
                .Select
                (
                    c => CreateBuilder(c, buildersNamespace)
                        .AddInterfaces(codeStatementBuilderInterfaceType)
                        .Chain(x => x.Methods.First(x => x.Name == "Build").WithType(codeStatementInterfaceType))
                        .Build()
                ).ToArray();

        protected override string FormatInstanceTypeName(ITypeBase instance, bool forCreate)
        {
            if (instance.Namespace == "DataFramework.Core")
            {
                return forCreate
                    ? "DataFramework.Core." + instance.Name
                    : "DataFramework.Abstractions.I" + instance.Name;
            }

            return string.Empty;
        }

        protected override void FixImmutableBuilderProperties(ClassBuilder classBuilder)
        {
            foreach (var property in classBuilder.Properties)
            {
                var typeName = property.TypeName.FixTypeName();
                if (typeName.StartsWith("DataFramework.Abstractions.I", StringComparison.InvariantCulture))
                {
                    property.ConvertSinglePropertyToBuilderOnBuilder
                    (
                        typeName.Replace("Abstractions.I", "Core.Builders.") + "Builder"
                    );
                }
                else if (typeName.Contains("Collection<DataFramework."))
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        typeof(ValueCollection<>).WithoutGenerics(),
                        typeName.Replace("Abstractions.I", "Core.Builders.").ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture),
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

        protected override IEnumerable<ClassMethodBuilder> CreateExtraOverloads(IClass c)
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

        protected static Type[] GetDataFrameworkModelTypes()
            => new[]
            {
                typeof(IDataObjectInfo),
                typeof(IFieldInfo),
                typeof(IMetadata)
            };

        private static readonly string[] _propertiesWithDefaultValueTrue = new[]
        {
            nameof(IFieldInfo.IsVisible),
            nameof(IFieldInfo.IsPersistable),
            nameof(IFieldInfo.CanGet),
            nameof(IFieldInfo.CanSet),
            nameof(IDataObjectInfo.IsQueryable),
            nameof(IDataObjectInfo.IsVisible)
        };
    }
}
