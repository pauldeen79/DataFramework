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

namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.ClassGenerator"", @""1.0.0.0"")]
    public partial class MyEntity
    {
        public int MyField
        {
            get;
        }

        public int MyFieldOriginal
        {
            get;
        }

        public MyEntity(int myField, int myFieldOriginal)
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
        public int MyFieldOriginal
        {
            get;
            set;
        }

        public MyEntity()
        {
            MyField = default(System.Int32);
            MyFieldOriginal = default(System.Int32);
        }
    }
}
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
            .WithDefaultEntityNamespace("MyNamespace")
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

        public Builders.MyEntityIdentityBuilder ToBuilder()
        {
            return new Builders.MyEntityIdentityBuilder(this);
        }
    }
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

        public int MyFieldOriginal
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
            MyField = default(System.Int32);
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

        public MyNamespace.Builders.MyEntityBuilder WithMyFieldOriginal(int myFieldOriginal)
        {
            MyFieldOriginal = myFieldOriginal;
            return this;
        }
    }
}
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

namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.CommandEntityProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityCommandEntityProvider : CrossCutting.Data.Abstractions.IDatabaseCommandEntityProvider<MyNamespace.MyEntity>
    {
        public CrossCutting.Data.Abstractions.CreateResultEntityHandler<MyNamespace.MyEntity, CrossCutting.Data.Abstractions.DatabaseOperation, MyNamespace.MyEntity> CreateResultEntity
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
                }
            }
        }

        public CrossCutting.Data.Abstractions.AfterReadHandler<MyNamespace.MyEntity, CrossCutting.Data.Abstractions.DatabaseOperation, System.Data.IDataReader, MyNamespace.MyEntity> AfterRead
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
                             throw new System.ArgumentOutOfRangeException(""operation"", string.Format(""Unsupported operation: {0}"", operation));
                    }
                }
            }
        }

        public CrossCutting.Data.Abstractions.CreateBuilderHandler<MyNamespace.MyEntity, MyNamespace.MyEntity> CreateBuilder
        {
            get
            {
                return entity;
            }
        }

        public CrossCutting.Data.Abstractions.CreateEntityHandler<MyNamespace.MyEntity, MyNamespace.MyEntity> CreateEntity
        {
            get
            {
                return builder => builder.Build();
            }
        }

        private MyNamespace.MyEntity AddResultEntity(MyNamespace.MyEntity resultEntity)
        {
            return resultEntity;
        }

        private MyNamespace.MyEntity AddAfterRead(MyNamespace.MyEntity resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.MyField = reader.GetInt32(@""MyField"");
            resultEntity.MyFieldOriginal = reader.GetInt32(@""MyField"");
            return resultEntity;
        }

        private MyNamespace.MyEntity UpdateResultEntity(MyNamespace.MyEntity resultEntity)
        {
            return resultEntity;
        }

        private MyNamespace.MyEntity UpdateAfterRead(MyNamespace.MyEntity resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.MyField = reader.GetInt32(@""MyField"");
            resultEntity.MyFieldOriginal = reader.GetInt32(@""MyField"");
            return resultEntity;
        }

        private MyNamespace.MyEntity DeleteResultEntity(MyNamespace.MyEntity resultEntity)
        {
            return resultEntity;
        }

        private MyNamespace.MyEntity DeleteAfterRead(MyNamespace.MyEntity resultEntity, System.Data.IDataReader reader)
        {
            resultEntity.MyField = reader.GetInt32(@""MyField"");
            resultEntity.MyFieldOriginal = reader.GetInt32(@""MyField"");
            return resultEntity;
        }
    }
}
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

namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.CommandProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityCommandProvider : CrossCutting.Data.Abstractions.IDatabaseCommandProvider<MyEntityIdentity>
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
");
    }

    [Fact]
    public async Task Can_Create_Code_For_DatabaseEntityRetrieverProvider_Class()
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

        public bool TryCreate<TResult>(QueryFramework.Abstractions.IQuery query, out CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<TResult> result)
            where TResult : class
        {
            if (typeof(TResult) == typeof(MyNamespace.MyEntity)
            {
                result = (CrossCutting.Data.Abstractions.IDatabaseEntityRetriever<MyNamespace.MyEntity>)_databaseEntityRetriever;
                return true;
            }
            result = default;
            return false;
        }
    }
}
");
    }

    [Fact]
    public async Task Can_Create_Code_For_DatabaseSchema()
    {
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
	[MyField] INT NOT NULL
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
            .WithTypeName("MyNamespace.MyEntity") // this will be used when CommandEntityProviderNamespace is empty on the settings
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithType(typeof(int)))
            .Build();
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
	[MyField] INT NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteMyEntity]
	@MyField INT
AS
BEGIN
    DELETE FROM [MyEntity] OUTPUT DELETED.[MyField] WHERE [MyField] = @MyFieldOriginal
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertMyEntity]
	@MyField INT
