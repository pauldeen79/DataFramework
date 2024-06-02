namespace DataFramework.Pipelines.Tests;

public sealed class IntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task Can_Create_Code_For_Immutable_Class_Entity_With_ConcurrencyChecks()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.ImmutableClass)
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var pipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await pipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(context.Builder.Build()), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNamespace
{
    public partial class MyEntity : IEquatable<MyEntity>
    {
        public int MyField
        {
            get;
        }

        public int MyFieldOriginal
        {
            get;
        }

        public MyEntity(int myField, int myFieldOriginal = default)
        {
            this.MyField = myField;
            this.MyFieldOriginal = myFieldOriginal;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public MyNamespace.MyEntityBuilder ToBuilder()
        {
            return new MyNamespace.MyEntityBuilder(this);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MyEntity);
        }

        public bool Equals(MyEntity other)
        {
            return other is not null &&
                MyField == other.MyField;
        }

        public override int GetHashCode()
        {
            int hashCode = 235838129;
            hashCode = hashCode * -1521134295 + MyField.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(MyEntity left, MyEntity right)
        {
            return EqualityComparer<MyEntity>.Default.Equals(left, right);
        }

        public static bool operator !=(MyEntity left, MyEntity right)
        {
            return !(left == right);
        }
    }
}
");
    }

    [Fact]
    public async Task Can_Create_Code_For_Immutable_Class_Entity_With_ConcurrencyChecks_Using_ClassFramework_EntityPipeline()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.Poco) //default
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var classFrameworkPipelineService = Scope.ServiceProvider.GetRequiredService<ClassFramework.Pipelines.Abstractions.IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var entity = context.Builder.Build();
        var entityContext = new ClassFramework.Pipelines.Entity.EntityContext(entity, new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder().WithAddFullConstructor(true).Build(), CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(entityContext, CancellationToken.None);
        result.ThrowIfInvalid();
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(entityContext.Builder.Build()), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNamespace
{
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
    public async Task Can_Create_Code_For_Builder_For_Immutable_Class_Entity_With_ConcurrencyChecks()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder()
            .WithName("MyEntity")
            .AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!))
            .Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.ImmutableClass)
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var dataFrameworkPipelineService = Scope!.ServiceProvider.GetRequiredService<IPipelineService>();
        var classFrameworkPipelineService = Scope.ServiceProvider.GetRequiredService<ClassFramework.Pipelines.Abstractions.IPipelineService>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await dataFrameworkPipelineService.Process(context, CancellationToken.None);
        result.ThrowIfInvalid();
        var entity = context.Builder.Build();
        var builderContext = new ClassFramework.Pipelines.Builder.BuilderContext(entity, new ClassFramework.Pipelines.Builders.PipelineSettingsBuilder().Build(), CultureInfo.InvariantCulture);
        result = await classFrameworkPipelineService.Process(builderContext, CancellationToken.None);
        result.ThrowIfInvalid();
        await codeGenerationEngine.Generate(new TestCodeGenerationProvider(builderContext.Builder.Build()), generationEnvironment, codeGenerationSettings, CancellationToken.None);

        // Assert
        generationEnvironment.Builder.ToString().Should().Be(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNamespace.Builders
{
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

    private sealed class TestCodeGenerationProvider : CsharpClassGeneratorCodeGenerationProviderBase
    {
        private readonly TypeBase _model;

        public TestCodeGenerationProvider(TypeBase model)
            => _model = model;

        public override string Path
            => string.Empty;

        public override bool RecurseOnDeleteGeneratedFiles
            => false;

        public override string LastGeneratedFilesFilename
            => string.Empty;

        public override Encoding Encoding
            => Encoding.UTF8;

        public override CsharpClassGeneratorSettings Settings
            => new CsharpClassGeneratorSettingsBuilder()
                .WithCultureInfo(CultureInfo.InvariantCulture)
                .WithEncoding(Encoding)
                .Build();

        public override Task<IEnumerable<TypeBase>> GetModel()
            => Task.FromResult<IEnumerable<TypeBase>>([_model]);
    }
}
