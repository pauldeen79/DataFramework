﻿using CrossCutting.Common.Extensions;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Tests
{
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

        public Entities.TestEntity Build()
        {
            return new Entities.TestEntity { Id = Id, Name = Name, Description = Description, IdOriginal = IdOriginal, NameOriginal = NameOriginal, DescriptionOriginal = DescriptionOriginal };
        }

        public TestEntityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestEntityBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public TestEntityBuilder WithIdOriginal(int? idOriginal)
        {
            IdOriginal = idOriginal;
            return this;
        }

        public TestEntityBuilder WithNameOriginal(string? nameOriginal)
        {
            NameOriginal = nameOriginal;
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(string? descriptionOriginal)
        {
            DescriptionOriginal = descriptionOriginal;
            return this;
        }

        public TestEntityBuilder()
        {
        }

        public TestEntityBuilder(Entities.TestEntity source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IdOriginal = source.IdOriginal;
            NameOriginal = source.NameOriginal;
            DescriptionOriginal = source.DescriptionOriginal;
        }
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

        public int? IdOriginal
        {
            get;
            set;
        }

        public string? NameOriginal
        {
            get;
            set;
        }

        public string? DescriptionOriginal
        {
            get;
            set;
        }

        public Entities.TestEntity Build()
        {
            return new Entities.TestEntity(Id, Name, Description, IdOriginal, NameOriginal, DescriptionOriginal);
        }

        public TestEntityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestEntityBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public TestEntityBuilder WithIdOriginal(int? idOriginal)
        {
            IdOriginal = idOriginal;
            return this;
        }

        public TestEntityBuilder WithNameOriginal(string? nameOriginal)
        {
            NameOriginal = nameOriginal;
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(string? descriptionOriginal)
        {
            DescriptionOriginal = descriptionOriginal;
            return this;
        }

        public TestEntityBuilder()
        {
        }

        public TestEntityBuilder(Entities.TestEntity source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IdOriginal = source.IdOriginal;
            NameOriginal = source.NameOriginal;
            DescriptionOriginal = source.DescriptionOriginal;
        }
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

        public int? IdOriginal
        {
            get;
            set;
        }

        public string? NameOriginal
        {
            get;
            set;
        }

        public string? DescriptionOriginal
        {
            get;
            set;
        }

        public Entities.TestEntity Build()
        {
            return new Entities.TestEntity(Id, Name, Description, IdOriginal, NameOriginal, DescriptionOriginal);
        }

        public TestEntityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestEntityBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public TestEntityBuilder WithIdOriginal(int? idOriginal)
        {
            IdOriginal = idOriginal;
            return this;
        }

        public TestEntityBuilder WithNameOriginal(string? nameOriginal)
        {
            NameOriginal = nameOriginal;
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(string? descriptionOriginal)
        {
            DescriptionOriginal = descriptionOriginal;
            return this;
        }

        public TestEntityBuilder()
        {
        }

        public TestEntityBuilder(Entities.TestEntity source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IdOriginal = source.IdOriginal;
            NameOriginal = source.NameOriginal;
            DescriptionOriginal = source.DescriptionOriginal;
        }
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

        public Entities.TestEntity Build()
        {
            return new Entities.TestEntity { Id = Id, Name = Name, Description = Description, IdOriginal = IdOriginal, NameOriginal = NameOriginal, DescriptionOriginal = DescriptionOriginal };
        }

        public TestEntityBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public TestEntityBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestEntityBuilder WithDescription(string? description)
        {
            Description = description;
            return this;
        }

        public TestEntityBuilder WithIdOriginal(int? idOriginal)
        {
            IdOriginal = idOriginal;
            return this;
        }

        public TestEntityBuilder WithNameOriginal(string? nameOriginal)
        {
            NameOriginal = nameOriginal;
            return this;
        }

        public TestEntityBuilder WithDescriptionOriginal(string? descriptionOriginal)
        {
            DescriptionOriginal = descriptionOriginal;
            return this;
        }

        public TestEntityBuilder()
        {
        }

        public TestEntityBuilder(Entities.TestEntity source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            IdOriginal = source.IdOriginal;
            NameOriginal = source.NameOriginal;
            DescriptionOriginal = source.DescriptionOriginal;
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
    }
}
