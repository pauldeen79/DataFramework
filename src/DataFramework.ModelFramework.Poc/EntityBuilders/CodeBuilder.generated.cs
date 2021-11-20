using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PDC.Net.Core.Entities
{
#nullable enable
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator", @"1.0.0.0")]
    public partial class CodeBuilder
    {
        [StringLength(3)]
        [Required]
        public string Code
        {
            get;
            set;
        }

        [StringLength(3)]
        [Required]
        public string CodeType
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

        public bool IsExistingEntity
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? CodeOriginal
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string? CodeTypeOriginal
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

        public CodeBuilder SetCode(string value)
        {
            this.Code = value;
            return this;
        }

        public CodeBuilder SetCodeType(string value)
        {
            this.CodeType = value;
            return this;
        }

        public CodeBuilder SetDescription(string value)
        {
            this.Description = value;
            return this;
        }

        public CodeBuilder SetIsExistingEntity(bool value)
        {
            this.IsExistingEntity = value;
            return this;
        }

        public CodeBuilder SetCodeOriginal(string? value)
        {
            this.CodeOriginal = value;
            return this;
        }

        public CodeBuilder SetCodeTypeOriginal(string? value)
        {
            this.CodeTypeOriginal = value;
            return this;
        }

        public CodeBuilder SetDescriptionOriginal(string? value)
        {
            this.DescriptionOriginal = value;
            return this;
        }

        public Code Build()
        {
            return new Code(Code, CodeType, Description, IsExistingEntity, CodeOriginal, CodeTypeOriginal, DescriptionOriginal);
        }

        public CodeBuilder Update(Code instance)
        {
            this.Code = instance.CodeProperty;
            this.CodeType = instance.CodeType;
            this.Description = instance.Description;
            this.IsExistingEntity = instance.IsExistingEntity;
            this.CodeOriginal = instance.CodeOriginal;
            this.CodeTypeOriginal = instance.CodeTypeOriginal;
            this.DescriptionOriginal = instance.DescriptionOriginal;
            return this;
        }

        public CodeBuilder()
        {
            Code = string.Empty;
            CodeType = string.Empty;
        }

        public CodeBuilder(Code instance)
        {
            Code = instance.CodeProperty;
            CodeType = instance.CodeType;
            Description = instance.Description;
            IsExistingEntity = instance.IsExistingEntity;
            CodeOriginal = instance.CodeOriginal;
            CodeTypeOriginal = instance.CodeTypeOriginal;
            DescriptionOriginal = instance.DescriptionOriginal;
        }

        public CodeBuilder(string code,
                           string codeType,
                           string? description,
                           bool isExistingEntity = false,
                           string? codeOriginal = default,
                           string? codeTypeOriginal = default,
                           string? descriptionOriginal = default)
        {
            this.Code = code;
            this.CodeType = codeType;
            this.Description = description;
            this.IsExistingEntity = isExistingEntity;
            this.CodeOriginal = codeOriginal;
            this.CodeTypeOriginal = codeTypeOriginal;
            this.DescriptionOriginal = descriptionOriginal;
        }
    }
#nullable restore
}

