using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PDC.Net.Core.Entities
{
#nullable enable
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator", @"1.0.0.0")]
    [ExcludeFromCodeCoverage]
    public partial class ExtraFieldBuilder
    {
        [StringLength(64)]
        [Required]
        public string EntityName
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

        [StringLength(512)]
        public string? Description
        {
            get;
            set;
        }

        public byte FieldNumber
        {
            get;
            set;
        }

        [StringLength(64)]
        [Required]
        public string FieldType
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
        public string? EntityNameOriginal
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
        public string? DescriptionOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public byte? FieldNumberOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? FieldTypeOriginal
        {
            get;
            set;
        }

        public ExtraFieldBuilder SetEntityName(string value)
        {
            this.EntityName = value;
            return this;
        }

        public ExtraFieldBuilder SetName(string value)
        {
            this.Name = value;
            return this;
        }

        public ExtraFieldBuilder SetDescription(string? value)
        {
            this.Description = value;
            return this;
        }

        public ExtraFieldBuilder SetFieldNumber(byte value)
        {
            this.FieldNumber = value;
            return this;
        }

        public ExtraFieldBuilder SetFieldType(string value)
        {
            this.FieldType = value;
            return this;
        }

        public ExtraFieldBuilder SetIsExistingEntity(bool value)
        {
            this.IsExistingEntity = value;
            return this;
        }

        public ExtraFieldBuilder SetEntityNameOriginal(string? value)
        {
            this.EntityNameOriginal = value;
            return this;
        }

        public ExtraFieldBuilder SetNameOriginal(string? value)
        {
            this.NameOriginal = value;
            return this;
        }

        public ExtraFieldBuilder SetDescriptionOriginal(string? value)
        {
            this.DescriptionOriginal = value;
            return this;
        }

        public ExtraFieldBuilder SetFieldNumberOriginal(byte? value)
        {
            this.FieldNumberOriginal = value;
            return this;
        }

        public ExtraFieldBuilder SetFieldTypeOriginal(string? value)
        {
            this.FieldTypeOriginal = value;
            return this;
        }

        public ExtraField Build()
        {
            return new ExtraField(EntityName, Name, Description, FieldNumber, FieldType, IsExistingEntity, EntityNameOriginal, NameOriginal, DescriptionOriginal, FieldNumberOriginal, FieldTypeOriginal);
        }

        public ExtraFieldBuilder Update(ExtraField instance)
        {
            this.EntityName = instance.EntityName;
            this.Name = instance.Name;
            this.Description = instance.Description;
            this.FieldNumber = instance.FieldNumber;
            this.FieldType = instance.FieldType;
            this.IsExistingEntity = instance.IsExistingEntity;
            this.EntityNameOriginal = instance.EntityNameOriginal;
            this.NameOriginal = instance.NameOriginal;
            this.DescriptionOriginal = instance.DescriptionOriginal;
            this.FieldNumberOriginal = instance.FieldNumberOriginal;
            this.FieldTypeOriginal = instance.FieldTypeOriginal;
            return this;
        }

        public ExtraFieldBuilder()
        {
            Name = string.Empty;
            EntityName = string.Empty;
            FieldType = string.Empty;
        }

        public ExtraFieldBuilder(ExtraField instance)
        {
            EntityName = instance.EntityName;
            Name = instance.Name;
            Description = instance.Description;
            FieldNumber = instance.FieldNumber;
            FieldType = instance.FieldType;
            IsExistingEntity = instance.IsExistingEntity;
            EntityNameOriginal = instance.EntityNameOriginal;
            NameOriginal = instance.NameOriginal;
            DescriptionOriginal = instance.DescriptionOriginal;
            FieldNumberOriginal = instance.FieldNumberOriginal;
            FieldTypeOriginal = instance.FieldTypeOriginal;
        }

        public ExtraFieldBuilder(string entityName,
                                 string name,
                                 string? description,
                                 byte fieldNumber,
                                 string fieldType,
                                 bool isExistingEntity = false,
                                 string? entityNameOriginal = default,
                                 string? nameOriginal = default,
                                 string? descriptionOriginal = default,
                                 byte? fieldNumberOriginal = default,
                                 string? fieldTypeOriginal = default)
        {
            this.EntityName = entityName;
            this.Name = name;
            this.Description = description;
            this.FieldNumber = fieldNumber;
            this.FieldType = fieldType;
            this.IsExistingEntity = isExistingEntity;
            this.EntityNameOriginal = entityNameOriginal;
            this.NameOriginal = nameOriginal;
            this.DescriptionOriginal = descriptionOriginal;
            this.FieldNumberOriginal = fieldNumberOriginal;
            this.FieldTypeOriginal = fieldTypeOriginal;
        }
    }
#nullable restore
}

