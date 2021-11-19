using System;
using System.Collections.Generic;
using System.Data;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using CrossCutting.Data.Sql.Extensions;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    public class CatalogDatabaseCommandEntityProvider : IDatabaseCommandEntityProvider<Catalog, CatalogBuilder>
    {
        public Func<CatalogBuilder, DatabaseOperation, IDatabaseCommand> CommandDelegate
            => (entity, operation) =>
            {
                switch (operation)
                {
                    case DatabaseOperation.Insert:
                        return new StoredProcedureCommand<CatalogBuilder>(@"[InsertCatalog]", entity, DatabaseOperation.Insert, AddParameters);
                    case DatabaseOperation.Update:
                        return new StoredProcedureCommand<CatalogBuilder>(@"[UpdateCatalog]", entity, DatabaseOperation.Update, UpdateParameters);
                    case DatabaseOperation.Delete:
                        return new StoredProcedureCommand<CatalogBuilder>(@"[DeleteCatalog]", entity, DatabaseOperation.Delete, DeleteParameters);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(operation), $"Unsupported operation: {operation}");
                }
            };

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

        private object AddParameters(CatalogBuilder resultEntity)
        {
            return new[]
            {
                new KeyValuePair<string, object?>("@Name", resultEntity.Name),
                new KeyValuePair<string, object?>("@DateCreated", resultEntity.DateCreated),
                new KeyValuePair<string, object?>("@DateLastModified", resultEntity.DateLastModified),
                new KeyValuePair<string, object?>("@DateSynchronized", resultEntity.DateSynchronized),
                new KeyValuePair<string, object?>("@DriveSerialNumber", resultEntity.DriveSerialNumber),
                new KeyValuePair<string, object?>("@DriveTypeCodeType", resultEntity.DriveTypeCodeType),
                new KeyValuePair<string, object?>("@DriveTypeCode", resultEntity.DriveTypeCode),
                new KeyValuePair<string, object?>("@DriveTotalSize", resultEntity.DriveTotalSize),
                new KeyValuePair<string, object?>("@DriveFreeSpace", resultEntity.DriveFreeSpace),
                new KeyValuePair<string, object?>("@Recursive", resultEntity.Recursive),
                new KeyValuePair<string, object?>("@Sorted", resultEntity.Sorted),
                new KeyValuePair<string, object?>("@StartDirectory", resultEntity.StartDirectory),
                new KeyValuePair<string, object?>("@ExtraField1", resultEntity.ExtraField1),
                new KeyValuePair<string, object?>("@ExtraField2", resultEntity.ExtraField2),
                new KeyValuePair<string, object?>("@ExtraField3", resultEntity.ExtraField3),
                new KeyValuePair<string, object?>("@ExtraField4", resultEntity.ExtraField4),
                new KeyValuePair<string, object?>("@ExtraField5", resultEntity.ExtraField5),
                new KeyValuePair<string, object?>("@ExtraField6", resultEntity.ExtraField6),
                new KeyValuePair<string, object?>("@ExtraField7", resultEntity.ExtraField7),
                new KeyValuePair<string, object?>("@ExtraField8", resultEntity.ExtraField8),
                new KeyValuePair<string, object?>("@ExtraField9", resultEntity.ExtraField9),
                new KeyValuePair<string, object?>("@ExtraField10", resultEntity.ExtraField10),
                new KeyValuePair<string, object?>("@ExtraField11", resultEntity.ExtraField11),
                new KeyValuePair<string, object?>("@ExtraField12", resultEntity.ExtraField12),
                new KeyValuePair<string, object?>("@ExtraField13", resultEntity.ExtraField13),
                new KeyValuePair<string, object?>("@ExtraField14", resultEntity.ExtraField14),
                new KeyValuePair<string, object?>("@ExtraField15", resultEntity.ExtraField15),
                new KeyValuePair<string, object?>("@ExtraField16", resultEntity.ExtraField16),
            };
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

        private object UpdateParameters(CatalogBuilder resultEntity)
        {
            return new[]
            {
                new KeyValuePair<string, object?>("@Name", resultEntity.Name),
                new KeyValuePair<string, object?>("@DateCreated", resultEntity.DateCreated),
                new KeyValuePair<string, object?>("@DateLastModified", resultEntity.DateLastModified),
                new KeyValuePair<string, object?>("@DateSynchronized", resultEntity.DateSynchronized),
                new KeyValuePair<string, object?>("@DriveSerialNumber", resultEntity.DriveSerialNumber),
                new KeyValuePair<string, object?>("@DriveTypeCodeType", resultEntity.DriveTypeCodeType),
                new KeyValuePair<string, object?>("@DriveTypeCode", resultEntity.DriveTypeCode),
                new KeyValuePair<string, object?>("@DriveTotalSize", resultEntity.DriveTotalSize),
                new KeyValuePair<string, object?>("@DriveFreeSpace", resultEntity.DriveFreeSpace),
                new KeyValuePair<string, object?>("@Recursive", resultEntity.Recursive),
                new KeyValuePair<string, object?>("@Sorted", resultEntity.Sorted),
                new KeyValuePair<string, object?>("@StartDirectory", resultEntity.StartDirectory),
                new KeyValuePair<string, object?>("@ExtraField1", resultEntity.ExtraField1),
                new KeyValuePair<string, object?>("@ExtraField2", resultEntity.ExtraField2),
                new KeyValuePair<string, object?>("@ExtraField3", resultEntity.ExtraField3),
                new KeyValuePair<string, object?>("@ExtraField4", resultEntity.ExtraField4),
                new KeyValuePair<string, object?>("@ExtraField5", resultEntity.ExtraField5),
                new KeyValuePair<string, object?>("@ExtraField6", resultEntity.ExtraField6),
                new KeyValuePair<string, object?>("@ExtraField7", resultEntity.ExtraField7),
                new KeyValuePair<string, object?>("@ExtraField8", resultEntity.ExtraField8),
                new KeyValuePair<string, object?>("@ExtraField9", resultEntity.ExtraField9),
                new KeyValuePair<string, object?>("@ExtraField10", resultEntity.ExtraField10),
                new KeyValuePair<string, object?>("@ExtraField11", resultEntity.ExtraField11),
                new KeyValuePair<string, object?>("@ExtraField12", resultEntity.ExtraField12),
                new KeyValuePair<string, object?>("@ExtraField13", resultEntity.ExtraField13),
                new KeyValuePair<string, object?>("@ExtraField14", resultEntity.ExtraField14),
                new KeyValuePair<string, object?>("@ExtraField15", resultEntity.ExtraField15),
                new KeyValuePair<string, object?>("@ExtraField16", resultEntity.ExtraField16),
                new KeyValuePair<string, object?>("@IdOriginal", resultEntity.IdOriginal),
                new KeyValuePair<string, object?>("@NameOriginal", resultEntity.NameOriginal),
                new KeyValuePair<string, object?>("@DateCreatedOriginal", resultEntity.DateCreatedOriginal),
                new KeyValuePair<string, object?>("@DateLastModifiedOriginal", resultEntity.DateLastModifiedOriginal),
                new KeyValuePair<string, object?>("@DateSynchronizedOriginal", resultEntity.DateSynchronizedOriginal),
                new KeyValuePair<string, object?>("@DriveSerialNumberOriginal", resultEntity.DriveSerialNumberOriginal),
                new KeyValuePair<string, object?>("@DriveTypeCodeTypeOriginal", resultEntity.DriveTypeCodeTypeOriginal),
                new KeyValuePair<string, object?>("@DriveTypeCodeOriginal", resultEntity.DriveTypeCodeOriginal),
                new KeyValuePair<string, object?>("@DriveTotalSizeOriginal", resultEntity.DriveTotalSizeOriginal),
                new KeyValuePair<string, object?>("@DriveFreeSpaceOriginal", resultEntity.DriveFreeSpaceOriginal),
                new KeyValuePair<string, object?>("@RecursiveOriginal", resultEntity.RecursiveOriginal),
                new KeyValuePair<string, object?>("@SortedOriginal", resultEntity.SortedOriginal),
                new KeyValuePair<string, object?>("@StartDirectoryOriginal", resultEntity.StartDirectoryOriginal),
                new KeyValuePair<string, object?>("@ExtraField1Original", resultEntity.ExtraField1Original),
                new KeyValuePair<string, object?>("@ExtraField2Original", resultEntity.ExtraField2Original),
                new KeyValuePair<string, object?>("@ExtraField3Original", resultEntity.ExtraField3Original),
                new KeyValuePair<string, object?>("@ExtraField4Original", resultEntity.ExtraField4Original),
                new KeyValuePair<string, object?>("@ExtraField5Original", resultEntity.ExtraField5Original),
                new KeyValuePair<string, object?>("@ExtraField6Original", resultEntity.ExtraField6Original),
                new KeyValuePair<string, object?>("@ExtraField7Original", resultEntity.ExtraField7Original),
                new KeyValuePair<string, object?>("@ExtraField8Original", resultEntity.ExtraField8Original),
                new KeyValuePair<string, object?>("@ExtraField9Original", resultEntity.ExtraField9Original),
                new KeyValuePair<string, object?>("@ExtraField10Original", resultEntity.ExtraField10Original),
                new KeyValuePair<string, object?>("@ExtraField11Original", resultEntity.ExtraField11Original),
                new KeyValuePair<string, object?>("@ExtraField12Original", resultEntity.ExtraField12Original),
                new KeyValuePair<string, object?>("@ExtraField13Original", resultEntity.ExtraField13Original),
                new KeyValuePair<string, object?>("@ExtraField14Original", resultEntity.ExtraField14Original),
                new KeyValuePair<string, object?>("@ExtraField15Original", resultEntity.ExtraField15Original),
                new KeyValuePair<string, object?>("@ExtraField16Original", resultEntity.ExtraField16Original),
            };
        }

        private CatalogBuilder DeleteResultEntity(CatalogBuilder resultEntity)
        {

            return resultEntity;
        }

        private object DeleteParameters(CatalogBuilder resultEntity)
        {
            return new[]
            {
                new KeyValuePair<string, object?>("@Id", resultEntity.Id),
                new KeyValuePair<string, object?>("@Name", resultEntity.Name),
                new KeyValuePair<string, object?>("@DateCreated", resultEntity.DateCreated),
                new KeyValuePair<string, object?>("@DateLastModified", resultEntity.DateLastModified),
                new KeyValuePair<string, object?>("@DateSynchronized", resultEntity.DateSynchronized),
                new KeyValuePair<string, object?>("@DriveSerialNumber", resultEntity.DriveSerialNumber),
                new KeyValuePair<string, object?>("@DriveTypeCodeType", resultEntity.DriveTypeCodeType),
                new KeyValuePair<string, object?>("@DriveTypeCode", resultEntity.DriveTypeCode),
                new KeyValuePair<string, object?>("@DriveTotalSize", resultEntity.DriveTotalSize),
                new KeyValuePair<string, object?>("@DriveFreeSpace", resultEntity.DriveFreeSpace),
                new KeyValuePair<string, object?>("@Recursive", resultEntity.Recursive),
                new KeyValuePair<string, object?>("@Sorted", resultEntity.Sorted),
                new KeyValuePair<string, object?>("@StartDirectory", resultEntity.StartDirectory),
                new KeyValuePair<string, object?>("@ExtraField1", resultEntity.ExtraField1),
                new KeyValuePair<string, object?>("@ExtraField2", resultEntity.ExtraField2),
                new KeyValuePair<string, object?>("@ExtraField3", resultEntity.ExtraField3),
                new KeyValuePair<string, object?>("@ExtraField4", resultEntity.ExtraField4),
                new KeyValuePair<string, object?>("@ExtraField5", resultEntity.ExtraField5),
                new KeyValuePair<string, object?>("@ExtraField6", resultEntity.ExtraField6),
                new KeyValuePair<string, object?>("@ExtraField7", resultEntity.ExtraField7),
                new KeyValuePair<string, object?>("@ExtraField8", resultEntity.ExtraField8),
                new KeyValuePair<string, object?>("@ExtraField9", resultEntity.ExtraField9),
                new KeyValuePair<string, object?>("@ExtraField10", resultEntity.ExtraField10),
                new KeyValuePair<string, object?>("@ExtraField11", resultEntity.ExtraField11),
                new KeyValuePair<string, object?>("@ExtraField12", resultEntity.ExtraField12),
                new KeyValuePair<string, object?>("@ExtraField13", resultEntity.ExtraField13),
                new KeyValuePair<string, object?>("@ExtraField14", resultEntity.ExtraField14),
                new KeyValuePair<string, object?>("@ExtraField15", resultEntity.ExtraField15),
                new KeyValuePair<string, object?>("@ExtraField16", resultEntity.ExtraField16),
            };
        }
    }
}
