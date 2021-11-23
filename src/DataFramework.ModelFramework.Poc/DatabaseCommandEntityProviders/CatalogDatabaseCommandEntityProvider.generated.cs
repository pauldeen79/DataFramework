using System;
using System.CodeDom.Compiler;
using System.Data;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Sql.Extensions;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.DatabaseCommandEntityProviders
{
#nullable enable
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogDatabaseCommandEntityProvider : IDatabaseCommandEntityProvider<Catalog, CatalogBuilder>
    {
        public Func<CatalogBuilder, DatabaseOperation, CatalogBuilder>? ResultEntityDelegate
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

        public Func<CatalogBuilder, DatabaseOperation, IDataReader, CatalogBuilder>? AfterReadDelegate
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

        public Func<Catalog, CatalogBuilder>? CreateBuilderDelegate => entity => new CatalogBuilder(entity);

        public Func<CatalogBuilder, Catalog>? CreateEntityDelegate => builder => builder.Build();

        private CatalogBuilder AddResultEntity(CatalogBuilder resultEntity)
        {
            // Moved from Finalize
            resultEntity = resultEntity.SetIsExistingEntity(true);
            return resultEntity;
        }

        private CatalogBuilder AddAfterRead(CatalogBuilder resultEntity, IDataReader reader)
        {
            resultEntity = resultEntity.SetId(reader.GetInt32("Id"));
            resultEntity = resultEntity.SetName(reader.GetString("Name"));
            resultEntity = resultEntity.SetDateCreated(reader.GetDateTime("DateCreated"));
            resultEntity = resultEntity.SetDateLastModified(reader.GetDateTime("DateLastModified"));
            resultEntity = resultEntity.SetDateSynchronized(reader.GetDateTime("DateSynchronized"));
            resultEntity = resultEntity.SetDriveSerialNumber(reader.GetString("DriveSerialNumber"));
            resultEntity = resultEntity.SetDriveTypeCodeType(reader.GetString("DriveTypeCodeType"));
            resultEntity = resultEntity.SetDriveTypeCode(reader.GetString("DriveTypeCode"));
            resultEntity = resultEntity.SetDriveTotalSize(reader.GetInt32("DriveTotalSize"));
            resultEntity = resultEntity.SetDriveFreeSpace(reader.GetInt32("DriveFreeSpace"));
            resultEntity = resultEntity.SetRecursive(reader.GetBoolean("Recursive"));
            resultEntity = resultEntity.SetSorted(reader.GetBoolean("Sorted"));
            resultEntity = resultEntity.SetStartDirectory(reader.GetString("StartDirectory"));
            resultEntity = resultEntity.SetExtraField1(reader.GetNullableString("ExtraField1"));
            resultEntity = resultEntity.SetExtraField2(reader.GetNullableString("ExtraField2"));
            resultEntity = resultEntity.SetExtraField3(reader.GetNullableString("ExtraField3"));
            resultEntity = resultEntity.SetExtraField4(reader.GetNullableString("ExtraField4"));
            resultEntity = resultEntity.SetExtraField5(reader.GetNullableString("ExtraField5"));
            resultEntity = resultEntity.SetExtraField6(reader.GetNullableString("ExtraField6"));
            resultEntity = resultEntity.SetExtraField7(reader.GetNullableString("ExtraField7"));
            resultEntity = resultEntity.SetExtraField8(reader.GetNullableString("ExtraField8"));
            resultEntity = resultEntity.SetExtraField9(reader.GetNullableString("ExtraField9"));
            resultEntity = resultEntity.SetExtraField10(reader.GetNullableString("ExtraField10"));
            resultEntity = resultEntity.SetExtraField11(reader.GetNullableString("ExtraField11"));
            resultEntity = resultEntity.SetExtraField12(reader.GetNullableString("ExtraField12"));
            resultEntity = resultEntity.SetExtraField13(reader.GetNullableString("ExtraField13"));
            resultEntity = resultEntity.SetExtraField14(reader.GetNullableString("ExtraField14"));
            resultEntity = resultEntity.SetExtraField15(reader.GetNullableString("ExtraField15"));
            resultEntity = resultEntity.SetExtraField16(reader.GetNullableString("ExtraField16"));
            resultEntity = resultEntity.SetIdOriginal(reader.GetInt32("Id"));
            resultEntity = resultEntity.SetNameOriginal(reader.GetString("Name"));
            resultEntity = resultEntity.SetDateCreatedOriginal(reader.GetDateTime("DateCreated"));
            resultEntity = resultEntity.SetDateLastModifiedOriginal(reader.GetDateTime("DateLastModified"));
            resultEntity = resultEntity.SetDateSynchronizedOriginal(reader.GetDateTime("DateSynchronized"));
            resultEntity = resultEntity.SetDriveSerialNumberOriginal(reader.GetString("DriveSerialNumber"));
            resultEntity = resultEntity.SetDriveTypeCodeTypeOriginal(reader.GetString("DriveTypeCodeType"));
            resultEntity = resultEntity.SetDriveTypeCodeOriginal(reader.GetString("DriveTypeCode"));
            resultEntity = resultEntity.SetDriveTotalSizeOriginal(reader.GetInt32("DriveTotalSize"));
            resultEntity = resultEntity.SetDriveFreeSpaceOriginal(reader.GetInt32("DriveFreeSpace"));
            resultEntity = resultEntity.SetRecursiveOriginal(reader.GetBoolean("Recursive"));
            resultEntity = resultEntity.SetSortedOriginal(reader.GetBoolean("Sorted"));
            resultEntity = resultEntity.SetStartDirectoryOriginal(reader.GetString("StartDirectory"));
            resultEntity = resultEntity.SetExtraField1Original(reader.GetNullableString("ExtraField1"));
            resultEntity = resultEntity.SetExtraField2Original(reader.GetNullableString("ExtraField2"));
            resultEntity = resultEntity.SetExtraField3Original(reader.GetNullableString("ExtraField3"));
            resultEntity = resultEntity.SetExtraField4Original(reader.GetNullableString("ExtraField4"));
            resultEntity = resultEntity.SetExtraField5Original(reader.GetNullableString("ExtraField5"));
            resultEntity = resultEntity.SetExtraField6Original(reader.GetNullableString("ExtraField6"));
            resultEntity = resultEntity.SetExtraField7Original(reader.GetNullableString("ExtraField7"));
            resultEntity = resultEntity.SetExtraField8Original(reader.GetNullableString("ExtraField8"));
            resultEntity = resultEntity.SetExtraField9Original(reader.GetNullableString("ExtraField9"));
            resultEntity = resultEntity.SetExtraField10Original(reader.GetNullableString("ExtraField10"));
            resultEntity = resultEntity.SetExtraField11Original(reader.GetNullableString("ExtraField11"));
            resultEntity = resultEntity.SetExtraField12Original(reader.GetNullableString("ExtraField12"));
            resultEntity = resultEntity.SetExtraField13Original(reader.GetNullableString("ExtraField13"));
            resultEntity = resultEntity.SetExtraField14Original(reader.GetNullableString("ExtraField14"));
            resultEntity = resultEntity.SetExtraField15Original(reader.GetNullableString("ExtraField15"));
            resultEntity = resultEntity.SetExtraField16Original(reader.GetNullableString("ExtraField16"));

            return resultEntity;
        }

        private CatalogBuilder UpdateResultEntity(CatalogBuilder resultEntity)
        {

            return resultEntity;
        }

        private CatalogBuilder UpdateAfterRead(CatalogBuilder resultEntity, IDataReader reader)
        {
            resultEntity = resultEntity.SetId(reader.GetInt32("Id"));
            resultEntity = resultEntity.SetName(reader.GetString("Name"));
            resultEntity = resultEntity.SetDateCreated(reader.GetDateTime("DateCreated"));
            resultEntity = resultEntity.SetDateLastModified(reader.GetDateTime("DateLastModified"));
            resultEntity = resultEntity.SetDateSynchronized(reader.GetDateTime("DateSynchronized"));
            resultEntity = resultEntity.SetDriveSerialNumber(reader.GetString("DriveSerialNumber"));
            resultEntity = resultEntity.SetDriveTypeCodeType(reader.GetString("DriveTypeCodeType"));
            resultEntity = resultEntity.SetDriveTypeCode(reader.GetString("DriveTypeCode"));
            resultEntity = resultEntity.SetDriveTotalSize(reader.GetInt32("DriveTotalSize"));
            resultEntity = resultEntity.SetDriveFreeSpace(reader.GetInt32("DriveFreeSpace"));
            resultEntity = resultEntity.SetRecursive(reader.GetBoolean("Recursive"));
            resultEntity = resultEntity.SetSorted(reader.GetBoolean("Sorted"));
            resultEntity = resultEntity.SetStartDirectory(reader.GetString("StartDirectory"));
            resultEntity = resultEntity.SetExtraField1(reader.GetNullableString("ExtraField1"));
            resultEntity = resultEntity.SetExtraField2(reader.GetNullableString("ExtraField2"));
            resultEntity = resultEntity.SetExtraField3(reader.GetNullableString("ExtraField3"));
            resultEntity = resultEntity.SetExtraField4(reader.GetNullableString("ExtraField4"));
            resultEntity = resultEntity.SetExtraField5(reader.GetNullableString("ExtraField5"));
            resultEntity = resultEntity.SetExtraField6(reader.GetNullableString("ExtraField6"));
            resultEntity = resultEntity.SetExtraField7(reader.GetNullableString("ExtraField7"));
            resultEntity = resultEntity.SetExtraField8(reader.GetNullableString("ExtraField8"));
            resultEntity = resultEntity.SetExtraField9(reader.GetNullableString("ExtraField9"));
            resultEntity = resultEntity.SetExtraField10(reader.GetNullableString("ExtraField10"));
            resultEntity = resultEntity.SetExtraField11(reader.GetNullableString("ExtraField11"));
            resultEntity = resultEntity.SetExtraField12(reader.GetNullableString("ExtraField12"));
            resultEntity = resultEntity.SetExtraField13(reader.GetNullableString("ExtraField13"));
            resultEntity = resultEntity.SetExtraField14(reader.GetNullableString("ExtraField14"));
            resultEntity = resultEntity.SetExtraField15(reader.GetNullableString("ExtraField15"));
            resultEntity = resultEntity.SetExtraField16(reader.GetNullableString("ExtraField16"));
            resultEntity = resultEntity.SetIdOriginal(reader.GetInt32("Id"));
            resultEntity = resultEntity.SetNameOriginal(reader.GetString("Name"));
            resultEntity = resultEntity.SetDateCreatedOriginal(reader.GetDateTime("DateCreated"));
            resultEntity = resultEntity.SetDateLastModifiedOriginal(reader.GetDateTime("DateLastModified"));
            resultEntity = resultEntity.SetDateSynchronizedOriginal(reader.GetDateTime("DateSynchronized"));
            resultEntity = resultEntity.SetDriveSerialNumberOriginal(reader.GetString("DriveSerialNumber"));
            resultEntity = resultEntity.SetDriveTypeCodeTypeOriginal(reader.GetString("DriveTypeCodeType"));
            resultEntity = resultEntity.SetDriveTypeCodeOriginal(reader.GetString("DriveTypeCode"));
            resultEntity = resultEntity.SetDriveTotalSizeOriginal(reader.GetInt32("DriveTotalSize"));
            resultEntity = resultEntity.SetDriveFreeSpaceOriginal(reader.GetInt32("DriveFreeSpace"));
            resultEntity = resultEntity.SetRecursiveOriginal(reader.GetBoolean("Recursive"));
            resultEntity = resultEntity.SetSortedOriginal(reader.GetBoolean("Sorted"));
            resultEntity = resultEntity.SetStartDirectoryOriginal(reader.GetString("StartDirectory"));
            resultEntity = resultEntity.SetExtraField1Original(reader.GetNullableString("ExtraField1"));
            resultEntity = resultEntity.SetExtraField2Original(reader.GetNullableString("ExtraField2"));
            resultEntity = resultEntity.SetExtraField3Original(reader.GetNullableString("ExtraField3"));
            resultEntity = resultEntity.SetExtraField4Original(reader.GetNullableString("ExtraField4"));
            resultEntity = resultEntity.SetExtraField5Original(reader.GetNullableString("ExtraField5"));
            resultEntity = resultEntity.SetExtraField6Original(reader.GetNullableString("ExtraField6"));
            resultEntity = resultEntity.SetExtraField7Original(reader.GetNullableString("ExtraField7"));
            resultEntity = resultEntity.SetExtraField8Original(reader.GetNullableString("ExtraField8"));
            resultEntity = resultEntity.SetExtraField9Original(reader.GetNullableString("ExtraField9"));
            resultEntity = resultEntity.SetExtraField10Original(reader.GetNullableString("ExtraField10"));
            resultEntity = resultEntity.SetExtraField11Original(reader.GetNullableString("ExtraField11"));
            resultEntity = resultEntity.SetExtraField12Original(reader.GetNullableString("ExtraField12"));
            resultEntity = resultEntity.SetExtraField13Original(reader.GetNullableString("ExtraField13"));
            resultEntity = resultEntity.SetExtraField14Original(reader.GetNullableString("ExtraField14"));
            resultEntity = resultEntity.SetExtraField15Original(reader.GetNullableString("ExtraField15"));
            resultEntity = resultEntity.SetExtraField16Original(reader.GetNullableString("ExtraField16"));

            return resultEntity;
        }

        private CatalogBuilder DeleteResultEntity(CatalogBuilder resultEntity)
        {

            return resultEntity;
        }
    }
#nullable restore
}
