using System;
using System.Collections.Generic;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.Builders;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static IClass ToIdentityCommandProviderClass(this IDataObjectInfo instance, GeneratorSettings settings)
            => instance.ToIdentityCommandProviderClassBuilder(settings).Build();

        public static ClassBuilder ToIdentityCommandProviderClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
            => new ClassBuilder()
                .AddUsings(typeof(SelectCommandBuilder).FullName.GetNamespaceWithDefault(string.Empty))
                .WithName($"{instance.Name}IdentityCommandProvider")
                .WithNamespace(instance.GetCommandProvidersNamespace())
                .FillFrom(instance)
                .WithVisibility(instance.Metadata.GetValue(CommandProviders.Visibility, () => instance.IsVisible.ToVisibility()))
                .AddInterfaces(typeof(IDatabaseCommandProvider<>).CreateGenericTypeName(instance.GetEntityIdentityFullName()))
                .AddAttributes(GetIdentityCommandProviderClassAttributes(instance))
                .AddFields(new ClassFieldBuilder().WithName("_settings").WithTypeName(instance.GetEntityRetrieverFullName()).WithReadOnly())
                .AddConstructors(new ClassConstructorBuilder().AddLiteralCodeStatements($"_settings = new {instance.GetEntityRetrieverFullName()}();"))
                .AddMethods(new ClassMethodBuilder().WithName(nameof(IDatabaseCommandProvider<object>.Create))
                                                    .AddParameter("source", instance.GetEntityIdentityFullName())
                                                    .AddParameter("operation", typeof(DatabaseOperation))
                                                    .AddLiteralCodeStatements($"if (operation != {typeof(DatabaseOperation).FullName}.{DatabaseOperation.Select})",
                                                                              "{",
                                                                              $@"    throw new {nameof(ArgumentOutOfRangeException)}(""operation"", ""Only Select operation is supported"");",
                                                                              "}",
                                                                              $"return new {nameof(SelectCommandBuilder)}()",
                                                                              "    .Select(_settings.Fields)",
                                                                              "    .From(_settings.TableName)",
                                                                              $"    .Where(\"{GetFindWhereStatement(instance)}\")",
                                                                              "    .AppendParameters(source)",
                                                                              "    .Build();"));

        private static IEnumerable<AttributeBuilder> GetIdentityCommandProviderClassAttributes(IDataObjectInfo instance)
        {
            yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.IdentityCommandProviderGenerator");

            foreach (var attribute in instance.Metadata.GetValues<IAttribute>(CommandProviders.Attribute))
            {
                yield return new AttributeBuilder(attribute);
            }
        }
    }
}
