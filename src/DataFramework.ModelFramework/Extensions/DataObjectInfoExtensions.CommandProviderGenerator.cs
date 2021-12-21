using System;
using System.Collections.Generic;
using System.Linq;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.Commands;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static IClass ToCommandProviderClass(this IDataObjectInfo instance, GeneratorSettings settings)
            => instance.ToCommandProviderClassBuilder(settings).Build();

        public static ClassBuilder ToCommandProviderClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
            => new ClassBuilder()
                .WithName($"{instance.Name}CommandProvider")
                .WithNamespace(instance.GetCommandProvidersNamespace())
                .FillFrom(instance)
                .WithVisibility(instance.Metadata.GetValue(CommandProviders.Visibility, () => instance.IsVisible.ToVisibility()))
                .AddInterfaces(typeof(IDatabaseCommandProvider<>).CreateGenericTypeName(instance.GetEntityIdentityFullName()))
                .AddAttributes(GetCommandProviderClassAttributes(instance))
                .AddMethods(GetCommandProviderClassMethods(instance, settings));

        private static IEnumerable<AttributeBuilder> GetCommandProviderClassAttributes(IDataObjectInfo instance)
        {
            yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.CommandProviderGenerator");

            foreach (var attribute in instance.Metadata.GetValues<IAttribute>(CommandProviders.Attribute))
            {
                yield return new AttributeBuilder(attribute);
            }
        }

        private static IEnumerable<ClassMethodBuilder> GetCommandProviderClassMethods(IDataObjectInfo instance, GeneratorSettings settings)
        {
            yield return new ClassMethodBuilder()
                .WithName(nameof(IDatabaseCommandProvider<object>.Create))
                .WithType(typeof(IDatabaseCommand))
                .AddParameter("source", instance.GetEntityFullName())
                .AddParameter("operation", typeof(DatabaseOperation))
                .AddLiteralCodeStatements
                (
                    "switch (operation)",
                    "{"
                )
                .AddCommandProviderMethod(instance, CommandProviders.PreventAdd, DatabaseOperation.Insert, GetInsertCommandType(instance), GetInsertCommand(instance))
                .AddCommandProviderMethod(instance, CommandProviders.PreventUpdate, DatabaseOperation.Update, GetUpdateCommandType(instance), GetUpdateCommand(instance))
                .AddCommandProviderMethod(instance, CommandProviders.PreventDelete, DatabaseOperation.Delete, GetDeleteCommandType(instance), GetDeleteCommand(instance))
                .AddLiteralCodeStatements
                (
                    "    default:",
                    $@"        throw new {nameof(ArgumentOutOfRangeException)}(""operation"", string.Format(""Unsupported operation: {{0}}"", operation));",
                    "}"
                );

            if (!instance.Metadata.GetBooleanValue(CommandProviders.PreventAdd))
            {
                yield return new ClassMethodBuilder()
                    .WithName("AddParameters")
                    .WithType(typeof(object))
                    .AddParameter("resultEntity", instance.GetEntityFullName())
                    .AddLiteralCodeStatements("return new[]", "{")
                    .AddLiteralCodeStatements(instance.Fields.Where(x => x.UseOnInsert()).Select(x => $"    new KeyValuePair<string, {GetObjectType(settings)}>(\"@{x.Name}\", resultEntity.{x.Name}),"))
                    .AddLiteralCodeStatements("};");
            }

            if (!instance.Metadata.GetBooleanValue(CommandProviders.PreventUpdate))
            {
                yield return new ClassMethodBuilder()
                    .WithName("UpdateParameters")
                    .WithType(typeof(object))
                    .AddParameter("resultEntity", instance.GetEntityFullName())
                    .AddLiteralCodeStatements("return new[]", "{")
                    .AddLiteralCodeStatements(instance.Fields.Where(x => x.UseOnUpdate()).Select(x => $"    new KeyValuePair<string, {GetObjectType(settings)}>(\"@{x.Name}\", resultEntity.{x.Name}),"))
                    .AddLiteralCodeStatements(instance.GetUpdateConcurrencyCheckFields().Where(x => x.UseOnUpdate() || x.IsIdentityField || x.IsSqlIdentity()).Select(x => $"    new KeyValuePair<string, {GetObjectType(settings)}>(\"@{x.Name}Original\", resultEntity.{x.Name}Original),"))
                    .AddLiteralCodeStatements("};");
            }

            if (!instance.Metadata.GetBooleanValue(CommandProviders.PreventDelete))
            {
                yield return new ClassMethodBuilder()
                    .WithName("DeleteParameters")
                    .WithType(typeof(object))
                    .AddParameter("resultEntity", instance.GetEntityFullName())
                    .AddLiteralCodeStatements("return new[]", "{")
                    .AddLiteralCodeStatements(instance.GetUpdateConcurrencyCheckFields().Where(x => x.UseOnDelete() || x.IsIdentityField || x.IsSqlIdentity()).Select(x => $"    new KeyValuePair<string, {GetObjectType(settings)}>(\"@{x.Name}Original\", resultEntity.{x.Name}Original),"))
                    .AddLiteralCodeStatements("};");
            }
        }

        private static string GetObjectType(GeneratorSettings settings)
            => settings.EnableNullableContext
                ? "object?"
                : "object";

        private static string GetInsertCommandType(IDataObjectInfo instance)
            => instance.HasAddStoredProcedure()
                ? typeof(StoredProcedureCommand<>).CreateGenericTypeName(instance.GetEntityFullName())
                : typeof(TextCommand<>).CreateGenericTypeName(instance.GetEntityFullName());

        private static string GetInsertCommand(IDataObjectInfo instance)
            => instance.HasAddStoredProcedure()
                ? $"[{string.Format(instance.Metadata.GetStringValue(Database.AddStoredProcedureName, "Insert{0}"), instance.GetTableName())}]"
                : instance.CreateDatabaseInsertCommandText();

        private static string GetUpdateCommandType(IDataObjectInfo instance)
            => instance.HasUpdateStoredProcedure()
                ? typeof(StoredProcedureCommand<>).CreateGenericTypeName(instance.GetEntityFullName())
                : typeof(TextCommand<>).CreateGenericTypeName(instance.GetEntityFullName());

        private static string GetUpdateCommand(IDataObjectInfo instance)
            => instance.HasUpdateStoredProcedure()
                ? $"[{string.Format(instance.Metadata.GetStringValue(Database.UpdateStoredProcedureName, "Update{0}"), instance.GetTableName())}]"
                : instance.CreateDatabaseUpdateCommandText();

        private static string GetDeleteCommandType(IDataObjectInfo instance)
            => instance.HasDeleteStoredProcedure()
                ? typeof(StoredProcedureCommand<>).CreateGenericTypeName(instance.GetEntityFullName())
                : typeof(TextCommand<>).CreateGenericTypeName(instance.GetEntityFullName());

        private static string GetDeleteCommand(IDataObjectInfo instance)
            => instance.HasDeleteStoredProcedure()
                ? $"[{string.Format(instance.Metadata.GetStringValue(Database.DeleteStoredProcedureName, "Delete{0}"), instance.GetTableName())}]"
                : instance.CreateDatabaseDeleteCommandText();
    }
}
