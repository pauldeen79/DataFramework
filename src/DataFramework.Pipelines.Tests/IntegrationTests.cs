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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var classFrameworkPipelineService = Scope.ServiceProvider.GetRequiredService<ClassFramework.Pipelines.Abstractions.IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var entity = result.Value!;
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor(true)
            .WithAddPublicParameterlessConstructor(false)
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext, CancellationToken.None);
        result.ThrowIfInvalid();
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(result.Value!), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var classFrameworkPipelineService = Scope.ServiceProvider.GetRequiredService<ClassFramework.Pipelines.Abstractions.IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var entity = result.Value!;
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor(false)
            .WithAddPublicParameterlessConstructor(true) // note that you might want to omit this in case you don't have custom default values
            .WithAddSetters()
            .WithToBuilderFormatString(string.Empty) // no builder necessary
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext, CancellationToken.None);
        result.ThrowIfInvalid();
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(result.Value!), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var classFrameworkPipelineService = Scope.ServiceProvider.GetRequiredService<ClassFramework.Pipelines.Abstractions.IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var entity = result.Value!;
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor(true)
            .WithAddPublicParameterlessConstructor(false)
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext, CancellationToken.None);
        result.ThrowIfInvalid();
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(result.Value!), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var classFrameworkPipelineService = Scope.ServiceProvider.GetRequiredService<ClassFramework.Pipelines.Abstractions.IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var cls = result.Value!;
        var classFrameworkSettings = new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder()
            .WithAddFullConstructor()
            .WithAddSetters(false)
            .WithCopyAttributes()
            .Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(cls, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext, CancellationToken.None);
        result.ThrowIfInvalid();
        var entity = result.Value!;
        var builderContext = new ClassFramework.Pipelines.Builder.BuilderContext(entity, classFrameworkSettings, CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(builderContext, CancellationToken.None);
        result.ThrowIfInvalid();
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(result.Value!), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var commandEntityProvider = result.Value!;
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(commandEntityProvider), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var commandProvider = result.Value!;
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(commandProvider), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var commandProvider = result.Value!;
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(commandProvider), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var databaseObjects = result.Value!;
        await codeGenerationEngine.Generate(new TestDatabaseSchemaGenerationProvider(databaseObjects), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().ReplaceLineEndings().Should().Be(@"SET ANSI_NULLS ON
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var databaseObjects = result.Value!;
        await codeGenerationEngine.Generate(new TestDatabaseSchemaGenerationProvider(databaseObjects), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().ReplaceLineEndings().Should().Be(@"SET ANSI_NULLS ON
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var commandEntityProvider = result.Value!;
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(commandEntityProvider), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var commandEntityProvider = result.Value!;
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(commandEntityProvider), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
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
