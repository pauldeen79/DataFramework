using System.Collections.Generic;
using System.Linq;
using CrossCutting.Data.Abstractions;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static IClass ToEntityRetrieverSettingsClass(this IDataObjectInfo instance, GeneratorSettings settings)
            => instance.ToEntityRetrieverSettingsClassBuilder(settings).Build();

        public static ClassBuilder ToEntityRetrieverSettingsClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
            => new ClassBuilder()
                .WithName($"{instance.Name}PagedEntityRetrieverSettings")
                .WithNamespace(instance.GetEntityRetrieverSettingsNamespace())
                .FillFrom(instance)
                .WithVisibility(instance.Metadata.GetValue(EntityRetrieverSettings.Visibility, () => instance.IsVisible.ToVisibility()))
                .AddInterfaces(typeof(IPagedDatabaseEntityRetrieverSettings))
                .AddAttributes(GetEntityRetrieverSettingsClassAttributes(instance))
                .AddProperties(GetEntityRetrieverSettingsClassProperties(instance));

        private static IEnumerable<AttributeBuilder> GetEntityRetrieverSettingsClassAttributes(IDataObjectInfo instance)
        {
            yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.EntityRetrieverSettingsGenerator");

            foreach (var attribute in instance.Metadata.GetValues<IAttribute>(EntityRetrieverSettings.Attribute))
            {
                yield return new AttributeBuilder(attribute);
            }
        }

        private static IEnumerable<ClassPropertyBuilder> GetEntityRetrieverSettingsClassProperties(IDataObjectInfo instance)
        {
            yield return new ClassPropertyBuilder()
                .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.TableName))
                .WithType(typeof(string))
                .WithHasSetter(false)
                .AddGetterLiteralCodeStatements($"return {instance.Metadata.GetStringValue(EntityRetrieverSettings.TableName, () => instance.GetTableName()).CsharpFormat()};");

            yield return new ClassPropertyBuilder()
                .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.Fields))
                .WithType(typeof(string))
                .WithHasSetter(false)
                .AddGetterLiteralCodeStatements(string.Concat
                (
                    "return ",
                    string.Join(", ", instance.Fields.Where(x => x.UseOnSelect()).Select(x => x.GetDatabaseFieldName()).Concat(instance.Metadata.GetStringValues(EntityRetrieverSettings.Field))).CsharpFormat(),
                    ";"
                ));

            yield return new ClassPropertyBuilder()
                .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.DefaultOrderBy))
                .WithType(typeof(string))
                .WithHasSetter(false)
                .AddGetterLiteralCodeStatements($"return {instance.Metadata.GetStringValue(EntityRetrieverSettings.DefaultOrderByFields).CsharpFormat()};");

            yield return new ClassPropertyBuilder()
                .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.DefaultWhere))
                .WithType(typeof(string))
                .WithHasSetter(false)
                .AddGetterLiteralCodeStatements($"return {instance.Metadata.GetStringValue(EntityRetrieverSettings.DefaultWhereClause).CsharpFormat()};");

            yield return new ClassPropertyBuilder()
                .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.OverridePageSize))
                .WithType(typeof(int?))
                .WithHasSetter(false)
                .AddGetterLiteralCodeStatements($"return {instance.Metadata.GetValue<int?>(EntityRetrieverSettings.OverridePageSize, () => -1)};");
        }
    }
}
