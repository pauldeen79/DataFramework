using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using CrossCutting.Data.Sql.Extensions;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandEntityProviders
{
#nullable enable
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldDatabaseCommandEntityProvider : IDatabaseCommandEntityProvider<ExtraField, ExtraFieldBuilder>
    {
        public Func<ExtraFieldBuilder, DatabaseOperation, IDatabaseCommand> CommandDelegate
            => (entity, operation) =>
            {
                switch (operation)
                {
                    case DatabaseOperation.Insert:
                        return new StoredProcedureCommand<ExtraFieldBuilder>(@"[InsertExtraField]", entity, DatabaseOperation.Insert, AddParameters);
                    case DatabaseOperation.Update:
                        return new StoredProcedureCommand<ExtraFieldBuilder>(@"[UpdateExtraField]", entity, DatabaseOperation.Update, UpdateParameters);
                    case DatabaseOperation.Delete:
                        return new StoredProcedureCommand<ExtraFieldBuilder>(@"[DeleteExtraField]", entity, DatabaseOperation.Delete, DeleteParameters);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(operation), $"Unsupported operation: {operation}");
                }
            };

        public Func<ExtraFieldBuilder, DatabaseOperation, ExtraFieldBuilder>? ResultEntityDelegate
            => (entity, operation) =>
            {
                switch (operation)
                {
                    case DatabaseOperation.Insert:
                        return AddResultEntity(entity);
                    case DatabaseOperation.Update:
                        return UpdateResultEntity(entity);
                    case DatabaseOperation.Delete:
                        return DeleteResultEntity(entity);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(operation), $"Unsupported operation: {operation}");
                }
            };

        public Func<ExtraFieldBuilder, DatabaseOperation, IDataReader, ExtraFieldBuilder>? AfterReadDelegate
            => (entity, operation, reader) =>
            {
                switch (operation)
                {
                    case DatabaseOperation.Insert:
                        return AddAfterRead(entity, reader);
                    case DatabaseOperation.Update:
                        return UpdateAfterRead(entity, reader);
                    case DatabaseOperation.Delete:
                        return entity;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(operation), $"Unsupported operation: {operation}");
                }
            };

        public Func<ExtraField, ExtraFieldBuilder>? CreateBuilderDelegate => entity => new ExtraFieldBuilder(entity);

        public Func<ExtraFieldBuilder, ExtraField>? CreateEntityDelegate => builder => builder.Build();

        private ExtraFieldBuilder AddResultEntity(ExtraFieldBuilder resultEntity)
        {
            // Moved from Finalize
            resultEntity = resultEntity.SetIsExistingEntity(true);

            return resultEntity;
        }

        private ExtraFieldBuilder AddAfterRead(ExtraFieldBuilder resultEntity, IDataReader reader)
        {
            resultEntity = resultEntity.SetEntityName(reader.GetString("EntityName"));
            resultEntity = resultEntity.SetName(reader.GetString("Name"));
            resultEntity = resultEntity.SetDescription(reader.GetNullableString("Description"));
            resultEntity = resultEntity.SetFieldNumber(reader.GetByte("FieldNumber"));
            resultEntity = resultEntity.SetFieldType(reader.GetString("FieldType"));
            resultEntity = resultEntity.SetEntityNameOriginal(reader.GetString("EntityName"));
            resultEntity = resultEntity.SetNameOriginal(reader.GetString("Name"));
            resultEntity = resultEntity.SetDescriptionOriginal(reader.GetString("Description"));
            resultEntity = resultEntity.SetFieldNumberOriginal(reader.GetByte("FieldNumber"));
            resultEntity = resultEntity.SetFieldTypeOriginal(reader.GetString("FieldType"));

            return resultEntity;
        }

        private object AddParameters(ExtraFieldBuilder resultEntity)
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

        private ExtraFieldBuilder UpdateResultEntity(ExtraFieldBuilder resultEntity)
        {

            return resultEntity;
        }

        private ExtraFieldBuilder UpdateAfterRead(ExtraFieldBuilder resultEntity, IDataReader reader)
        {
            resultEntity = resultEntity.SetEntityName(reader.GetString("EntityName"));
            resultEntity = resultEntity.SetName(reader.GetString("Name"));
            resultEntity = resultEntity.SetDescription(reader.GetNullableString("Description"));
            resultEntity = resultEntity.SetFieldNumber(reader.GetByte("FieldNumber"));
            resultEntity = resultEntity.SetFieldType(reader.GetString("FieldType"));
            resultEntity = resultEntity.SetEntityNameOriginal(reader.GetString("EntityName"));
            resultEntity = resultEntity.SetNameOriginal(reader.GetString("Name"));
            resultEntity = resultEntity.SetDescriptionOriginal(reader.GetNullableString("Description"));
            resultEntity = resultEntity.SetFieldNumberOriginal(reader.GetByte("FieldNumber"));
            resultEntity = resultEntity.SetFieldTypeOriginal(reader.GetString("FieldType"));

            return resultEntity;
        }

        private object UpdateParameters(ExtraFieldBuilder resultEntity)
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

        private ExtraFieldBuilder DeleteResultEntity(ExtraFieldBuilder resultEntity)
        {

            return resultEntity;
        }

        private object DeleteParameters(ExtraFieldBuilder resultEntity)
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
