﻿namespace DataFramework.ModelFramework.Tests;

public partial class IntegrationTests
{
    [Fact]
    public void Can_Generate_EntityBuilder_For_Poco()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var dataObjectInfo = CreateDataObjectInfo(EntityClassType.Poco);
        var input = dataObjectInfo.ToEntityBuilderClass(settings);

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

namespace EntityBuilders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public partial class TestEntityBuilder
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id
        {
            get
            {
                return _idDelegate.Value;
            }
            set
            {
                _idDelegate = new (() => value);
            }
        }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name
        {
            get
            {
                return _nameDelegate.Value;
            }
            set
            {
                _nameDelegate = new (() => value);
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string? Description
        {
            get
            {
                return _descriptionDelegate.Value;
            }
            set
            {
                _descriptionDelegate = new (() => value);
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public int? IdOriginal
        {
            get
            {
                return _idOriginalDelegate.Value;
            }
            set
            {
                _idOriginalDelegate = new (() => value);
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public string? NameOriginal
        {
            get
            {
                return _nameOriginalDelegate.Value;
            }
            set
            {
                _nameOriginalDelegate = new (() => value);
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public string? DescriptionOriginal
        {
            get
            {
                return _descriptionOriginalDelegate.Value;
            }
            set
            {
                _descriptionOriginalDelegate = new (() => value);
            }
        }

        public Entities.TestEntity Build()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            return new Entities.TestEntity { Id = Id, Name = Name, Description = Description, IdOriginal = IdOriginal, NameOriginal = NameOriginal, DescriptionOriginal = DescriptionOriginal };
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public TestEntityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithId(System.Func<int> idDelegate)
        {
            _idDelegate = new (idDelegate);
            return this;
        }

        public TestEntityBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestEntityBuilder WithName(System.Func<string> nameDelegate)
        {
            _nameDelegate = new (nameDelegate);
            return this;
        }

        public TestEntityBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public TestEntityBuilder WithDescription(System.Func<string?> descriptionDelegate)
        {
            _descriptionDelegate = new (descriptionDelegate);
            return this;
        }

        public TestEntityBuilder WithIdOriginal(int? idOriginal)
        {
            IdOriginal = idOriginal;
            return this;
        }

        public TestEntityBuilder WithIdOriginal(System.Func<int?> idOriginalDelegate)
        {
            _idOriginalDelegate = new (idOriginalDelegate);
            return this;
        }

        public TestEntityBuilder WithNameOriginal(string? nameOriginal)
        {
            NameOriginal = nameOriginal;
            return this;
        }

        public TestEntityBuilder WithNameOriginal(System.Func<string?> nameOriginalDelegate)
        {
            _nameOriginalDelegate = new (nameOriginalDelegate);
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(string? descriptionOriginal)
        {
            DescriptionOriginal = descriptionOriginal;
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(System.Func<string?> descriptionOriginalDelegate)
        {
            _descriptionOriginalDelegate = new (descriptionOriginalDelegate);
            return this;
        }

        public TestEntityBuilder()
        {
            #pragma warning disable CS8603 // Possible null reference return.
            _idDelegate = new (() => default);
            _nameDelegate = new (() => string.Empty);
            _descriptionDelegate = new (() => default);
            _idOriginalDelegate = new (() => default);
            _nameOriginalDelegate = new (() => default);
            _descriptionOriginalDelegate = new (() => default);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public TestEntityBuilder(Entities.TestEntity source)
        {
            _idDelegate = new (() => source.Id);
            _nameDelegate = new (() => source.Name);
            _descriptionDelegate = new (() => source.Description);
            _idOriginalDelegate = new (() => source.IdOriginal);
            _nameOriginalDelegate = new (() => source.NameOriginal);
            _descriptionOriginalDelegate = new (() => source.DescriptionOriginal);
        }

        private System.Lazy<int> _idDelegate;

        private System.Lazy<string> _nameDelegate;

        private System.Lazy<string?> _descriptionDelegate;

        private System.Lazy<int?> _idOriginalDelegate;

        private System.Lazy<string?> _nameOriginalDelegate;

        private System.Lazy<string?> _descriptionOriginalDelegate;
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_EntityBuilder_For_Record()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var dataObjectInfo = CreateDataObjectInfo(EntityClassType.Record);
        var input = dataObjectInfo.ToEntityBuilderClass(settings);

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

namespace EntityBuilders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public partial class TestEntityBuilder
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id
        {
            get
            {
                return _idDelegate.Value;
            }
            set
            {
                _idDelegate = new (() => value);
            }
        }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name
        {
            get
            {
                return _nameDelegate.Value;
            }
            set
            {
                _nameDelegate = new (() => value);
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string? Description
        {
            get
            {
                return _descriptionDelegate.Value;
            }
            set
            {
                _descriptionDelegate = new (() => value);
            }
        }

        public int? IdOriginal
        {
            get
            {
                return _idOriginalDelegate.Value;
            }
            set
            {
                _idOriginalDelegate = new (() => value);
            }
        }

        public string? NameOriginal
        {
            get
            {
                return _nameOriginalDelegate.Value;
            }
            set
            {
                _nameOriginalDelegate = new (() => value);
            }
        }

        public string? DescriptionOriginal
        {
            get
            {
                return _descriptionOriginalDelegate.Value;
            }
            set
            {
                _descriptionOriginalDelegate = new (() => value);
            }
        }

        public Entities.TestEntity Build()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            return new Entities.TestEntity(Id, Name, Description, IdOriginal, NameOriginal, DescriptionOriginal);
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public TestEntityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithId(System.Func<int> idDelegate)
        {
            _idDelegate = new (idDelegate);
            return this;
        }

        public TestEntityBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestEntityBuilder WithName(System.Func<string> nameDelegate)
        {
            _nameDelegate = new (nameDelegate);
            return this;
        }

        public TestEntityBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public TestEntityBuilder WithDescription(System.Func<string?> descriptionDelegate)
        {
            _descriptionDelegate = new (descriptionDelegate);
            return this;
        }

        public TestEntityBuilder WithIdOriginal(int? idOriginal)
        {
            IdOriginal = idOriginal;
            return this;
        }

        public TestEntityBuilder WithIdOriginal(System.Func<int?> idOriginalDelegate)
        {
            _idOriginalDelegate = new (idOriginalDelegate);
            return this;
        }

        public TestEntityBuilder WithNameOriginal(string? nameOriginal)
        {
            NameOriginal = nameOriginal;
            return this;
        }

        public TestEntityBuilder WithNameOriginal(System.Func<string?> nameOriginalDelegate)
        {
            _nameOriginalDelegate = new (nameOriginalDelegate);
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(string? descriptionOriginal)
        {
            DescriptionOriginal = descriptionOriginal;
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(System.Func<string?> descriptionOriginalDelegate)
        {
            _descriptionOriginalDelegate = new (descriptionOriginalDelegate);
            return this;
        }

        public TestEntityBuilder()
        {
            #pragma warning disable CS8603 // Possible null reference return.
            _idDelegate = new (() => default);
            _nameDelegate = new (() => string.Empty);
            _descriptionDelegate = new (() => default);
            _idOriginalDelegate = new (() => default);
            _nameOriginalDelegate = new (() => default);
            _descriptionOriginalDelegate = new (() => default);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public TestEntityBuilder(Entities.TestEntity source)
        {
            _idDelegate = new (() => source.Id);
            _nameDelegate = new (() => source.Name);
            _descriptionDelegate = new (() => source.Description);
            _idOriginalDelegate = new (() => source.IdOriginal);
            _nameOriginalDelegate = new (() => source.NameOriginal);
            _descriptionOriginalDelegate = new (() => source.DescriptionOriginal);
        }

        private System.Lazy<int> _idDelegate;

        private System.Lazy<string> _nameDelegate;

        private System.Lazy<string?> _descriptionDelegate;

        private System.Lazy<int?> _idOriginalDelegate;

        private System.Lazy<string?> _nameOriginalDelegate;

        private System.Lazy<string?> _descriptionOriginalDelegate;
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_EntityBuilder_For_ImmutableClass()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var dataObjectInfo = CreateDataObjectInfo(EntityClassType.ImmutableClass);
        var input = dataObjectInfo.ToEntityBuilderClass(settings);

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

namespace EntityBuilders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public partial class TestEntityBuilder
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id
        {
            get
            {
                return _idDelegate.Value;
            }
            set
            {
                _idDelegate = new (() => value);
            }
        }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name
        {
            get
            {
                return _nameDelegate.Value;
            }
            set
            {
                _nameDelegate = new (() => value);
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string? Description
        {
            get
            {
                return _descriptionDelegate.Value;
            }
            set
            {
                _descriptionDelegate = new (() => value);
            }
        }

        public int? IdOriginal
        {
            get
            {
                return _idOriginalDelegate.Value;
            }
            set
            {
                _idOriginalDelegate = new (() => value);
            }
        }

        public string? NameOriginal
        {
            get
            {
                return _nameOriginalDelegate.Value;
            }
            set
            {
                _nameOriginalDelegate = new (() => value);
            }
        }

        public string? DescriptionOriginal
        {
            get
            {
                return _descriptionOriginalDelegate.Value;
            }
            set
            {
                _descriptionOriginalDelegate = new (() => value);
            }
        }

        public Entities.TestEntity Build()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            return new Entities.TestEntity(Id, Name, Description, IdOriginal, NameOriginal, DescriptionOriginal);
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public TestEntityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithId(System.Func<int> idDelegate)
        {
            _idDelegate = new (idDelegate);
            return this;
        }

        public TestEntityBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestEntityBuilder WithName(System.Func<string> nameDelegate)
        {
            _nameDelegate = new (nameDelegate);
            return this;
        }

        public TestEntityBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public TestEntityBuilder WithDescription(System.Func<string?> descriptionDelegate)
        {
            _descriptionDelegate = new (descriptionDelegate);
            return this;
        }

        public TestEntityBuilder WithIdOriginal(int? idOriginal)
        {
            IdOriginal = idOriginal;
            return this;
        }

        public TestEntityBuilder WithIdOriginal(System.Func<int?> idOriginalDelegate)
        {
            _idOriginalDelegate = new (idOriginalDelegate);
            return this;
        }

        public TestEntityBuilder WithNameOriginal(string? nameOriginal)
        {
            NameOriginal = nameOriginal;
            return this;
        }

        public TestEntityBuilder WithNameOriginal(System.Func<string?> nameOriginalDelegate)
        {
            _nameOriginalDelegate = new (nameOriginalDelegate);
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(string? descriptionOriginal)
        {
            DescriptionOriginal = descriptionOriginal;
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(System.Func<string?> descriptionOriginalDelegate)
        {
            _descriptionOriginalDelegate = new (descriptionOriginalDelegate);
            return this;
        }

        public TestEntityBuilder()
        {
            #pragma warning disable CS8603 // Possible null reference return.
            _idDelegate = new (() => default);
            _nameDelegate = new (() => string.Empty);
            _descriptionDelegate = new (() => default);
            _idOriginalDelegate = new (() => default);
            _nameOriginalDelegate = new (() => default);
            _descriptionOriginalDelegate = new (() => default);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public TestEntityBuilder(Entities.TestEntity source)
        {
            _idDelegate = new (() => source.Id);
            _nameDelegate = new (() => source.Name);
            _descriptionDelegate = new (() => source.Description);
            _idOriginalDelegate = new (() => source.IdOriginal);
            _nameOriginalDelegate = new (() => source.NameOriginal);
            _descriptionOriginalDelegate = new (() => source.DescriptionOriginal);
        }

        private System.Lazy<int> _idDelegate;

        private System.Lazy<string> _nameDelegate;

        private System.Lazy<string?> _descriptionDelegate;

        private System.Lazy<int?> _idOriginalDelegate;

        private System.Lazy<string?> _nameOriginalDelegate;

        private System.Lazy<string?> _descriptionOriginalDelegate;
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_EntityBuilder_For_ObservablePoco()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var dataObjectInfo = CreateDataObjectInfo(EntityClassType.ObservablePoco);
        var input = dataObjectInfo.ToEntityBuilderClass(settings);

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

namespace EntityBuilders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public partial class TestEntityBuilder
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id
        {
            get
            {
                return _idDelegate.Value;
            }
            set
            {
                _idDelegate = new (() => value);
            }
        }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name
        {
            get
            {
                return _nameDelegate.Value;
            }
            set
            {
                _nameDelegate = new (() => value);
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string? Description
        {
            get
            {
                return _descriptionDelegate.Value;
            }
            set
            {
                _descriptionDelegate = new (() => value);
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public int? IdOriginal
        {
            get
            {
                return _idOriginalDelegate.Value;
            }
            set
            {
                _idOriginalDelegate = new (() => value);
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public string? NameOriginal
        {
            get
            {
                return _nameOriginalDelegate.Value;
            }
            set
            {
                _nameOriginalDelegate = new (() => value);
            }
        }

        [System.ComponentModel.ReadOnly(true)]
        public string? DescriptionOriginal
        {
            get
            {
                return _descriptionOriginalDelegate.Value;
            }
            set
            {
                _descriptionOriginalDelegate = new (() => value);
            }
        }

        public Entities.TestEntity Build()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            return new Entities.TestEntity { Id = Id, Name = Name, Description = Description, IdOriginal = IdOriginal, NameOriginal = NameOriginal, DescriptionOriginal = DescriptionOriginal };
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public TestEntityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithId(System.Func<int> idDelegate)
        {
            _idDelegate = new (idDelegate);
            return this;
        }

        public TestEntityBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestEntityBuilder WithName(System.Func<string> nameDelegate)
        {
            _nameDelegate = new (nameDelegate);
            return this;
        }

        public TestEntityBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public TestEntityBuilder WithDescription(System.Func<string?> descriptionDelegate)
        {
            _descriptionDelegate = new (descriptionDelegate);
            return this;
        }

        public TestEntityBuilder WithIdOriginal(int? idOriginal)
        {
            IdOriginal = idOriginal;
            return this;
        }

        public TestEntityBuilder WithIdOriginal(System.Func<int?> idOriginalDelegate)
        {
            _idOriginalDelegate = new (idOriginalDelegate);
            return this;
        }

        public TestEntityBuilder WithNameOriginal(string? nameOriginal)
        {
            NameOriginal = nameOriginal;
            return this;
        }

        public TestEntityBuilder WithNameOriginal(System.Func<string?> nameOriginalDelegate)
        {
            _nameOriginalDelegate = new (nameOriginalDelegate);
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(string? descriptionOriginal)
        {
            DescriptionOriginal = descriptionOriginal;
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(System.Func<string?> descriptionOriginalDelegate)
        {
            _descriptionOriginalDelegate = new (descriptionOriginalDelegate);
            return this;
        }

        public TestEntityBuilder()
        {
            #pragma warning disable CS8603 // Possible null reference return.
            _idDelegate = new (() => default);
            _nameDelegate = new (() => string.Empty);
            _descriptionDelegate = new (() => default);
            _idOriginalDelegate = new (() => default);
            _nameOriginalDelegate = new (() => default);
            _descriptionOriginalDelegate = new (() => default);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public TestEntityBuilder(Entities.TestEntity source)
        {
            _idDelegate = new (() => source.Id);
            _nameDelegate = new (() => source.Name);
            _descriptionDelegate = new (() => source.Description);
            _idOriginalDelegate = new (() => source.IdOriginal);
            _nameOriginalDelegate = new (() => source.NameOriginal);
            _descriptionOriginalDelegate = new (() => source.DescriptionOriginal);
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

        private System.Lazy<int> _idDelegate;

        private System.Lazy<string> _nameDelegate;

        private System.Lazy<string?> _descriptionDelegate;

        private System.Lazy<int?> _idOriginalDelegate;

        private System.Lazy<string?> _nameOriginalDelegate;

        private System.Lazy<string?> _descriptionOriginalDelegate;
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_EntityBuilder_For_Entity_With_Collection()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var dataObjectInfo = CreateDataObjectInfoWithCollectionField();
        var input = dataObjectInfo.ToEntityBuilderClass(settings);

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

namespace GeneratedNamespace
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator"", @""1.0.0.0"")]
    public partial class TestEntityBuilder
    {
        [System.ComponentModel.DataAnnotations.Required]
        public long Id
        {
            get
            {
                return _idDelegate.Value;
            }
            set
            {
                _idDelegate = new (() => value);
            }
        }

        public System.Collections.Generic.List<string> Tags
        {
            get;
            set;
        }

        public TestEntity Build()
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            return new TestEntity { Id = Id, Tags = Tags };
            #pragma warning restore CS8604 // Possible null reference argument.
        }

        public TestEntityBuilder WithId(long id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithId(System.Func<long> idDelegate)
        {
            _idDelegate = new (idDelegate);
            return this;
        }

        public TestEntityBuilder AddTags(System.Collections.Generic.IEnumerable<string> tags)
        {
            return AddTags(tags.ToArray());
        }

        public TestEntityBuilder AddTags(params string[] tags)
        {
            Tags.AddRange(tags);
            return this;
        }

        public TestEntityBuilder()
        {
            Tags = new System.Collections.Generic.List<string>();
            #pragma warning disable CS8603 // Possible null reference return.
            _idDelegate = new (() => default);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public TestEntityBuilder(TestEntity source)
        {
            Tags = new System.Collections.Generic.List<string>();
            _idDelegate = new (() => source.Id);
            Tags.AddRange(source.Tags);
        }

        private System.Lazy<long> _idDelegate;
    }
#nullable restore
}
");
    }
}
