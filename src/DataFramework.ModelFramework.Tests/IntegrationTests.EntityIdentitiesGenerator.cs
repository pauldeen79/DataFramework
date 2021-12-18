﻿using CrossCutting.Common.Extensions;
using DataFramework.ModelFramework.Extensions;
using FluentAssertions;
using Xunit;

namespace DataFramework.ModelFramework.Tests
{
    public partial class IntegrationTests
    {
        [Fact]
        public void Can_Generate_EntityIdentity_For_Poco()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(EntityClassType.Poco).ToEntityIdentityClass(settings);

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

namespace EntityIdentities
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCode(@""DataFramework.ModelFramework.Generators.Entities.EntityIdentityGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityIdentity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public long Id
        {
            get;
            set;
        }

        public TestEntityIdentity(Entities.TestEntity instance)
        {
            Id = instance.Id;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}
");
        }

        [Fact]
        public void Can_Generate_EntityIdentity_For_Record()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(EntityClassType.Record).ToEntityIdentityClass(settings);

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

namespace EntityIdentities
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCode(@""DataFramework.ModelFramework.Generators.Entities.EntityIdentityGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial record TestEntityIdentity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public long Id
        {
            get;
        }

        public TestEntityIdentity(Entities.TestEntity instance)
        {
            Id = instance.Id;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public TestEntityIdentity(int id)
        {
            Id = id;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}
");
        }

        [Fact]
        public void Can_Generate_EntityIdentity_For_ImmutableClass()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(EntityClassType.ImmutableClass).ToEntityIdentityClass(settings);

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

namespace EntityIdentities
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCode(@""DataFramework.ModelFramework.Generators.Entities.EntityIdentityGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityIdentity : IEquatable<TestEntityIdentity>
    {
        [System.ComponentModel.DataAnnotations.Required]
        public long Id
        {
            get;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TestEntityIdentity);
        }

        public bool Equals(TestEntityIdentity other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            int hashCode = 235838129;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(TestEntityIdentity left, TestEntityIdentity right)
        {
            return EqualityComparer<TestEntityIdentity>.Default.Equals(left, right);
        }

        public static bool operator !=(TestEntityIdentity left, TestEntityIdentity right)
        {
            return !(left == right);
        }

        public TestEntityIdentity(Entities.TestEntity instance)
        {
            Id = instance.Id;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public TestEntityIdentity(int id)
        {
            Id = id;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}
");
        }

        [Fact]
        public void Can_Generate_EntityIdentity_For_ObservablePoco()
        {
            // Arrange
            var settings = GeneratorSettings.Default;
            var input = CreateDataObjectInfo(EntityClassType.ObservablePoco).ToEntityIdentityClass(settings);

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

namespace EntityIdentities
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCode(@""DataFramework.ModelFramework.Generators.Entities.EntityIdentityGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityIdentity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public long Id
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

        public TestEntityIdentity(Entities.TestEntity instance)
        {
            Id = instance.Id;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}
");
        }
    }
}
