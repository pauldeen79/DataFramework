using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PDC.Net.Core.Entities
{
#nullable enable
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator", @"1.0.0.0")]
    public partial class FileBuilder
    {
        public int Id
        {
            get;
            set;
        }

        [StringLength(512)]
        [Required]
        public string Path
        {
            get;
            set;
        }

        [StringLength(128)]
        [Required]
        public string FileName
        {
            get;
            set;
        }

        [StringLength(12)]
        [Required]
        public string Extension
        {
            get;
            set;
        }

        [Required]
        public System.DateTime Date
        {
            get;
            set;
        }

        [Required]
        public long Size
        {
            get;
            set;
        }

        [Required]
        public int CatalogId
        {
            get;
            set;
        }

        public string? CatalogName
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
        public string? PathOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? FileNameOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? ExtensionOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public System.DateTime? DateOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public long? SizeOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public int? CatalogIdOriginal
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

        public FileBuilder SetId(int value)
        {
            this.Id = value;
            return this;
        }

        public FileBuilder SetPath(string value)
        {
            this.Path = value;
            return this;
        }

        public FileBuilder SetFileName(string value)
        {
            this.FileName = value;
            return this;
        }

        public FileBuilder SetExtension(string value)
        {
            this.Extension = value;
            return this;
        }

        public FileBuilder SetDate(System.DateTime value)
        {
            this.Date = value;
            return this;
        }

        public FileBuilder SetSize(long value)
        {
            this.Size = value;
            return this;
        }

        public FileBuilder SetCatalogId(int value)
        {
            this.CatalogId = value;
            return this;
        }

        public FileBuilder SetCatalogName(string value)
        {
            this.CatalogName = value;
            return this;
        }

        public FileBuilder SetExtraField1(string? value)
        {
            this.ExtraField1 = value;
            return this;
        }

        public FileBuilder SetExtraField2(string? value)
        {
            this.ExtraField2 = value;
            return this;
        }

        public FileBuilder SetExtraField3(string? value)
        {
            this.ExtraField3 = value;
            return this;
        }

        public FileBuilder SetExtraField4(string? value)
        {
            this.ExtraField4 = value;
            return this;
        }

        public FileBuilder SetExtraField5(string? value)
        {
            this.ExtraField5 = value;
            return this;
        }

        public FileBuilder SetExtraField6(string? value)
        {
            this.ExtraField6 = value;
            return this;
        }

        public FileBuilder SetExtraField7(string? value)
        {
            this.ExtraField7 = value;
            return this;
        }

        public FileBuilder SetExtraField8(string? value)
        {
            this.ExtraField8 = value;
            return this;
        }

        public FileBuilder SetExtraField9(string? value)
        {
            this.ExtraField9 = value;
            return this;
        }

        public FileBuilder SetExtraField10(string? value)
        {
            this.ExtraField10 = value;
            return this;
        }

        public FileBuilder SetExtraField11(string? value)
        {
            this.ExtraField11 = value;
            return this;
        }

        public FileBuilder SetExtraField12(string? value)
        {
            this.ExtraField12 = value;
            return this;
        }

        public FileBuilder SetExtraField13(string? value)
        {
            this.ExtraField13 = value;
            return this;
        }

        public FileBuilder SetExtraField14(string? value)
        {
            this.ExtraField14 = value;
            return this;
        }

        public FileBuilder SetExtraField15(string? value)
        {
            this.ExtraField15 = value;
            return this;
        }

        public FileBuilder SetExtraField16(string? value)
        {
            this.ExtraField16 = value;
            return this;
        }

        public FileBuilder SetIsExistingEntity(bool value)
        {
            this.IsExistingEntity = value;
            return this;
        }

        public FileBuilder SetIdOriginal(int? value)
        {
            this.IdOriginal = value;
            return this;
        }

        public FileBuilder SetPathOriginal(string? value)
        {
            this.PathOriginal = value;
            return this;
        }

        public FileBuilder SetFileNameOriginal(string? value)
        {
            this.FileNameOriginal = value;
            return this;
        }

        public FileBuilder SetExtensionOriginal(string? value)
        {
            this.ExtensionOriginal = value;
            return this;
        }

        public FileBuilder SetDateOriginal(System.DateTime? value)
        {
            this.DateOriginal = value;
            return this;
        }

        public FileBuilder SetSizeOriginal(long? value)
        {
            this.SizeOriginal = value;
            return this;
        }

        public FileBuilder SetCatalogIdOriginal(int? value)
        {
            this.CatalogIdOriginal = value;
            return this;
        }

        public FileBuilder SetExtraField1Original(string? value)
        {
            this.ExtraField1Original = value;
            return this;
        }

        public FileBuilder SetExtraField2Original(string? value)
        {
            this.ExtraField2Original = value;
            return this;
        }

        public FileBuilder SetExtraField3Original(string? value)
        {
            this.ExtraField3Original = value;
            return this;
        }

        public FileBuilder SetExtraField4Original(string? value)
        {
            this.ExtraField4Original = value;
            return this;
        }

        public FileBuilder SetExtraField5Original(string? value)
        {
            this.ExtraField5Original = value;
            return this;
        }

        public FileBuilder SetExtraField6Original(string? value)
        {
            this.ExtraField6Original = value;
            return this;
        }

        public FileBuilder SetExtraField7Original(string? value)
        {
            this.ExtraField7Original = value;
            return this;
        }

        public FileBuilder SetExtraField8Original(string? value)
        {
            this.ExtraField8Original = value;
            return this;
        }

        public FileBuilder SetExtraField9Original(string? value)
        {
            this.ExtraField9Original = value;
            return this;
        }

        public FileBuilder SetExtraField10Original(string? value)
        {
            this.ExtraField10Original = value;
            return this;
        }

        public FileBuilder SetExtraField11Original(string? value)
        {
            this.ExtraField11Original = value;
            return this;
        }

        public FileBuilder SetExtraField12Original(string? value)
        {
            this.ExtraField12Original = value;
            return this;
        }

        public FileBuilder SetExtraField13Original(string? value)
        {
            this.ExtraField13Original = value;
            return this;
        }

        public FileBuilder SetExtraField14Original(string? value)
        {
            this.ExtraField14Original = value;
            return this;
        }

        public FileBuilder SetExtraField15Original(string? value)
        {
            this.ExtraField15Original = value;
            return this;
        }

        public FileBuilder SetExtraField16Original(string? value)
        {
            this.ExtraField16Original = value;
            return this;
        }

        public File Build()
        {
            return new File(Id, Path, FileName, Extension, Date, Size, CatalogId, CatalogName, ExtraField1, ExtraField2, ExtraField3, ExtraField4, ExtraField5, ExtraField6, ExtraField7, ExtraField8, ExtraField9, ExtraField10, ExtraField11, ExtraField12, ExtraField13, ExtraField14, ExtraField15, ExtraField16, IsExistingEntity, IdOriginal, PathOriginal, FileNameOriginal, ExtensionOriginal, DateOriginal, SizeOriginal, CatalogIdOriginal, ExtraField1Original, ExtraField2Original, ExtraField3Original, ExtraField4Original, ExtraField5Original, ExtraField6Original, ExtraField7Original, ExtraField8Original, ExtraField9Original, ExtraField10Original, ExtraField11Original, ExtraField12Original, ExtraField13Original, ExtraField14Original, ExtraField15Original, ExtraField16Original);
        }

        public FileBuilder Update(File instance)
        {
            this.Id = instance.Id;
            this.Path = instance.Path;
            this.FileName = instance.FileName;
            this.Extension = instance.Extension;
            this.Date = instance.Date;
            this.Size = instance.Size;
            this.CatalogId = instance.CatalogId;
            this.CatalogName = instance.CatalogName;
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
            this.PathOriginal = instance.PathOriginal;
            this.FileNameOriginal = instance.FileNameOriginal;
            this.ExtensionOriginal = instance.ExtensionOriginal;
            this.DateOriginal = instance.DateOriginal;
            this.SizeOriginal = instance.SizeOriginal;
            this.CatalogIdOriginal = instance.CatalogIdOriginal;
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

        public FileBuilder()
        {
            FileName = string.Empty;
            Path = string.Empty;
            Extension = string.Empty;
        }

        public FileBuilder(File instance)
        {
            Id = instance.Id;
            Path = instance.Path;
            FileName = instance.FileName;
            Extension = instance.Extension;
            Date = instance.Date;
            Size = instance.Size;
            CatalogId = instance.CatalogId;
            CatalogName = instance.CatalogName;
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
            PathOriginal = instance.PathOriginal;
            FileNameOriginal = instance.FileNameOriginal;
            ExtensionOriginal = instance.ExtensionOriginal;
            DateOriginal = instance.DateOriginal;
            SizeOriginal = instance.SizeOriginal;
            CatalogIdOriginal = instance.CatalogIdOriginal;
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

        public FileBuilder(int id,
                           string path,
                           string fileName,
                           string extension,
                           System.DateTime date,
                           long size,
                           int catalogId,
                           string catalogName,
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
                           string? pathOriginal = default,
                           string? fileNameOriginal = default,
                           string? extensionOriginal = default,
                           System.DateTime? dateOriginal = default,
                           long? sizeOriginal = default,
                           int? catalogIdOriginal = default,
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
            this.Path = path;
            this.FileName = fileName;
            this.Extension = extension;
            this.Date = date;
            this.Size = size;
            this.CatalogId = catalogId;
            this.CatalogName = catalogName;
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
            this.PathOriginal = pathOriginal;
            this.FileNameOriginal = fileNameOriginal;
            this.ExtensionOriginal = extensionOriginal;
            this.DateOriginal = dateOriginal;
            this.SizeOriginal = sizeOriginal;
            this.CatalogIdOriginal = catalogIdOriginal;
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

