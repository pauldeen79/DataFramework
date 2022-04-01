﻿namespace DataFramework.ModelFramework.Tests;

public partial class IntegrationTests
{
    [Fact]
    public void Can_Generate_Entity_For_Poco()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(EntityClassType.Poco).ToEntityClass(settings);

        // Act
        var actual = GenerateCode(input, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityGenerator"", @""1.0.0.0"")]
    [System.ComponentModel.Description(@""Description goes here"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntity : ITestEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string? Description
        {
            get;
            set;
        }

        public bool IsExistingEntity
        {
            get
            {
                return Id > 0;
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public int? IdOriginal
        {
            get;
            set;
        }

        [System.ComponentModel.ReadOnly(true)]
        public string? NameOriginal
        {
            get;
            set;
        }

        [System.ComponentModel.ReadOnly(true)]
        public string? DescriptionOriginal
        {
            get;
            set;
        }

        [System.ComponentModel.ReadOnly(true)]
        public bool? IsExistingEntityOriginal
        {
            get;
            set;
        }
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_Entity_For_Record()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(EntityClassType.Record).ToEntityClass(settings);

        // Act
        var actual = GenerateCode(input, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityGenerator"", @""1.0.0.0"")]
    [System.ComponentModel.Description(@""Description goes here"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial record TestEntity : ITestEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string? Description
        {
            get;
        }

        public bool IsExistingEntity
        {
            get
            {
                return Id > 0;
            }
        }

        public int? IdOriginal
        {
            get;
        }

        public string? NameOriginal
        {
            get;
        }

        public string? DescriptionOriginal
        {
            get;
        }

        public bool? IsExistingEntityOriginal
        {
            get;
        }

        public TestEntity(int id, string name, string? description, int? idOriginal = default, string? nameOriginal = default, string? descriptionOriginal = default, bool? isExistingEntityOriginal = default)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.IdOriginal = idOriginal;
            this.NameOriginal = nameOriginal;
            this.DescriptionOriginal = descriptionOriginal;
            this.IsExistingEntityOriginal = isExistingEntityOriginal;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_Entity_For_ImmutableClass()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(EntityClassType.ImmutableClass).ToEntityClass(settings);

        // Act
        var actual = GenerateCode(input, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityGenerator"", @""1.0.0.0"")]
    [System.ComponentModel.Description(@""Description goes here"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntity : ITestEntity, IEquatable<TestEntity>
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string? Description
        {
            get;
        }

        public bool IsExistingEntity
        {
            get
            {
                return Id > 0;
            }
        }

        public int? IdOriginal
        {
            get;
        }

        public string? NameOriginal
        {
            get;
        }

        public string? DescriptionOriginal
        {
            get;
        }

        public bool? IsExistingEntityOriginal
        {
            get;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TestEntity);
        }

        public bool Equals(TestEntity other)
        {
            return other != null &&
                   Id == other.Id &&
                   Name == other.Name &&
                   Description == other.Description &&
                   IsExistingEntity == other.IsExistingEntity;
        }

        public override int GetHashCode()
        {
            int hashCode = 235838129;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<System.String>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<System.String>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + IsExistingEntity.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(TestEntity left, TestEntity right)
        {
            return EqualityComparer<TestEntity>.Default.Equals(left, right);
        }

        public static bool operator !=(TestEntity left, TestEntity right)
        {
            return !(left == right);
        }

        public TestEntity(int id, string name, string? description, int? idOriginal = default, string? nameOriginal = default, string? descriptionOriginal = default, bool? isExistingEntityOriginal = default)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.IdOriginal = idOriginal;
            this.NameOriginal = nameOriginal;
            this.DescriptionOriginal = descriptionOriginal;
            this.IsExistingEntityOriginal = isExistingEntityOriginal;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_Entity_For_ObservablePoco()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(EntityClassType.ObservablePoco).ToEntityClass(settings);

        // Act
        var actual = GenerateCode(input, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityGenerator"", @""1.0.0.0"")]
    [System.ComponentModel.Description(@""Description goes here"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntity : ITestEntity, System.ComponentModel.INotifyPropertyChanged
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""Id""));
            }
        }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""Name""));
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string? Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""Description""));
            }
        }

        public bool IsExistingEntity
        {
            get
            {
                return Id > 0;
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public int? IdOriginal
        {
            get
            {
                return _idOriginal;
            }
            set
            {
                _idOriginal = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""Id""));
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public string? NameOriginal
        {
            get
            {
                return _nameOriginal;
            }
            set
            {
                _nameOriginal = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""Name""));
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public string? DescriptionOriginal
        {
            get
            {
                return _descriptionOriginal;
            }
            set
            {
                _descriptionOriginal = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""Description""));
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public bool? IsExistingEntityOriginal
        {
            get
            {
                return _isExistingEntityOriginal;
            }
            set
            {
                _isExistingEntityOriginal = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""IsExistingEntity""));
            }
        }

        private int _id;

        private string _name;

        private string? _description;

        private bool _isExistingEntity;

        private int? _idOriginal;

        private string? _nameOriginal;

        private string? _descriptionOriginal;

        private bool? _isExistingEntityOriginal;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_Entity_With_Custom_DisplayName_For_Property_With_Same_Name_As_Instance()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var sut = new DataObjectInfoBuilder()
            .WithName("Test")
            .AddFields(new FieldInfoBuilder().WithName("Test").WithDisplayName("CustomDisplayName"))
            .Build()
            .ToEntityClass(settings);

        // Act
        var actual = GenerateCode(sut, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratedNamespace
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityGenerator"", @""1.0.0.0"")]
    public partial class Test
    {
        [System.ComponentModel.DisplayName(@""CustomDisplayName"")]
        public object TestProperty
        {
            get;
            set;
        }
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_Entity_With_Default_DisplayName_For_Property_With_Same_Name_As_Instance()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        string? val = null;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        var sut = new DataObjectInfoBuilder()
            .WithName("Test")
            .AddFields(new FieldInfoBuilder().WithName("Test").WithDisplayName(val))
            .Build()
            .ToEntityClass(settings);

        // Act
        var actual = GenerateCode(sut, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratedNamespace
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityGenerator"", @""1.0.0.0"")]
    public partial class Test
    {
        [System.ComponentModel.DataAnnotations.DisplayName(@""Test"")]
        public object TestProperty
        {
            get;
            set;
        }
    }
#nullable restore
}
");
    }
}
