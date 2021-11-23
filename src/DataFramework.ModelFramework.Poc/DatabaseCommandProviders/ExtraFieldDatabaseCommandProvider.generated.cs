using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core.Commands;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandProviders
{
#nullable enable
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldDatabaseCommandProvider : IDatabaseCommandProvider<ExtraField>
    {
        public IDatabaseCommand Create(ExtraField source, DatabaseOperation operation)
        {
            switch (operation)
            {
                case DatabaseOperation.Insert:
                    return new StoredProcedureCommand<ExtraField>(@"[InsertExtraField]", source, DatabaseOperation.Insert, AddParameters);
                case DatabaseOperation.Update:
                    return new StoredProcedureCommand<ExtraField>(@"[UpdateExtraField]", source, DatabaseOperation.Update, UpdateParameters);
                case DatabaseOperation.Delete:
                    return new StoredProcedureCommand<ExtraField>(@"[DeleteExtraField]", source, DatabaseOperation.Delete, DeleteParameters);
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), $"Unsupported operation: {operation}");
            }
        }

        public IDatabaseCommand Create(DatabaseOperation operation)
        {
            throw new NotSupportedException("Can only generate a command with a ExtraField entity");
        }

        private object AddParameters(ExtraField resultEntity)
        {
            return new[]
            {
                new KeyValuePair<string, object?>("@EntityName", resultEntity.EntityName),
                new KeyValuePair<string, object?>("@Name", resultEntity.Name),
                new KeyValuePair<string, object?>("@Description", resultEntity.Description),
                new KeyValuePair<string, object?>("@FieldNumber", resultEntity.FieldNumber),
                new KeyValuePair<string, object?>("@FieldType", resultEntity.FieldType),
            };
        }

        private object UpdateParameters(ExtraField resultEntity)
        {
            return new[]
            {
                new KeyValuePair<string, object?>("@EntityName", resultEntity.EntityName),
                new KeyValuePair<string, object?>("@Name", resultEntity.Name),
                new KeyValuePair<string, object?>("@Description", resultEntity.Description),
                new KeyValuePair<string, object?>("@FieldNumber", resultEntity.FieldNumber),
                new KeyValuePair<string, object?>("@FieldType", resultEntity.FieldType),
                new KeyValuePair<string, object?>("@EntityNameOriginal", resultEntity.EntityNameOriginal),
                new KeyValuePair<string, object?>("@NameOriginal", resultEntity.NameOriginal),
                new KeyValuePair<string, object?>("@DescriptionOriginal", resultEntity.DescriptionOriginal),
                new KeyValuePair<string, object?>("@FieldNumberOriginal", resultEntity.FieldNumberOriginal),
                new KeyValuePair<string, object?>("@FieldTypeOriginal", resultEntity.FieldTypeOriginal),
            };
        }

        private object DeleteParameters(ExtraField resultEntity)
        {
            return new[]
            {
                new KeyValuePair<string, object?>("@EntityName", resultEntity.EntityName),
                new KeyValuePair<string, object?>("@Name", resultEntity.Name),
                new KeyValuePair<string, object?>("@Description", resultEntity.Description),
                new KeyValuePair<string, object?>("@FieldNumber", resultEntity.FieldNumber),
                new KeyValuePair<string, object?>("@FieldType", resultEntity.FieldType),
            };
        }
    }
#nullable restore
}
