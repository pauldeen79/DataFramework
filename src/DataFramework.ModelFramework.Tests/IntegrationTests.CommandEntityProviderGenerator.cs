﻿namespace DataFramework.ModelFramework.Tests;

public partial class IntegrationTests
{
    [Fact]
    public void Can_Generate_CommandEntityProvider_For_Record()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(EntityClassType.Record).ToCommandEntityProviderClass(settings);

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

namespace CommandEntityProviders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.EntityCommandProviderGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityCommandEntityProvider : CrossCutting.Data.Abstractions.IDatabaseCommandEntityProvider<Entities.TestEntity,EntityBuilders.TestEntityBuilder>
    {
        public Func<EntityBuilders.TestEntityBuilder, CrossCutting.Data.Abstractions.DatabaseOperation, EntityBuilders.TestEntityBuilder>? CreateResultEntity
        {
            get
            {
                return (entity, operation) =>
                {
                    switch (operation)
                    {
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Insert:
                            return AddResultEntity(entity);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Update:
                            return UpdateResultEntity(entity);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Delete:
                            return DeleteResultEntity(entity);
                         default:
                             throw new ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                }
            }
        }

        public Func<EntityBuilders.TestEntityBuilder, CrossCutting.Data.Abstractions.DatabaseOperation, System.Data.IDataReader, EntityBuilders.TestEntityBuilder>? AfterRead
        {
            get
            {
                return (entity, operation, reader) =>
                {
                    switch (operation)
                    {
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Insert:
                            return AddAfterRead(entity);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Update:
                            return UpdateAfterRead(entity);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Delete:
                            return DeleteAfterRead(entity);
                         default:
                             throw new ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                }
            }
        }

        public Func<EntityBuilders.TestEntityBuilder, Entities.TestEntity>? CreateBuilder
        {
            get
            {
                return entity => new EntityBuilders.TestEntityBuilder(entity);
            }
        }

        public Func<EntityBuilders.TestEntityBuilder, Entities.TestEntity>? CreateEntity
        {
            get
            {
                return builder => builder.Build();
            }
        }

        public EntityBuilders.TestEntityBuilder AddResultEntity(EntityBuilders.TestEntityBuilder resultEntity)
        {
            // additional code goes here
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder AddAfterRead(EntityBuilders.TestEntityBuilder resultEntity, System.Data.IDataReader reader)
        {
            resultEntity = resultEntity.SetId(reader.GetInt32(""Id""));
            resultEntity = resultEntity.SetName(reader.GetString(""Name""));
            resultEntity = resultEntity.SetDescription(reader.GetNullableString(""Description""));
            resultEntity = resultEntity.SetIdOriginal(reader.GetInt32(""Id""));
            resultEntity = resultEntity.SetNameOriginal(reader.GetString(""Name""));
            resultEntity = resultEntity.SetDescriptionOriginal(reader.GetNullableString(""Description""));
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder UpdateResultEntity(EntityBuilders.TestEntityBuilder resultEntity)
        {
            // additional code goes here
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder UpdateAfterRead(EntityBuilders.TestEntityBuilder resultEntity, System.Data.IDataReader reader)
        {
            resultEntity = resultEntity.SetId(reader.GetInt32(""Id""));
            resultEntity = resultEntity.SetName(reader.GetString(""Name""));
            resultEntity = resultEntity.SetDescription(reader.GetNullableString(""Description""));
            resultEntity = resultEntity.SetIdOriginal(reader.GetInt32(""Id""));
            resultEntity = resultEntity.SetNameOriginal(reader.GetString(""Name""));
            resultEntity = resultEntity.SetDescriptionOriginal(reader.GetNullableString(""Description""));
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder DeleteResultEntity(EntityBuilders.TestEntityBuilder resultEntity)
        {
            // additional code goes here
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder DeleteAfterRead(EntityBuilders.TestEntityBuilder resultEntity, System.Data.IDataReader reader)
        {
            resultEntity = resultEntity.SetId(reader.GetInt32(""Id""));
            resultEntity = resultEntity.SetName(reader.GetString(""Name""));
            resultEntity = resultEntity.SetDescription(reader.GetNullableString(""Description""));
            resultEntity = resultEntity.SetIdOriginal(reader.GetInt32(""Id""));
            resultEntity = resultEntity.SetNameOriginal(reader.GetString(""Name""));
            resultEntity = resultEntity.SetDescriptionOriginal(reader.GetNullableString(""Description""));
            return resultEntity;
        }
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_CommandEntityProvider_For_Poco()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfo(EntityClassType.Poco).ToCommandEntityProviderClass(settings);

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

namespace CommandEntityProviders
{
#nullable enable
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.EntityCommandProviderGenerator"", @""1.0.0.0"")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal partial class TestEntityCommandEntityProvider : CrossCutting.Data.Abstractions.IDatabaseCommandEntityProvider<Entities.TestEntity,EntityBuilders.TestEntityBuilder>
    {
        public Func<EntityBuilders.TestEntityBuilder, CrossCutting.Data.Abstractions.DatabaseOperation, EntityBuilders.TestEntityBuilder>? CreateResultEntity
        {
            get
            {
                return (entity, operation) =>
                {
                    switch (operation)
                    {
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Insert:
                            return AddResultEntity(entity);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Update:
                            return UpdateResultEntity(entity);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Delete:
                            return DeleteResultEntity(entity);
                         default:
                             throw new ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                }
            }
        }

        public Func<EntityBuilders.TestEntityBuilder, CrossCutting.Data.Abstractions.DatabaseOperation, System.Data.IDataReader, EntityBuilders.TestEntityBuilder>? AfterRead
        {
            get
            {
                return (entity, operation, reader) =>
                {
                    switch (operation)
                    {
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Insert:
                            return AddAfterRead(entity);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Update:
                            return UpdateAfterRead(entity);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Delete:
                            return DeleteAfterRead(entity);
                         default:
                             throw new ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                }
            }
        }

        public Func<EntityBuilders.TestEntityBuilder, Entities.TestEntity>? CreateBuilder
        {
            get
            {
                return entity => new EntityBuilders.TestEntityBuilder(entity);
            }
        }

        public Func<EntityBuilders.TestEntityBuilder, Entities.TestEntity>? CreateEntity
        {
            get
            {
                return builder => builder.Build();
            }
        }

        public EntityBuilders.TestEntityBuilder AddResultEntity(EntityBuilders.TestEntityBuilder resultEntity)
        {
            // additional code goes here
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder AddAfterRead(EntityBuilders.TestEntityBuilder resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.Id = reader.GetInt32(""Id"");
            resultEntity.Name = reader.GetString(""Name"");
            resultEntity.Description = reader.GetNullableString(""Description"");
            resultEntity.IdOriginal = reader.GetInt32(""Id"");
            resultEntity.NameOriginal = reader.GetString(""Name"");
            resultEntity.DescriptionOriginal = reader.GetNullableString(""Description"");
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder UpdateResultEntity(EntityBuilders.TestEntityBuilder resultEntity)
        {
            // additional code goes here
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder UpdateAfterRead(EntityBuilders.TestEntityBuilder resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.Id = reader.GetInt32(""Id"");
            resultEntity.Name = reader.GetString(""Name"");
            resultEntity.Description = reader.GetNullableString(""Description"");
            resultEntity.IdOriginal = reader.GetInt32(""Id"");
            resultEntity.NameOriginal = reader.GetString(""Name"");
            resultEntity.DescriptionOriginal = reader.GetNullableString(""Description"");
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder DeleteResultEntity(EntityBuilders.TestEntityBuilder resultEntity)
        {
            // additional code goes here
            return resultEntity;
        }

        public EntityBuilders.TestEntityBuilder DeleteAfterRead(EntityBuilders.TestEntityBuilder resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.Id = reader.GetInt32(""Id"");
            resultEntity.Name = reader.GetString(""Name"");
            resultEntity.Description = reader.GetNullableString(""Description"");
            resultEntity.IdOriginal = reader.GetInt32(""Id"");
            resultEntity.NameOriginal = reader.GetString(""Name"");
            resultEntity.DescriptionOriginal = reader.GetNullableString(""Description"");
            return resultEntity;
        }
    }
#nullable restore
}
");
    }

    [Fact]
    public void Can_Generate_CommandEntityProvider_With_Prevention_For_Update_And_Delete()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = CreateDataObjectInfoInsertOnly(EntityClassType.Poco).ToCommandEntityProviderClass(settings);

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
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.ModelFramework.Generators.EntityCommandProviderGenerator"", @""1.0.0.0"")]
    public partial class TestEntityCommandEntityProvider : CrossCutting.Data.Abstractions.IDatabaseCommandEntityProvider<TestEntity,TestEntityBuilder>
    {
        public Func<TestEntityBuilder, CrossCutting.Data.Abstractions.DatabaseOperation, TestEntityBuilder>? CreateResultEntity
        {
            get
            {
                return (entity, operation) =>
                {
                    switch (operation)
                    {
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Insert:
                            return AddResultEntity(entity);
                         default:
                             throw new ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                }
            }
        }

        public Func<TestEntityBuilder, CrossCutting.Data.Abstractions.DatabaseOperation, System.Data.IDataReader, TestEntityBuilder>? AfterRead
        {
            get
            {
                return (entity, operation, reader) =>
                {
                    switch (operation)
                    {
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Insert:
                            return AddAfterRead(entity);
                         default:
                             throw new ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                }
            }
        }

        public Func<TestEntityBuilder, TestEntity>? CreateBuilder
        {
            get
            {
                return entity => new TestEntityBuilder(entity);
            }
        }

        public Func<TestEntityBuilder, TestEntity>? CreateEntity
        {
            get
            {
                return builder => builder.Build();
            }
        }

        public TestEntityBuilder AddResultEntity(TestEntityBuilder resultEntity)
        {
            return resultEntity;
        }

        public TestEntityBuilder AddAfterRead(TestEntityBuilder resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.Id = reader.GetInt32(""Id"");
            resultEntity.Name = reader.GetString(""Name"");
            resultEntity.Description = reader.GetNullableString(""Description"");
            resultEntity.IdOriginal = reader.GetInt32(""Id"");
            resultEntity.NameOriginal = reader.GetString(""Name"");
            resultEntity.DescriptionOriginal = reader.GetNullableString(""Description"");
            return resultEntity;
        }
    }
#nullable restore
}
");
    }
}
