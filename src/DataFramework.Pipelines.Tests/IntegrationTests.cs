namespace DataFramework.Pipelines.Tests;

public sealed class IntegrationTests : TestBase
{
    public IntegrationTests()
    {
        var assemblyInfoContextService = Fixture.Freeze<IAssemblyInfoContextService>();
        Provider = new ServiceCollection()
            .AddTemplateFramework()
            .AddTemplateFrameworkChildTemplateProvider()
            .AddTemplateFrameworkCodeGeneration()
            .AddTemplateFrameworkRuntime()
            .AddCsharpExpressionDumper()
            .AddClassFrameworkTemplates()
            .AddParsers()
            .AddPipelines()
            .AddDataFrameworkPipelines()
            .AddScoped(_ => assemblyInfoContextService)
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        Scope = Provider.CreateScope();
    }

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
        var entityPipeline = Scope!.ServiceProvider.GetRequiredService<IPipeline<EntityContext>>();
        var generationEnvironment = new StringBuilderEnvironment();
        var codeGenerationSettings = new CodeGenerationSettings(string.Empty, "GeneratedCode.cs", true);
        var codeGenerationEngine = Scope.ServiceProvider.GetRequiredService<ICodeGenerationEngine>();

        // Act
        var result = await entityPipeline.Process(context);
        result.ThrowIfInvalid();
        await codeGenerationEngine.Generate(new MyGenerator(context.Builder.Build()), generationEnvironment, codeGenerationSettings, CancellationToken.None);

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

    private sealed class MyGenerator : CsharpClassGeneratorCodeGenerationProviderBase
    {
        private readonly TypeBase _model;

        public MyGenerator(TypeBase model)
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