AS
BEGIN
    INSERT INTO [MyEntity]([MyField]) OUTPUT INSERTED.[MyField] VALUES(@MyField)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateMyEntity]
	@MyField INT
AS
BEGIN
    UPDATE [MyEntity] SET [MyField] = @MyField OUTPUT INSERTED.[MyField] WHERE [MyField] = @MyFieldOriginal
END
GO
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

namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.EntityMapperGenerator"", @""1.0.0.0"")]
    public partial class MyEntityEntityMapper : CrossCutting.Data.Abstractions.IDatabaseEntityMapper<MyNamespace.MyEntity>
    {
        public MyNamespace.MyEntity Map(System.Data.IDataReader reader)
        {
            return new MyNamespace.MyEntity
            {
                MyField1 = reader.GetInt32(""MyField1""),
                MyField2 = reader.GetString(""MyField2"")
            };
        }
    }
}
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

namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.IdentityCommandProviderGenerator"", @""1.0.0.0"")]
    public partial class MyEntityIdentityCommandProvider : CrossCutting.Data.Core.CommandProviders.IdentityDatabaseCommandProviderBase<MyEntityIdentity>
    {
        public MyEntityIdentityCommandProvider() : base(new MyEntityEntityRetriever())
        {
        }

        protected override System.Collections.Generic.IEnumerable<CrossCutting.Data.Core.IdentityDatabaseCommandProviderField> GetFields(MyEntityIdentity source, CrossCutting.Data.Abstractions.DatabaseOperation operation)
        {
            yield return new IdentityDatabaseCommandProviderField(@""Id"", @""Id"");
        }
    }
}
");
    }

    [Fact]
    public async Task Can_Create_Code_For_PagedEntityRetrieverSettings_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when PagedEntityRetrieverSettingsNamespace is empty on the settings
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

namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.PagedEntityRetrieverSettingsGenerator"", @""1.0.0.0"")]
    public partial class MyEntityPagedEntityRetrieverSettings : CrossCutting.Data.Abstractions.IPagedDatabaseEntityRetrieverSettings
    {
        public string TableName
        {
            get
            {
                return MyEntity;
            }
        }

        public string Fields
        {
            get
            {
                return new[]
                {
                    @""MyField1"",
                    @""MyField2"",
                };
            }
        }

        public string DefaultOrderBy
        {
            get
            {
                return null;
            }
        }

        public string DefaultWhere
        {
            get
            {
                return null;
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
");
    }

    [Fact]
    public async Task Can_Create_Code_For_Query_Class()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithTypeName("MyNamespace.MyEntity") // this will be used when PagedEntityRetrieverSettingsNamespace is empty on the settings
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
        var pagedEntityRetrieverSettings = result.GetValueOrThrow();
        var code = await GenerateCode(new TestCodeGenerationProvider(pagedEntityRetrieverSettings));

        // Assert
        code.Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNamespace
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute(@""DataFramework.Pipelines.QueryGenerator"", @""1.0.0.0"")]
    public partial class MyEntityQuery : QueryFramework.Core.Query, System.ComponentModel.DataAnnotations.IValidatableObject
    {
        private static readonly string[] ValidFieldNames = new[] { ""MyField1"", ""MyField2"" };

        private const int MaxLimit = int.MaxValue;

        public MyEntityQuery() : this(null, null, new ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable(Enumerable.Empty<ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable>()), Enumerable.Empty<QueryFramework.Abstractions.IQuerySortOrder>())
        {
        }

        public MyEntityQuery(System.Nullable<int> limit, System.Nullable<int> offset, ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable filter, System.Collections.Generic.IEnumerable<QueryFramework.Abstractions.IQuerySortOrder> orderByFields) : base(limit, offset, filter, orderByFields)
        {
        }

        public MyEntityQuery(QueryFramework.Abstractions.IQuery query) : this(query.Limit, query.Offset, query.Filter, query.OrderByFields
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

        private bool IsValidExpression(ExpressionFramework.Domain.Expression expression)
        {
            if (expression is ExpressionFramework.Domain.Expressions.FieldExpression fieldExpression)
            {
                return ValidFieldNames.Any(s => s.Equals(fieldExpression.FieldName, ""StringComparison.OrdinalIgnoreCase""));
            }
            return true;
        }
    }
}
");
    }

    private sealed class TestCodeGenerationProvider : CsharpClassGeneratorCodeGenerationProviderBase
    {
        private readonly TypeBase _model;

        public TestCodeGenerationProvider(TypeBase model) => _model = model;

        public override string Path => string.Empty;

        public override bool RecurseOnDeleteGeneratedFiles => false;

        public override string LastGeneratedFilesFilename => string.Empty;

        public override Encoding Encoding => Encoding.UTF8;

        public override CsharpClassGeneratorSettings Settings
            => new CsharpClassGeneratorSettingsBuilder()
                .WithCultureInfo(CultureInfo.InvariantCulture)
                .WithEncoding(Encoding)
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
