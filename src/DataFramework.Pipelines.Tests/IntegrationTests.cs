namespace DataFramework.Pipelines.Tests;

public sealed class IntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task Can_Create_Code_For_Immutable_Class_Entity_With_ConcurrencyChecks_Using_ClassFramework_EntityPipeline()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.ImmutableClass) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var classFrameworkPipelineService = GetClassFrameworkPipelineService();
        var classPipeline = GetClassPipeline();

        // Act
        var result = (await classPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var entity = result.GetValueOrThrow();
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor(true)
            .WithAddPublicParameterlessConstructor(false)
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext);
        var code = await GenerateCode(new TestCodeGenerationProvider(result.GetValueOrThrow()));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.ClassGenerator"", @""1.0.0.0"")]
    public partial class MyEntity
    {
        public int MyField
        {
            get;
        }

        public System.Nullable<int> MyFieldOriginal
        {
            get;
        }

        public MyEntity(int myField, int? myFieldOriginal)
        {
            this.MyField = myField;
            this.MyFieldOriginal = myFieldOriginal;
        }

        public MyNamespace.Builders.MyEntityBuilder ToBuilder()
        {
            return new MyNamespace.Builders.MyEntityBuilder(this);
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_Poco_Class_Entity_With_ConcurrencyChecks_Using_ClassFramework_EntityPipeline()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var classFrameworkPipelineService = GetClassFrameworkPipelineService();
        var classPipeline = GetClassPipeline();

        // Act
        var result = (await classPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var entity = result.GetValueOrThrow();
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor(false)
            .WithAddPublicParameterlessConstructor(true) // note that you might want to omit this in case you don't have custom default values
            .WithAddSetters()
            .WithToBuilderFormatString(string.Empty) // no builder necessary
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext);
        var code = await GenerateCode(new TestCodeGenerationProvider(result.GetValueOrThrow()));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.ClassGenerator"", @""1.0.0.0"")]
    public partial class MyEntity
    {
        public int MyField
        {
            get;
            set;
        }

        [System.ComponentModel.ReadOnly(true)]
        public System.Nullable<int> MyFieldOriginal
        {
            get;
            set;
        }

        public MyEntity()
        {
            MyField = default(System.Int32);
            MyFieldOriginal = default(System.Int32?);
        }
    }
}
#nullable disable
");
    }


    [Fact]
    public async Task Can_Create_Code_For_Poco_Class_Entity_Without_ConcurrencyChecks_Using_ClassFramework_EntityPipeline()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.NoFields) //default
            .Build();
        var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var classFrameworkPipelineService = GetClassFrameworkPipelineService();
        var classPipeline = GetClassPipeline();

        // Act
        var result = (await classPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var entity = result.GetValueOrThrow();
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor(false)
            .WithAddPublicParameterlessConstructor(true) // note that you might want to omit this in case you don't have custom default values
            .WithAddSetters()
            .WithToBuilderFormatString(string.Empty) // no builder necessary
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext);
        var code = await GenerateCode(new TestCodeGenerationProvider(result.GetValueOrThrow()));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.ClassGenerator"", @""1.0.0.0"")]
    public partial class MyEntity
    {
        public int MyField
        {
            get;
            set;
        }

        public MyEntity()
        {
            MyField = default(System.Int32);
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_Immutable_IdentityClass_Entity_With_ConcurrencyChecks_Using_ClassFramework_EntityPipeline()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(
                new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)), // gets filtered out because of not being an identity field
                new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField())
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.ImmutableClass) //default
            .WithDefaultIdentityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new IdentityClassContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var classFrameworkPipelineService = GetClassFrameworkPipelineService();
        var identityClassPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<IdentityClassContext>>();

        // Act
        var result = (await identityClassPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var entity = result.GetValueOrThrow();
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor(true)
            .WithAddPublicParameterlessConstructor(false)
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext);
        var code = await GenerateCode(new TestCodeGenerationProvider(result.GetValueOrThrow()));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.IdentityClassGenerator"", @""1.0.0.0"")]
    public partial class MyEntityIdentity
    {
        public int Id
        {
            get;
        }

        public MyEntityIdentity(int id)
        {
            this.Id = id;
        }

        public MyNamespace.Builders.MyEntityIdentityBuilder ToBuilder()
        {
            return new MyNamespace.Builders.MyEntityIdentityBuilder(this);
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_Builder_For_Immutable_Class_Entity_With_ConcurrencyChecks()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.ImmutableClass)
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var classFrameworkPipelineService = GetClassFrameworkPipelineService();
        var classPipeline = GetClassPipeline();

        // Act
        var result = (await classPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var cls = result.GetValueOrThrow();
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor()
            .WithAddSetters(false)
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(cls, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext);
        var entity = result.GetValueOrThrow();
        var builderContext = new ClassFramework.Pipelines.Builder.BuilderContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(builderContext);
        var code = await GenerateCode(new TestCodeGenerationProvider(result.GetValueOrThrow()));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace.Builders
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.ClassGenerator"", @""1.0.0.0"")]
    public partial class MyEntityBuilder
    {
        public int MyField
        {
            get;
            set;
        }

        public System.Nullable<int> MyFieldOriginal
        {
            get;
            set;
        }

        public MyEntityBuilder(MyNamespace.MyEntity source)
        {
            MyField = source.MyField;
            MyFieldOriginal = source.MyFieldOriginal;
        }

        public MyEntityBuilder()
        {
            SetDefaultValues();
        }

        public MyNamespace.MyEntity Build()
        {
            return new MyNamespace.MyEntity(MyField, MyFieldOriginal);
        }

        partial void SetDefaultValues();

        public MyNamespace.Builders.MyEntityBuilder WithMyField(int myField)
        {
            MyField = myField;
            return this;
        }

        public MyNamespace.Builders.MyEntityBuilder WithMyFieldOriginal(System.Nullable<int> myFieldOriginal)
        {
            MyFieldOriginal = myFieldOriginal;
            return this;
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_CommandEntityProvider_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when CommandEntityProviderNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new CommandEntityProviderContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var commandEntityProviderPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<CommandEntityProviderContext>>();

        // Act
        var result = (await commandEntityProviderPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var commandEntityProvider = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(commandEntityProvider));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.CommandEntityProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityCommandEntityProvider : CrossCutting.Data.Abstractions.IDatabaseCommandEntityProvider<MyNamespace.MyEntity>
    {
        public CrossCutting.Data.Abstractions.CreateResultEntityHandler<MyNamespace.MyEntity>? CreateResultEntity
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
                             throw new System.ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                };
            }
        }

        public CrossCutting.Data.Abstractions.AfterReadHandler<MyNamespace.MyEntity>? AfterRead
        {
            get
            {
                return (entity, operation, reader) =>
                {
                    switch (operation)
                    {
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Insert:
                            return AddAfterRead(entity, reader);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Update:
                            return UpdateAfterRead(entity, reader);
                        case CrossCutting.Data.Abstractions.DatabaseOperation.Delete:
                            return DeleteAfterRead(entity, reader);
                         default:
                             throw new System.ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                };
            }
        }

        public CrossCutting.Data.Abstractions.CreateBuilderHandler<MyNamespace.MyEntity, MyNamespace.MyEntity>? CreateBuilder
        {
            get
            {
                return entity => entity;
            }
        }

        public CrossCutting.Data.Abstractions.CreateEntityHandler<MyNamespace.MyEntity, MyNamespace.MyEntity>? CreateEntity
        {
            get
            {
                return builder => builder;
            }
        }

        private MyNamespace.MyEntity AddResultEntity(MyNamespace.MyEntity resultEntity)
        {
            return resultEntity;
        }

        private MyNamespace.MyEntity AddAfterRead(MyNamespace.MyEntity resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.MyField = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetInt32(reader, @""MyField"");
            resultEntity.MyFieldOriginal = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetInt32(reader, @""MyField"");
            return resultEntity;
        }

        private MyNamespace.MyEntity UpdateResultEntity(MyNamespace.MyEntity resultEntity)
        {
            return resultEntity;
        }

        private MyNamespace.MyEntity UpdateAfterRead(MyNamespace.MyEntity resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.MyField = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetInt32(reader, @""MyField"");
            resultEntity.MyFieldOriginal = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetInt32(reader, @""MyField"");
            return resultEntity;
        }

        private MyNamespace.MyEntity DeleteResultEntity(MyNamespace.MyEntity resultEntity)
        {
            return resultEntity;
        }

        private MyNamespace.MyEntity DeleteAfterRead(MyNamespace.MyEntity resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.MyField = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetInt32(reader, @""MyField"");
            resultEntity.MyFieldOriginal = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetInt32(reader, @""MyField"");
            return resultEntity;
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_CommandProvider_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when CommandProviderNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .WithEnableNullableContext()
            .Build();
        var context = new CommandProviderContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var commandProviderPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<CommandProviderContext>>();

        // Act
        var result = (await commandProviderPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var commandProvider = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(commandProvider));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.CommandProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityCommandProvider : CrossCutting.Data.Abstractions.IDatabaseCommandProvider<MyNamespace.MyEntity>
    {
        public CrossCutting.Data.Abstractions.IDatabaseCommand Create(MyNamespace.MyEntity source, CrossCutting.Data.Abstractions.DatabaseOperation operation)
        {
            switch (operation)
            {
                case CrossCutting.Data.Abstractions.DatabaseOperation.Insert:
                    return new CrossCutting.Data.Core.Commands.TextCommand<MyNamespace.MyEntity>(""INSERT INTO [MyEntity]([MyField]) OUTPUT INSERTED.[MyField] VALUES(@MyField)"", source, CrossCutting.Data.Abstractions.DatabaseOperation.Insert, AddParameters);
                case CrossCutting.Data.Abstractions.DatabaseOperation.Update:
                    return new CrossCutting.Data.Core.Commands.TextCommand<MyNamespace.MyEntity>(""UPDATE [MyEntity] SET [MyField] = @MyField OUTPUT INSERTED.[MyField] WHERE [MyField] = @MyFieldOriginal"", source, CrossCutting.Data.Abstractions.DatabaseOperation.Update, UpdateParameters);
                case CrossCutting.Data.Abstractions.DatabaseOperation.Delete:
                    return new CrossCutting.Data.Core.Commands.TextCommand<MyNamespace.MyEntity>(""DELETE FROM [MyEntity] OUTPUT DELETED.[MyField] WHERE [MyField] = @MyFieldOriginal"", source, CrossCutting.Data.Abstractions.DatabaseOperation.Delete, DeleteParameters);
                default:
                    throw new System.ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
            }
        }

        public object AddParameters(MyNamespace.MyEntity resultEntity)
        {
            return new[]
            {
                new System.Collections.Generic.KeyValuePair<System.String, System.Object?>(""@MyField"", resultEntity.MyField),
            };
        }

        public object UpdateParameters(MyNamespace.MyEntity resultEntity)
        {
            return new[]
            {
                new System.Collections.Generic.KeyValuePair<System.String, System.Object?>(""@MyField"", resultEntity.MyField),
                new System.Collections.Generic.KeyValuePair<System.String, System.Object?>(""@MyFieldOriginal"", resultEntity.MyFieldOriginal),
            };
        }

        public object DeleteParameters(MyNamespace.MyEntity resultEntity)
        {
            return new[]
            {
                new System.Collections.Generic.KeyValuePair<System.String, System.Object?>(""@MyFieldOriginal"", resultEntity.MyFieldOriginal),
            };
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_DatabaseEntityRetrieverProvider_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when DatabaseEntityRetrieverProviderNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new DatabaseEntityRetrieverProviderContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var databaseEntityRetrieverProviderPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<DatabaseEntityRetrieverProviderContext>>();

        // Act
        var result = (await databaseEntityRetrieverProviderPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var commandProvider = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(commandProvider));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.DatabaseEntityRetrieverProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityDatabaseEntityRetrieverProvider : QueryFramework.SqlServer.Abstractions.IDatabaseEntityRetrieverProvider
    {
        private readonly CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<MyNamespace.MyEntity> _databaseEntityRetriever;

        public MyEntityDatabaseEntityRetrieverProvider(CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<MyNamespace.MyEntity> databaseEntityRetriever)
        {
            _databaseEntityRetriever = databaseEntityRetriever;
        }

        public bool TryCreate<TResult>(QueryFramework.Abstractions.IQuery query, out CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<TResult>? result)
            where TResult : class
        {
            if (typeof(TResult) == typeof(MyNamespace.MyEntity))
            {
                result = (CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<TResult>)_databaseEntityRetriever;
                return true;
            }
            result = default;
            return false;
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_DatabaseSchema()
    {
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when DatabaseSchemaNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields
            (
                new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsDatabaseIdentityField(),
                new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)),
                new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string))
            ).Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new DatabaseSchemaContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var databaseSchemaPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<DatabaseSchemaContext>>();

        // Act
        var result = (await databaseSchemaPipeline.Process(context)).ProcessResult(context.Builders, () => context.Builders.Select(x => x.Build()));
        var databaseObjects = result.GetValueOrThrow();
        var code =  await GenerateCode(new TestDatabaseSchemaGenerationProvider(databaseObjects));

        // Assert
        code.Should().Be(@"SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyEntity](
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[MyField1] INT NOT NULL,
	[MyField2] VARCHAR(32) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
");
    }

    [Fact]
    public async Task Can_Create_Code_For_DatabaseSchema_With_StoredProcedures()
    {
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when DatabaseSchemaNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields
            (
                new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsDatabaseIdentityField(),
                new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)),
                new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string))
            ).Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .WithUseStoredProcedures()
            .Build();
        var context = new DatabaseSchemaContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var databaseSchemaPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<DatabaseSchemaContext>>();

        // Act
        var result = (await databaseSchemaPipeline.Process(context)).ProcessResult(context.Builders, () => context.Builders.Select(x => x.Build()));
        var databaseObjects = result.GetValueOrThrow();
        var code = await GenerateCode(new TestDatabaseSchemaGenerationProvider(databaseObjects));

        // Assert
        code.Should().Be(@"SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyEntity](
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[MyField1] INT NOT NULL,
	[MyField2] VARCHAR(32) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteMyEntity]
	@MyField1 INT,
	@MyField2 VARCHAR(32)
