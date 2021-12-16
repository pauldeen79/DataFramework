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
        public static IClass ToCommandProviderClass(this IDataObjectInfo instance, GeneratorSettings settings)
            => instance.ToCommandProviderClassBuilder(settings).Build();

        public static ClassBuilder ToCommandProviderClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
            => new ClassBuilder()
                .AddUsings("CrossCutting.Data.Core.Commands")
                .WithName($"{instance.Name}CommandProvider")
                .WithNamespace(instance.GetCommandProvidersNamespace())
                .FillFrom(instance)
                .WithVisibility(instance.Metadata.GetValue(CommandProviders.Visibility, () => instance.IsVisible.ToVisibility()))
                .AddInterfaces(typeof(IDatabaseCommandProvider<>).CreateGenericTypeName(instance.GetEntityIdentityFullName()))
                .AddAttributes(GetIdentityCommandProviderClassAttributes(instance))
                .AddMethods(GetIdentityCommandProviderClassMethods(instance, settings));

        private static IEnumerable<ClassMethodBuilder> GetIdentityCommandProviderClassMethods(IDataObjectInfo instance, GeneratorSettings settings)
        {
            yield return new ClassMethodBuilder()
                .WithName(nameof(IDatabaseCommandProvider<object>.Create))
                .WithType(typeof(IDatabaseCommand))
                .AddParameter("source", instance.GetEntityFullName())
                .AddParameter("operation", typeof(DatabaseOperation))
                .AddLiteralCodeStatements
                (
                    "switch (operation)",
                    "{",
                    "    case DatabaseOperation.Insert:",
                    $"        return new {GetInsertCommandType(instance)}<{instance.GetEntityFullName()}>(\"{GetInsertCommand(instance)}\", source, DatabaseOperation.Insert, AddParameters);",
                    "    case DatabaseOperation.Update:",
                    $"        return new {GetUpdateCommandType(instance)}<{instance.GetEntityFullName()}>(\"{GetUpdateCommand(instance)}\", source, DatabaseOperation.Update, UpdateParameters);",
                    "    case DatabaseOperation.Delete:",
                    $"        return new {GetDeleteCommandType(instance)}<{instance.GetEntityFullName()}>(\"{GetDeleteCommand(instance)}\", source, DatabaseOperation.Delete, DeleteParameters);",
                    "    default:",
                    @"        throw new ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));",
                    "}"
                );

            yield return new ClassMethodBuilder()
                .WithName("AddParameters")
                .WithType(typeof(object))
                .AddParameter("resultEntity", instance.GetEntityFullName())
                .AddLiteralCodeStatements("return new[]", "{")
                .AddLiteralCodeStatements(instance.Fields.Where(x => x.UseOnInsert()).Select(x => $"    new KeyValuePair<string, {GetObjectType(settings)}>(\"@{x.Name}\", resultEntity.{x.Name}),"))
                .AddLiteralCodeStatements("};");

            yield return new ClassMethodBuilder()
                .WithName("UpdateParameters")
                .WithType(typeof(object))
                .AddParameter("resultEntity", instance.GetEntityFullName())
                .AddLiteralCodeStatements("return new[]", "{")
                .AddLiteralCodeStatements(instance.Fields.Where(x => x.UseOnUpdate()).Select(x => $"    new KeyValuePair<string, {GetObjectType(settings)}>(\"@{x.Name}\", resultEntity.{x.Name}),"))
                .AddLiteralCodeStatements(instance.GetUpdateConcurrencyCheckFields().Where(x => x.UseOnUpdate()).Select(x => $"    new KeyValuePair<string, {GetObjectType(settings)}>(\"@{x.Name}Original\", resultEntity.{x.Name}Original),"))
                .AddLiteralCodeStatements("};");

            yield return new ClassMethodBuilder()
                .WithName("DeleteParameters")
                .WithType(typeof(object))
                .AddParameter("resultEntity", instance.GetEntityFullName())
                .AddLiteralCodeStatements("return new[]", "{")
                .AddLiteralCodeStatements(instance.Fields.Where(x => x.UseOnDelete()).Select(x => $"    new KeyValuePair<string, {GetObjectType(settings)}>(\"@{x.Name}\", resultEntity.{x.Name}),"))
                .AddLiteralCodeStatements("};");
        }

        private static string GetObjectType(GeneratorSettings settings)
            => settings.EnableNullableContext
                ? "object?"
                : "object";

        private static string GetInsertCommandType(IDataObjectInfo instance)
            => $"StoredProcedureCommand<{instance.GetEntityFullName()}>"; //might also want to use TextCommand maybe?

        private static string GetInsertCommand(IDataObjectInfo instance)
            => $"[Insert{instance.Name}]";

        private static string GetUpdateCommandType(IDataObjectInfo instance)
            => $"StoredProcedureCommand<{instance.GetEntityFullName()}>";

        private static string GetUpdateCommand(IDataObjectInfo instance)
            => $"[Update{instance.Name}]";

        private static string GetDeleteCommandType(IDataObjectInfo instance)
            => $"StoredProcedureCommand<{instance.GetEntityFullName()}>";

        private static string GetDeleteCommand(IDataObjectInfo instance)
            => $"[Delete{instance.Name}]";
    }
}
