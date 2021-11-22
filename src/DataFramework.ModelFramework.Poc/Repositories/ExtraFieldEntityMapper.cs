﻿using System.CodeDom.Compiler;
using System.Data;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Sql.Extensions;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldEntityMapper : IDataReaderMapper<ExtraField>
    {
        public ExtraField Map(IDataReader reader)
        {
            var instance = new ExtraField
            (
                entityName: reader.GetString("EntityName"),
                name: reader.GetString("Name"),
                description: reader.GetNullableString("Description"),
                fieldNumber: reader.GetByte("FieldNumber"),
                fieldType: reader.GetString("FieldType"),
                isExistingEntity: true,
                entityNameOriginal: reader.GetString("EntityName"),
                nameOriginal: reader.GetString("Name"),
                descriptionOriginal: reader.GetNullableString("Description"),
                fieldNumberOriginal: reader.GetByte("FieldNumber"),
                fieldTypeOriginal: reader.GetString("FieldType")
            );

            return instance;
        }
    }
}
