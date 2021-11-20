using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PDC.Net.Core.Entities
{
#nullable enable
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator", @"1.0.0.0")]
    public partial class CatalogBuilder
    {
        public int Id
        {
            get;
            set;
        }

        [StringLength(64)]
        [Required]
        public string Name
        {
            get;
            set;
        }

        [Required]
        public DateTime DateCreated
        {
            get;
            set;
        }

        [Required]
        public DateTime DateLastModified
        {
            get;
            set;
        }

        [Required]
        public DateTime DateSynchronized
        {
            get;
            set;
        }

        [StringLength(9)]
        [Required]
        [RegularExpression(@"[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}")]
        public string DriveSerialNumber
        {
            get;
            set;
        }

        [DefaultValue(@"CDT")]
        [StringLength(3)]
        [Required]
        public string DriveTypeCodeType
        {
            get;
            set;
        }

        [StringLength(3)]
        [Required]
        public string DriveTypeCode
        {
            get;
            set;
        }

        public string? DriveTypeDescription
        {
            get;
            set;
        }

        [Required]
        public int DriveTotalSize
        {
            get;
            set;
        }

        [Required]
        public int DriveFreeSpace
        {
            get;
            set;
        }

        [Required]
        public bool Recursive
        {
            get;
            set;
        }

        [Required]
        public bool Sorted
        {
            get;
            set;
        }

        [StringLength(512)]
        [Required]
        public string StartDirectory
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField1
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField2
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField3
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField4
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField5
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField6
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField7
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField8
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField9
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField10
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField11
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField12
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField13
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField14
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField15
        {
            get;
            set;
        }

        [StringLength(64)]
        public string? ExtraField16
        {
            get;
            set;
        }

        public bool IsExistingEntity
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public int? IdOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? NameOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public DateTime? DateCreatedOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public DateTime? DateLastModifiedOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public DateTime? DateSynchronizedOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? DriveSerialNumberOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? DriveTypeCodeTypeOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? DriveTypeCodeOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public int? DriveTotalSizeOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public int? DriveFreeSpaceOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public bool? RecursiveOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public bool? SortedOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? StartDirectoryOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField1Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField2Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField3Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField4Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField5Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField6Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField7Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField8Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField9Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField10Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField11Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField12Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField13Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField14Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField15Original
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtraField16Original
        {
            get;
            set;
        }

        public CatalogBuilder SetId(int value)
        {
            this.Id = value;
            return this;
        }

        public CatalogBuilder SetName(string value)
        {
            this.Name = value;
            return this;
        }

        public CatalogBuilder SetDateCreated(DateTime value)
        {
            this.DateCreated = value;
            return this;
        }

        public CatalogBuilder SetDateLastModified(DateTime value)
        {
            this.DateLastModified = value;
            return this;
        }

        public CatalogBuilder SetDateSynchronized(DateTime value)
        {
            this.DateSynchronized = value;
            return this;
        }

        public CatalogBuilder SetDriveSerialNumber(string value)
        {
            this.DriveSerialNumber = value;
            return this;
        }

        public CatalogBuilder SetDriveTypeCodeType(string value)
        {
            this.DriveTypeCodeType = value;
            return this;
        }

        public CatalogBuilder SetDriveTypeCode(string value)
        {
            this.DriveTypeCode = value;
            return this;
        }

        public CatalogBuilder SetDriveTypeDescription(string? value)
        {
            this.DriveTypeDescription = value;
            return this;
        }

        public CatalogBuilder SetDriveTotalSize(int value)
        {
            this.DriveTotalSize = value;
            return this;
        }

        public CatalogBuilder SetDriveFreeSpace(int value)
        {
            this.DriveFreeSpace = value;
            return this;
        }

        public CatalogBuilder SetRecursive(bool value)
        {
            this.Recursive = value;
            return this;
        }

        public CatalogBuilder SetSorted(bool value)
        {
            this.Sorted = value;
            return this;
        }

        public CatalogBuilder SetStartDirectory(string value)
        {
            this.StartDirectory = value;
            return this;
        }

        public CatalogBuilder SetExtraField1(string? value)
        {
            this.ExtraField1 = value;
            return this;
        }

        public CatalogBuilder SetExtraField2(string? value)
        {
            this.ExtraField2 = value;
            return this;
        }

        public CatalogBuilder SetExtraField3(string? value)
        {
            this.ExtraField3 = value;
            return this;
        }

        public CatalogBuilder SetExtraField4(string? value)
        {
            this.ExtraField4 = value;
            return this;
        }

        public CatalogBuilder SetExtraField5(string? value)
        {
            this.ExtraField5 = value;
            return this;
        }

        public CatalogBuilder SetExtraField6(string? value)
        {
            this.ExtraField6 = value;
            return this;
        }

        public CatalogBuilder SetExtraField7(string? value)
        {
            this.ExtraField7 = value;
            return this;
        }

        public CatalogBuilder SetExtraField8(string? value)
        {
            this.ExtraField8 = value;
            return this;
        }

        public CatalogBuilder SetExtraField9(string? value)
        {
            this.ExtraField9 = value;
            return this;
        }

        public CatalogBuilder SetExtraField10(string? value)
        {
            this.ExtraField10 = value;
            return this;
        }

        public CatalogBuilder SetExtraField11(string? value)
        {
            this.ExtraField11 = value;
            return this;
        }

        public CatalogBuilder SetExtraField12(string? value)
        {
            this.ExtraField12 = value;
            return this;
        }

        public CatalogBuilder SetExtraField13(string? value)
        {
            this.ExtraField13 = value;
            return this;
        }

        public CatalogBuilder SetExtraField14(string? value)
        {
            this.ExtraField14 = value;
            return this;
        }

        public CatalogBuilder SetExtraField15(string? value)
        {
            this.ExtraField15 = value;
            return this;
        }

        public CatalogBuilder SetExtraField16(string? value)
        {
            this.ExtraField16 = value;
            return this;
        }

        public CatalogBuilder SetIsExistingEntity(bool value)
        {
            this.IsExistingEntity = value;
            return this;
        }

        public CatalogBuilder SetIdOriginal(int? value)
        {
            this.IdOriginal = value;
            return this;
        }

        public CatalogBuilder SetNameOriginal(string? value)
        {
            this.NameOriginal = value;
            return this;
        }

        public CatalogBuilder SetDateCreatedOriginal(DateTime? value)
        {
            this.DateCreatedOriginal = value;
            return this;
        }

        public CatalogBuilder SetDateLastModifiedOriginal(DateTime? value)
        {
            this.DateLastModifiedOriginal = value;
            return this;
        }

        public CatalogBuilder SetDateSynchronizedOriginal(DateTime? value)
        {
            this.DateSynchronizedOriginal = value;
            return this;
        }

        public CatalogBuilder SetDriveSerialNumberOriginal(string? value)
        {
            this.DriveSerialNumberOriginal = value;
            return this;
        }

        public CatalogBuilder SetDriveTypeCodeTypeOriginal(string? value)
        {
            this.DriveTypeCodeTypeOriginal = value;
            return this;
        }

        public CatalogBuilder SetDriveTypeCodeOriginal(string? value)
        {
            this.DriveTypeCodeOriginal = value;
            return this;
        }

        public CatalogBuilder SetDriveTotalSizeOriginal(int? value)
        {
            this.DriveTotalSizeOriginal = value;
            return this;
        }

        public CatalogBuilder SetDriveFreeSpaceOriginal(int? value)
        {
            this.DriveFreeSpaceOriginal = value;
            return this;
        }

        public CatalogBuilder SetRecursiveOriginal(bool? value)
        {
            this.RecursiveOriginal = value;
            return this;
        }

        public CatalogBuilder SetSortedOriginal(bool? value)
        {
            this.SortedOriginal = value;
            return this;
        }

        public CatalogBuilder SetStartDirectoryOriginal(string? value)
        {
            this.StartDirectoryOriginal = value;
            return this;
        }

        public CatalogBuilder SetExtraField1Original(string? value)
        {
            this.ExtraField1Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField2Original(string? value)
        {
            this.ExtraField2Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField3Original(string? value)
        {
            this.ExtraField3Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField4Original(string? value)
        {
            this.ExtraField4Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField5Original(string? value)
        {
            this.ExtraField5Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField6Original(string? value)
        {
            this.ExtraField6Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField7Original(string? value)
        {
            this.ExtraField7Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField8Original(string? value)
        {
            this.ExtraField8Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField9Original(string? value)
        {
            this.ExtraField9Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField10Original(string? value)
        {
            this.ExtraField10Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField11Original(string? value)
        {
            this.ExtraField11Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField12Original(string? value)
        {
            this.ExtraField12Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField13Original(string? value)
        {
            this.ExtraField13Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField14Original(string? value)
        {
            this.ExtraField14Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField15Original(string? value)
        {
            this.ExtraField15Original = value;
            return this;
        }

        public CatalogBuilder SetExtraField16Original(string? value)
        {
            this.ExtraField16Original = value;
            return this;
        }

        public Catalog Build()
        {
            return new Catalog(Id, Name, DateCreated, DateLastModified, DateSynchronized, DriveSerialNumber, DriveTypeCodeType, DriveTypeCode, DriveTypeDescription, DriveTotalSize, DriveFreeSpace, Recursive, Sorted, StartDirectory, ExtraField1, ExtraField2, ExtraField3, ExtraField4, ExtraField5, ExtraField6, ExtraField7, ExtraField8, ExtraField9, ExtraField10, ExtraField11, ExtraField12, ExtraField13, ExtraField14, ExtraField15, ExtraField16, IsExistingEntity, IdOriginal, NameOriginal, DateCreatedOriginal, DateLastModifiedOriginal, DateSynchronizedOriginal, DriveSerialNumberOriginal, DriveTypeCodeTypeOriginal, DriveTypeCodeOriginal, DriveTotalSizeOriginal, DriveFreeSpaceOriginal, RecursiveOriginal, SortedOriginal, StartDirectoryOriginal, ExtraField1Original, ExtraField2Original, ExtraField3Original, ExtraField4Original, ExtraField5Original, ExtraField6Original, ExtraField7Original, ExtraField8Original, ExtraField9Original, ExtraField10Original, ExtraField11Original, ExtraField12Original, ExtraField13Original, ExtraField14Original, ExtraField15Original, ExtraField16Original);
        }

        public CatalogBuilder Update(Catalog instance)
        {
            this.Id = instance.Id;
            this.Name = instance.Name;
            this.DateCreated = instance.DateCreated;
            this.DateLastModified = instance.DateLastModified;
            this.DateSynchronized = instance.DateSynchronized;
            this.DriveSerialNumber = instance.DriveSerialNumber;
            this.DriveTypeCodeType = instance.DriveTypeCodeType;
            this.DriveTypeCode = instance.DriveTypeCode;
            this.DriveTypeDescription = instance.DriveTypeDescription;
            this.DriveTotalSize = instance.DriveTotalSize;
            this.DriveFreeSpace = instance.DriveFreeSpace;
            this.Recursive = instance.Recursive;
            this.Sorted = instance.Sorted;
            this.StartDirectory = instance.StartDirectory;
            this.ExtraField1 = instance.ExtraField1;
            this.ExtraField2 = instance.ExtraField2;
            this.ExtraField3 = instance.ExtraField3;
            this.ExtraField4 = instance.ExtraField4;
            this.ExtraField5 = instance.ExtraField5;
            this.ExtraField6 = instance.ExtraField6;
            this.ExtraField7 = instance.ExtraField7;
            this.ExtraField8 = instance.ExtraField8;
            this.ExtraField9 = instance.ExtraField9;
            this.ExtraField10 = instance.ExtraField10;
            this.ExtraField11 = instance.ExtraField11;
            this.ExtraField12 = instance.ExtraField12;
            this.ExtraField13 = instance.ExtraField13;
            this.ExtraField14 = instance.ExtraField14;
            this.ExtraField15 = instance.ExtraField15;
            this.ExtraField16 = instance.ExtraField16;
            this.IsExistingEntity = instance.IsExistingEntity;
            this.IdOriginal = instance.IdOriginal;
            this.NameOriginal = instance.NameOriginal;
            this.DateCreatedOriginal = instance.DateCreatedOriginal;
            this.DateLastModifiedOriginal = instance.DateLastModifiedOriginal;
            this.DateSynchronizedOriginal = instance.DateSynchronizedOriginal;
            this.DriveSerialNumberOriginal = instance.DriveSerialNumberOriginal;
            this.DriveTypeCodeTypeOriginal = instance.DriveTypeCodeTypeOriginal;
            this.DriveTypeCodeOriginal = instance.DriveTypeCodeOriginal;
            this.DriveTotalSizeOriginal = instance.DriveTotalSizeOriginal;
            this.DriveFreeSpaceOriginal = instance.DriveFreeSpaceOriginal;
            this.RecursiveOriginal = instance.RecursiveOriginal;
            this.SortedOriginal = instance.SortedOriginal;
            this.StartDirectoryOriginal = instance.StartDirectoryOriginal;
            this.ExtraField1Original = instance.ExtraField1Original;
            this.ExtraField2Original = instance.ExtraField2Original;
            this.ExtraField3Original = instance.ExtraField3Original;
            this.ExtraField4Original = instance.ExtraField4Original;
            this.ExtraField5Original = instance.ExtraField5Original;
            this.ExtraField6Original = instance.ExtraField6Original;
            this.ExtraField7Original = instance.ExtraField7Original;
            this.ExtraField8Original = instance.ExtraField8Original;
            this.ExtraField9Original = instance.ExtraField9Original;
            this.ExtraField10Original = instance.ExtraField10Original;
            this.ExtraField11Original = instance.ExtraField11Original;
            this.ExtraField12Original = instance.ExtraField12Original;
            this.ExtraField13Original = instance.ExtraField13Original;
            this.ExtraField14Original = instance.ExtraField14Original;
            this.ExtraField15Original = instance.ExtraField15Original;
            this.ExtraField16Original = instance.ExtraField16Original;
            return this;
        }

        public CatalogBuilder()
        {
            Name = string.Empty;
            DriveSerialNumber = "0000-0000";
            DriveTypeCodeType = "CDT";
            DriveTypeCode = string.Empty;
            DriveTypeDescription = string.Empty;
            StartDirectory = string.Empty;
        }

        public CatalogBuilder(Catalog instance)
        {
            Id = instance.Id;
            Name = instance.Name;
            DateCreated = instance.DateCreated;
            DateLastModified = instance.DateLastModified;
            DateSynchronized = instance.DateSynchronized;
            DriveSerialNumber = instance.DriveSerialNumber;
            DriveTypeCodeType = instance.DriveTypeCodeType;
            DriveTypeCode = instance.DriveTypeCode;
            DriveTypeDescription = instance.DriveTypeDescription;
            DriveTotalSize = instance.DriveTotalSize;
            DriveFreeSpace = instance.DriveFreeSpace;
            Recursive = instance.Recursive;
            Sorted = instance.Sorted;
            StartDirectory = instance.StartDirectory;
            ExtraField1 = instance.ExtraField1;
            ExtraField2 = instance.ExtraField2;
            ExtraField3 = instance.ExtraField3;
            ExtraField4 = instance.ExtraField4;
            ExtraField5 = instance.ExtraField5;
            ExtraField6 = instance.ExtraField6;
            ExtraField7 = instance.ExtraField7;
            ExtraField8 = instance.ExtraField8;
            ExtraField9 = instance.ExtraField9;
            ExtraField10 = instance.ExtraField10;
            ExtraField11 = instance.ExtraField11;
            ExtraField12 = instance.ExtraField12;
            ExtraField13 = instance.ExtraField13;
            ExtraField14 = instance.ExtraField14;
            ExtraField15 = instance.ExtraField15;
            ExtraField16 = instance.ExtraField16;
            IsExistingEntity = instance.IsExistingEntity;
            IdOriginal = instance.IdOriginal;
            NameOriginal = instance.NameOriginal;
            DateCreatedOriginal = instance.DateCreatedOriginal;
            DateLastModifiedOriginal = instance.DateLastModifiedOriginal;
            DateSynchronizedOriginal = instance.DateSynchronizedOriginal;
            DriveSerialNumberOriginal = instance.DriveSerialNumberOriginal;
            DriveTypeCodeTypeOriginal = instance.DriveTypeCodeTypeOriginal;
            DriveTypeCodeOriginal = instance.DriveTypeCodeOriginal;
            DriveTotalSizeOriginal = instance.DriveTotalSizeOriginal;
            DriveFreeSpaceOriginal = instance.DriveFreeSpaceOriginal;
            RecursiveOriginal = instance.RecursiveOriginal;
            SortedOriginal = instance.SortedOriginal;
            StartDirectoryOriginal = instance.StartDirectoryOriginal;
            ExtraField1Original = instance.ExtraField1Original;
            ExtraField2Original = instance.ExtraField2Original;
            ExtraField3Original = instance.ExtraField3Original;
            ExtraField4Original = instance.ExtraField4Original;
            ExtraField5Original = instance.ExtraField5Original;
            ExtraField6Original = instance.ExtraField6Original;
            ExtraField7Original = instance.ExtraField7Original;
            ExtraField8Original = instance.ExtraField8Original;
            ExtraField9Original = instance.ExtraField9Original;
            ExtraField10Original = instance.ExtraField10Original;
            ExtraField11Original = instance.ExtraField11Original;
            ExtraField12Original = instance.ExtraField12Original;
            ExtraField13Original = instance.ExtraField13Original;
            ExtraField14Original = instance.ExtraField14Original;
            ExtraField15Original = instance.ExtraField15Original;
            ExtraField16Original = instance.ExtraField16Original;
        }

        public CatalogBuilder(int id,
                              string name,
                              DateTime dateCreated,
                              DateTime dateLastModified,
                              DateTime dateSynchronized,
                              string driveSerialNumber,
                              string driveTypeCodeType,
                              string driveTypeCode,
                              string? driveTypeDescription,
                              int driveTotalSize,
                              int driveFreeSpace,
                              bool recursive,
                              bool sorted,
                              string startDirectory,
                              string? extraField1,
                              string? extraField2,
                              string? extraField3,
                              string? extraField4,
                              string? extraField5,
                              string? extraField6,
                              string? extraField7,
                              string? extraField8,
                              string? extraField9,
                              string? extraField10,
                              string? extraField11,
                              string? extraField12,
                              string? extraField13,
                              string? extraField14,
                              string? extraField15,
                              string? extraField16,
                              bool isExistingEntity = false,
                              int? idOriginal = default,
                              string? nameOriginal = default,
                              DateTime? dateCreatedOriginal = default,
                              DateTime? dateLastModifiedOriginal = default,
                              DateTime? dateSynchronizedOriginal = default,
                              string? driveSerialNumberOriginal = default,
                              string? driveTypeCodeTypeOriginal = default,
                              string? driveTypeCodeOriginal = default,
                              int? driveTotalSizeOriginal = default,
                              int? driveFreeSpaceOriginal = default,
                              bool? recursiveOriginal = default,
                              bool? sortedOriginal = default,
                              string? startDirectoryOriginal = default,
                              string? extraField1Original = default,
                              string? extraField2Original = default,
                              string? extraField3Original = default,
                              string? extraField4Original = default,
                              string? extraField5Original = default,
                              string? extraField6Original = default,
                              string? extraField7Original = default,
                              string? extraField8Original = default,
                              string? extraField9Original = default,
                              string? extraField10Original = default,
                              string? extraField11Original = default,
                              string? extraField12Original = default,
                              string? extraField13Original = default,
                              string? extraField14Original = default,
                              string? extraField15Original = default,
                              string? extraField16Original = default)
        {
            this.Id = id;
            this.Name = name;
            this.DateCreated = dateCreated;
            this.DateLastModified = dateLastModified;
            this.DateSynchronized = dateSynchronized;
            this.DriveSerialNumber = driveSerialNumber;
            this.DriveTypeCodeType = driveTypeCodeType;
            this.DriveTypeCode = driveTypeCode;
            this.DriveTypeDescription = driveTypeDescription;
            this.DriveTotalSize = driveTotalSize;
            this.DriveFreeSpace = driveFreeSpace;
            this.Recursive = recursive;
            this.Sorted = sorted;
            this.StartDirectory = startDirectory;
            this.ExtraField1 = extraField1;
            this.ExtraField2 = extraField2;
            this.ExtraField3 = extraField3;
            this.ExtraField4 = extraField4;
            this.ExtraField5 = extraField5;
            this.ExtraField6 = extraField6;
            this.ExtraField7 = extraField7;
            this.ExtraField8 = extraField8;
            this.ExtraField9 = extraField9;
            this.ExtraField10 = extraField10;
            this.ExtraField11 = extraField11;
            this.ExtraField12 = extraField12;
            this.ExtraField13 = extraField13;
            this.ExtraField14 = extraField14;
            this.ExtraField15 = extraField15;
            this.ExtraField16 = extraField16;
            this.IsExistingEntity = isExistingEntity;
            this.IdOriginal = idOriginal;
            this.NameOriginal = nameOriginal;
            this.DateCreatedOriginal = dateCreatedOriginal;
            this.DateLastModifiedOriginal = dateLastModifiedOriginal;
            this.DateSynchronizedOriginal = dateSynchronizedOriginal;
            this.DriveSerialNumberOriginal = driveSerialNumberOriginal;
            this.DriveTypeCodeTypeOriginal = driveTypeCodeTypeOriginal;
            this.DriveTypeCodeOriginal = driveTypeCodeOriginal;
            this.DriveTotalSizeOriginal = driveTotalSizeOriginal;
            this.DriveFreeSpaceOriginal = driveFreeSpaceOriginal;
            this.RecursiveOriginal = recursiveOriginal;
            this.SortedOriginal = sortedOriginal;
            this.StartDirectoryOriginal = startDirectoryOriginal;
            this.ExtraField1Original = extraField1Original;
            this.ExtraField2Original = extraField2Original;
            this.ExtraField3Original = extraField3Original;
            this.ExtraField4Original = extraField4Original;
            this.ExtraField5Original = extraField5Original;
            this.ExtraField6Original = extraField6Original;
            this.ExtraField7Original = extraField7Original;
            this.ExtraField8Original = extraField8Original;
            this.ExtraField9Original = extraField9Original;
            this.ExtraField10Original = extraField10Original;
            this.ExtraField11Original = extraField11Original;
            this.ExtraField12Original = extraField12Original;
            this.ExtraField13Original = extraField13Original;
            this.ExtraField14Original = extraField14Original;
            this.ExtraField15Original = extraField15Original;
            this.ExtraField16Original = extraField16Original;
        }
    }
#nullable restore
}