AS
BEGIN
    DELETE FROM [MyEntity] OUTPUT DELETED.[Id], DELETED.[MyField1], DELETED.[MyField2] WHERE [Id] = @IdOriginal AND [MyField1] = @MyField1Original AND [MyField2] = @MyField2Original
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertMyEntity]
	@MyField1 INT,
	@MyField2 VARCHAR(32)
AS
BEGIN
    INSERT INTO [MyEntity]([MyField1], [MyField2]) OUTPUT INSERTED.[Id], INSERTED.[MyField1], INSERTED.[MyField2] VALUES(@MyField1, @MyField2)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateMyEntity]
	@MyField1 INT,
	@MyField2 VARCHAR(32)
AS
BEGIN
    UPDATE [MyEntity] SET [MyField1] = @MyField1, [MyField2] = @MyField2 OUTPUT INSERTED.[Id], INSERTED.[MyField1], INSERTED.[MyField2] WHERE [Id] = @IdOriginal AND [MyField1] = @MyField1Original AND [MyField2] = @MyField2Original
END
GO
");
    }

    [Fact]
    public async Task Can_Create_Code_For_DatabaseSchema_With_Default_Values()
    {
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when DatabaseSchemaNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields
            (
                new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField().WithIsDatabaseIdentityField(),
                new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)),
                new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)).WithDefaultValue("default value")
            ).Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new DatabaseSchemaContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var databaseSchemaPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<DatabaseSchemaContext>>();

        // Act
        var result = (await databaseSchemaPipeline.Process(context)).ProcessResult(context.Builders, () => context.Builders.Select(x => x.Build()));
        var databaseObjects = result.GetValueOrThrow();
        var code = await GenerateCode(new TestDatabaseSchemaGenerationProvider(databaseObjects));

        // Assert
        code.Should().Be(@"SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MyEntity](
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[MyField1] INT NOT NULL,
	[MyField2] VARCHAR(32) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [MyEntity] ADD CONSTRAINT [DF_MyField2] DEFAULT ('default value') FOR [MyField2]
GO
");
    }

    [Fact]
    public async Task Can_Create_Code_For_DependencyInjection_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when DependencyInjectionNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new DependencyInjectionContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var dependencyInjectionPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<DependencyInjectionContext>>();

        // Act
        var result = (await dependencyInjectionPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var depdendencyInjection = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(depdendencyInjection));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.DependencyInjectionGenerator"", @""1.0.0.0"")]
    public static partial class ServiceCollectionExtensions
    {
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddMyEntityDependencies(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
        {
            return QueryFramework.SqlServer.Extensions.ServiceCollectionExtensions.AddQueryFrameworkSqlServer(serviceCollection, x =>
            {
                x.AddSingleton<CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<MyNamespace.MyEntity>, CrossCutting.Data.Sql.DatabaseEntityRetriever<MyNamespace.MyEntity>>();
                x.AddScoped<CrossCutting.Data.Abstractions.IDatabaseCommandProcessor<MyNamespace.MyEntity>, CrossCutting.Data.Sql.DatabaseCommandProcessor<MyNamespace.MyEntity>>();
                x.AddScoped<CrossCutting.Data.Abstractions.IDatabaseCommandEntityProvider<MyNamespace.MyEntity>, MyNamespace.MyEntityCommandEntityProvider>();
                x.AddSingleton<CrossCutting.Data.Abstractions.IDatabaseCommandProvider<MyNamespace.MyEntity>, MyNamespace.MyEntityCommandProvider>();
                x.AddSingleton<CrossCutting.Data.Abstractions.IDatabaseCommandProvider<MyNamespace.MyEntityIdentity>, MyNamespace.MyEntityIdentityCommandProvider>();
                x.AddSingleton<QueryFramework.SqlServer.Abstractions.IQueryFieldInfoProvider, MyNamespace.MyEntityQueryFieldInfoProvider>();
                x.AddSingleton<QueryFramework.SqlServer.Abstractions.IDatabaseEntityRetrieverProvider, MyNamespace.MyEntityDatabaseEntityRetrieverProvider>();
                x.AddSingleton<CrossCutting.Data.Abstractions.IDatabaseEntityRetrieverSettingsProvider, MyNamespace.MyEntityDatabaseEntityRetrieverSettingsProvider>();
                x.AddSingleton<CrossCutting.Data.Abstractions.IPagedDatabaseEntityRetrieverSettingsProvider, MyNamespace.MyEntityDatabaseEntityRetrieverSettingsProvider>();
                x.AddSingleton<CrossCutting.Data.Abstractions.IDatabaseEntityMapper<MyNamespace.MyEntity>, MyNamespace.MyEntityEntityMapper>();
                x.AddScoped<CrossCutting.Data.Abstractions.IRepository<MyNamespace.MyEntity>, MyNamespace.MyEntityRepository>();
            });
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_EntityMapper_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when EntityMapperNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new EntityMapperContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var entityMapperPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<EntityMapperContext>>();

        // Act
        var result = (await entityMapperPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var commandEntityProvider = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(commandEntityProvider));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.EntityMapperGenerator"", @""1.0.0.0"")]
    public partial class MyEntityEntityMapper : CrossCutting.Data.Abstractions.IDatabaseEntityMapper<MyNamespace.MyEntity>
    {
        public MyNamespace.MyEntity Map(System.Data.IDataReader reader)
        {
            return new MyNamespace.MyEntity
            {
                MyField1 = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetInt32(reader, ""MyField1""),
                MyField2 = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetString(reader, ""MyField2"")
            };
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_EntityMapper_Class_With_Custom_EntityMappings()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when EntityMapperNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .AddCustomEntityMappings(new EntityMappingBuilder().WithPropertyName("MyField2").WithMapping("some value"))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new EntityMapperContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var entityMapperPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<EntityMapperContext>>();

        // Act
        var result = (await entityMapperPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var commandEntityProvider = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(commandEntityProvider));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.EntityMapperGenerator"", @""1.0.0.0"")]
    public partial class MyEntityEntityMapper : CrossCutting.Data.Abstractions.IDatabaseEntityMapper<MyNamespace.MyEntity>
    {
        public MyNamespace.MyEntity Map(System.Data.IDataReader reader)
        {
            return new MyNamespace.MyEntity
            {
                MyField1 = CrossCutting.Data.Sql.Extensions.DataReaderExtensions.GetInt32(reader, ""MyField1""),
                MyField2 = @""some value""
            };
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_IdentityCommandProvider_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when IdentityCommandProviderNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField())
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new IdentityCommandProviderContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var identityCommandProviderPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<IdentityCommandProviderContext>>();

        // Act
        var result = (await identityCommandProviderPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var commandEntityProvider = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(commandEntityProvider));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.IdentityCommandProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityIdentityCommandProvider : CrossCutting.Data.Core.CommandProviders.IdentityDatabaseCommandProviderBase<MyNamespace.MyEntityIdentity>
    {
        public MyEntityIdentityCommandProvider(System.Collections.Generic.IEnumerable<CrossCutting.Data.Abstractions.IPagedDatabaseEntityRetrieverSettingsProvider> settingsProviders) : base(settingsProviders)
        {
        }

        protected override System.Collections.Generic.IEnumerable<CrossCutting.Data.Core.IdentityDatabaseCommandProviderField> GetFields()
        {
            yield return new CrossCutting.Data.Core.IdentityDatabaseCommandProviderField(@""Id"", @""Id"");
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_EntityRetrieverSettings_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when DatabasePagedEntityRetrieverSettingsNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new PagedEntityRetrieverSettingsContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var pagedEntityRetrieverSettingsPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<PagedEntityRetrieverSettingsContext>>();

        // Act
        var result = (await pagedEntityRetrieverSettingsPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var pagedEntityRetrieverSettings = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(pagedEntityRetrieverSettings));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.PagedEntityRetrieverSettingsGenerator"", @""1.0.0.0"")]
    public partial class MyEntityDatabaseEntityRetrieverSettings : CrossCutting.Data.Abstractions.IPagedDatabaseEntityRetrieverSettings
    {
        public string TableName
        {
            get
            {
                return @""MyEntity"";
            }
        }

        public string Fields
        {
            get
            {
                return @""MyField1, MyField2"";
            }
        }

        public string DefaultOrderBy
        {
            get
            {
                return string.Empty;
            }
        }

        public string DefaultWhere
        {
            get
            {
                return string.Empty;
            }
        }

        public System.Nullable<int> OverridePageSize
        {
            get
            {
                return -1;
            }
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_EntityRetrieverSettingsProvider_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when DatabaseEntityRetrieverSettingsProviderSettingsNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new DatabaseEntityRetrieverSettingsProviderContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var entityRetrieverSettingsProviderPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<DatabaseEntityRetrieverSettingsProviderContext>>();

        // Act
        var result = (await entityRetrieverSettingsProviderPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var pagedEntityRetrieverSettings = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(pagedEntityRetrieverSettings));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.DatabaseEntityRetrieverSettingsProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityDatabaseEntityRetrieverSettingsProvider : CrossCutting.Data.Abstractions.IDatabaseEntityRetrieverSettingsProvider, CrossCutting.Data.Abstractions.IPagedDatabaseEntityRetrieverSettingsProvider
    {
        bool CrossCutting.Data.Abstractions.IDatabaseEntityRetrieverSettingsProvider.TryGet<TSource>(out CrossCutting.Data.Abstractions.IDatabaseEntityRetrieverSettings? settings)
        {
            if (typeof(TSource) == typeof(MyNamespace.MyEntity) || typeof(TSource) == typeof(MyNamespace.MyEntityIdentity) || typeof(TSource) == typeof(MyNamespace.MyEntityQuery))
            {
                settings = new MyNamespace.MyEntityDatabaseEntityRetrieverSettings();
                return true;
            }
            settings = default;
            return false;
        }

        bool CrossCutting.Data.Abstractions.IPagedDatabaseEntityRetrieverSettingsProvider.TryGet<TSource>(out CrossCutting.Data.Abstractions.IPagedDatabaseEntityRetrieverSettings? settings)
        {
            if (typeof(TSource) == typeof(MyNamespace.MyEntity) || typeof(TSource) == typeof(MyNamespace.MyEntityIdentity) || typeof(TSource) == typeof(MyNamespace.MyEntityQuery))
            {
                settings = new MyNamespace.MyEntityDatabaseEntityRetrieverSettings();
                return true;
            }
            settings = default;
            return false;
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_Query_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when QueryNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new QueryContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var queryPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<QueryContext>>();

        // Act
        var result = (await queryPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var query = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(query));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.QueryGenerator"", @""1.0.0.0"")]
    public partial record MyEntityQuery : QueryFramework.Core.Query, System.ComponentModel.DataAnnotations.IValidatableObject
    {
        private static readonly string[] ValidFieldNames = new[] { ""MyField1"", ""MyField2"" };

        private const int MaxLimit = int.MaxValue;

        public MyEntityQuery() : this(null, null, new ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable(Enumerable.Empty<ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable>()), Enumerable.Empty<QueryFramework.Abstractions.IQuerySortOrder>())
        {
        }

        public MyEntityQuery(System.Nullable<int> limit, System.Nullable<int> offset, ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable filter, System.Collections.Generic.IEnumerable<QueryFramework.Abstractions.IQuerySortOrder> orderByFields) : base(limit, offset, filter, orderByFields)
        {
        }

        public MyEntityQuery(QueryFramework.Abstractions.IQuery query) : this(query.Limit, query.Offset, query.Filter, query.OrderByFields)
        {
        }

        public System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (Limit.HasValue && Limit.Value > MaxLimit)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Limit exceeds the maximum of "" + MaxLimit, new[] { nameof(Limit), nameof(Limit) });
            }
            foreach (var condition in Filter.Conditions)
            {
                if (!IsValidExpression(condition.LeftExpression))
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Invalid left expression in conditions: "" + condition.LeftExpression, new[] { nameof(Filter), nameof(Filter) });
                }
                if (!IsValidExpression(condition.RightExpression))
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Invalid right expression in conditions: "" + condition.RightExpression, new[] { nameof(Filter), nameof(Filter) });
                }
            }
            foreach (var querySortOrder in OrderByFields)
            {
                if (!IsValidExpression(querySortOrder.FieldNameExpression))
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult(""Invalid expression in order by fields: "" + querySortOrder.FieldNameExpression, new[] { nameof(OrderByFields), nameof(OrderByFields) });
                }
            }
        }

        public override QueryFramework.Abstractions.Builders.IQueryBuilder ToBuilder()
        {
            return new MyNamespace.MyEntityQueryBuilder(this);
        }

        private bool IsValidExpression(ExpressionFramework.Domain.Expression expression)
        {
            if (expression is ExpressionFramework.Domain.Expressions.FieldExpression fieldExpression)
            {
                return ValidFieldNames.Any(s => s.Equals(QueryFramework.Abstractions.Extensions.ExpressionExtensions.GetFieldName(fieldExpression), StringComparison.OrdinalIgnoreCase));
            }
            return true;
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_QueryBuilder_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when QueryBuilderNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .WithEnableNullableContext()
            .Build();
        var context = new QueryBuilderContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var queryPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<QueryBuilderContext>>();

        // Act
        var result = (await queryPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var queryBuilder = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(queryBuilder));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.QueryBuilderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityQueryBuilder : QueryFramework.Core.Builders.QueryBuilder
    {
        public MyEntityQueryBuilder() : base()
        {
        }

        public MyEntityQueryBuilder(QueryFramework.Abstractions.IQuery source) : base(source)
        {
        }

        public override QueryFramework.Abstractions.IQuery Build()
        {
            return BuildTyped();
        }

        public MyNamespace.MyEntityQuery BuildTyped()
        {
            return new MyNamespace.MyEntityQuery(Limit, Offset, Filter?.BuildTyped() ?? new ExpressionFramework.Domain.Builders.Evaluatables.ComposedEvaluatableBuilder().BuildTyped(), OrderByFields?.Select(x => x.Build()) ?? System.Linq.Enumerable.Empty<QueryFramework.Abstractions.IQuerySortOrder>());
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_QueryFieldInfo_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when QueryFieldInfoNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new QueryFieldInfoContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var queryFieldInfoPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<QueryFieldInfoContext>>();

        // Act
        var result = (await queryFieldInfoPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var queryFieldInfo = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(queryFieldInfo));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.QueryFieldInfoGenerator"", @""1.0.0.0"")]
    public partial class MyEntityQueryFieldInfo : QueryFramework.SqlServer.Abstractions.IQueryFieldInfo
    {
        public System.Collections.Generic.IEnumerable<string> GetAllFields()
        {
            yield return @""MyField1"";
            yield return @""MyField2"";
        }

        public string? GetDatabaseFieldName(string queryFieldName)
        {
            return GetAllFields().FirstOrDefault(x => x.Equals(queryFieldName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_QueryFieldInfoProvider_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when QueryFieldInfoProviderNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new QueryFieldInfoProviderContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var queryFieldInfoProviderPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<QueryFieldInfoProviderContext>>();

        // Act
        var result = (await queryFieldInfoProviderPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var queryFieldInfoProvider = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(queryFieldInfoProvider));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.QueryFieldInfoProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityQueryFieldInfoProvider : QueryFramework.SqlServer.Abstractions.IQueryFieldInfoProvider
    {
        public bool TryCreate(QueryFramework.Abstractions.IQuery query, out QueryFramework.SqlServer.Abstractions.IQueryFieldInfo? result)
        {
            if (query is MyNamespace.MyEntityQuery)
            {
                result = new MyNamespace.MyEntityQueryFieldInfo();
                return true;
            }
            result = default;
            return false;
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_Repository_Class_Without_Interface()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when RepositoryNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithDefaultIdentityNamespace("MyNamespace") //needed to use Identity entities
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new RepositoryContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var repositoryPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<RepositoryContext>>();

        // Act
        var result = (await repositoryPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);
        var repository = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(repository));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.RepositoryGenerator"", @""1.0.0.0"")]
    public partial class MyEntityRepository : CrossCutting.Data.Core.Repository<MyNamespace.MyEntity,MyNamespace.MyEntityIdentity>
    {
        public MyEntityRepository(CrossCutting.Data.Abstractions.IDatabaseCommandProcessor<MyNamespace.MyEntity> commandProcessor, CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<MyNamespace.MyEntity> entityRetriever, CrossCutting.Data.Abstractions.IDatabaseCommandProvider<MyNamespace.MyEntityIdentity> identitySelectCommandProvider, CrossCutting.Data.Abstractions.IPagedDatabaseCommandProvider pagedEntitySelectCommandProvider, CrossCutting.Data.Abstractions.IDatabaseCommandProvider entitySelectCommandProvider, CrossCutting.Data.Abstractions.IDatabaseCommandProvider<MyNamespace.MyEntity> entityCommandProvider) : base(commandProcessor, entityRetriever, identitySelectCommandProvider, pagedEntitySelectCommandProvider, entitySelectCommandProvider, entityCommandProvider)
        {
        }
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Code_For_Repository_Class_With_Interface()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when RepositoryNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)))
            .AddFields(new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string)))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithDefaultIdentityNamespace("MyNamespace")
            .WithRepositoryInterfaceNamespace("MyNamespace.Contracts")
            .WithUseRepositoryInterface() // needed to use repository interface
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new RepositoryContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var repositoryPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<RepositoryContext>>();

        // Act
        var result = (await repositoryPipeline.Process(context)).ProcessResult(context.Builder, context.Builder.BuildTyped);
        var repository = result.GetValueOrThrow();
        var code1 = await GenerateCode(new TestCodeGenerationProvider(repository));

        var interfaceContext = new RepositoryInterfaceContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var interfacePipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<RepositoryInterfaceContext>>();
        var interfaceResult = (await interfacePipeline.Process(interfaceContext)).ProcessResult(interfaceContext.Builder, interfaceContext.Builder.BuildTyped);
        var repositoryInterface = interfaceResult.GetValueOrThrow();
        var code2 = await GenerateCode(new TestCodeGenerationProvider(repositoryInterface));

        // Assert
        code1.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.RepositoryGenerator"", @""1.0.0.0"")]
    public partial class MyEntityRepository : CrossCutting.Data.Core.Repository<MyNamespace.MyEntity,MyNamespace.MyEntityIdentity>, MyNamespace.Contracts.IMyEntityRepository
    {
        public MyEntityRepository(CrossCutting.Data.Abstractions.IDatabaseCommandProcessor<MyNamespace.MyEntity> commandProcessor, CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<MyNamespace.MyEntity> entityRetriever, CrossCutting.Data.Abstractions.IDatabaseCommandProvider<MyNamespace.MyEntityIdentity> identitySelectCommandProvider, CrossCutting.Data.Abstractions.IPagedDatabaseCommandProvider pagedEntitySelectCommandProvider, CrossCutting.Data.Abstractions.IDatabaseCommandProvider entitySelectCommandProvider, CrossCutting.Data.Abstractions.IDatabaseCommandProvider<MyNamespace.MyEntity> entityCommandProvider) : base(commandProcessor, entityRetriever, identitySelectCommandProvider, pagedEntitySelectCommandProvider, entitySelectCommandProvider, entityCommandProvider)
        {
        }
    }
}
#nullable disable
");
        code2.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace MyNamespace.Contracts
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.RepositoryInterfaceGenerator"", @""1.0.0.0"")]
    public partial interface IMyEntityRepository : CrossCutting.Data.Abstractions.IRepository<MyNamespace.MyEntity,MyNamespace.MyEntityIdentity>
    {
    }
}
#nullable disable
");
    }

    [Fact]
    public async Task Can_Create_Everything()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when RepositoryNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields
            (
                new FieldInfoBuilder().WithName("Id").WithType(typeof(int)).WithIsIdentityField(),
                new FieldInfoBuilder().WithName("MyField1").WithType(typeof(int)),
                new FieldInfoBuilder().WithName("MyField2").WithType(typeof(string))
            )
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithDefaultIdentityNamespace("MyNamespace")
            .WithRepositoryInterfaceNamespace("MyNamespace.Contracts")
            .WithUseRepositoryInterface() // needed to use repository interface
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .WithEnableNullableContext()
            .Build();
        var generationEnvironment = new MultipleStringContentBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = GetCodeGenerationEngine();

        // Act
        foreach (var contextType in typeof(ContextBase).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(ContextBase) && !x.IsAbstract && x.GetProperty("Builder") is not null))
        {
            var context = Activator.CreateInstance(contextType, sourceModel, settings, CultureInfo.InvariantCulture);
            var pipeline = Scope!.ServiceProvider.GetRequiredService(typeof(IPipeline<>).MakeGenericType(contextType));
            var builder = (TypeBaseBuilder)contextType.GetProperty("Builder")!.GetValue(context)!;
            var task = (Task<Result>)pipeline.GetType().GetMethod(nameof(IPipeline<ContextBase>.Process))!.Invoke(pipeline, [context, CancellationToken.None])!;
            var result = (await task.ConfigureAwait(true)).ProcessResult(builder, builder.Build);
            var classInstance = result.GetValueOrThrow();
            (await codeGenerationEngine.Generate(new TestCodeGenerationProvider(classInstance, true), generationEnvironment, codeGenerationSettings)).ThrowIfInvalid();
        }

        var content = generationEnvironment.Builder.Build();
        var allContents = string.Join(Environment.NewLine, content.Contents.Select(x => x.Contents));
        allContents.Should().NotBeEmpty();
        ///(await generationEnvironment.SaveContents(new TestCodeGenerationProvider(new ClassBuilder().WithName("DummyClass").Build(), true), @"D:\Git\DataFramework\src\DataFramework.Pipelines.Tests\POC", "GeneratedCode.cs", CancellationToken.None)).ThrowIfInvalid();
    }

    private sealed class TestCodeGenerationProvider : CsharpClassGeneratorCodeGenerationProviderBase
    {
        private readonly TypeBase _model;
        private readonly bool _generateMultipleFiles;

        public TestCodeGenerationProvider(TypeBase model) : this(model, false)
        {
        }

        public TestCodeGenerationProvider(TypeBase model, bool generateMultipleFiles)
        {
            _model = model;
            _generateMultipleFiles = generateMultipleFiles;
        }

        public override string Path => string.Empty;

        public override bool RecurseOnDeleteGeneratedFiles => false;

        public override string LastGeneratedFilesFilename => string.Empty;

        public override Encoding Encoding => Encoding.UTF8;

        public override CsharpClassGeneratorSettings Settings
            => new CsharpClassGeneratorSettingsBuilder()
                .WithCultureInfo(CultureInfo.InvariantCulture)
                .WithEncoding(Encoding)
                .WithEnableNullableContext()
                .WithGenerateMultipleFiles(_generateMultipleFiles)
                .Build();

        public override Task<IEnumerable<TypeBase>> GetModel()
            => Task.FromResult<IEnumerable<TypeBase>>([_model]);
    }

    private sealed class TestDatabaseSchemaGenerationProvider : DatabaseSchemaGeneratorCodeGenerationProviderBase
    {
        private readonly IEnumerable<IDatabaseObject> _model;

        public TestDatabaseSchemaGenerationProvider(IEnumerable<IDatabaseObject> model)
        {
            _model = model;
        }

        public override string Path => string.Empty;

        public override bool RecurseOnDeleteGeneratedFiles => false;

        public override string LastGeneratedFilesFilename => string.Empty;

        public override Encoding Encoding => Encoding.UTF8;

        public override Task<IEnumerable<IDatabaseObject>> GetModel() => Task.FromResult(_model);

        public override DatabaseSchemaGeneratorSettings Settings => new DatabaseSchemaGeneratorSettingsBuilder()
            .WithCultureInfo(CultureInfo.InvariantCulture)
            .WithEncoding(Encoding)
            .Build();
    }
}
