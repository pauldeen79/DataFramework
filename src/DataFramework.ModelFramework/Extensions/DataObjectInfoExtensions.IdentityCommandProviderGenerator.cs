using System.Collections.Generic;
using System.Linq;
using CrossCutting.Data.Abstractions;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
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
                .AddUsings("CrossCutting.Data.Core.Builders")
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
                                                    .AddLiteralCodeStatements("if (operation != DatabaseOperation.Select)",
                                                                              "{",
                                                                              @"    throw new ArgumentOutOfRangeException(""operation"", ""Only Select operation is supported"");",
                                                                              "}",
                                                                              "return new SelectCommandBuilder()",
                                                                              "    .Select(_settings.Fields)",
                                                                              "    .From(_settings.TableName)",
                                                                              $"    .Where({GetIdentityWhereStatement(instance)})",
                                                                              "    .AppendParameters(source)",
                                                                              "    .Build();"));

        private static object GetIdentityWhereStatement(IDataObjectInfo instance)
            => string.Concat("\"",
                             string.Join(" AND ", instance.Fields.Where(x => x.IsIdentityField && !x.SkipFieldOnFind()).Select(x => $"[{x.Name}] = @{x.Name}")),
                             "\"");

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
