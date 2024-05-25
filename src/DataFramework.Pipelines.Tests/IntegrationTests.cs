namespace DataFramework.Pipelines.Tests;

public sealed class IntegrationTests : TestBase
{
    public IntegrationTests()
    {
        var templateFactory = Fixture.Freeze<ITemplateFactory>();
        var templateProviderPluginFactory = Fixture.Freeze<ITemplateComponentRegistryPluginFactory>();
        Provider = new ServiceCollection()
            .AddTemplateFramework()
            .AddTemplateFrameworkChildTemplateProvider()
            .AddCsharpExpressionDumper()
            .AddClassFrameworkTemplates()
            .AddParsers()
            .AddPipelines()
            .AddDataFrameworkPipelines()
            .AddScoped(_ => templateFactory)
            .AddScoped(_ => templateProviderPluginFactory)
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        Scope = Provider.CreateScope();
        templateFactory.Create(Arg.Any<Type>()).Returns(x => Scope.ServiceProvider.GetRequiredService(x.ArgAt<Type>(0)));
    }

    [Fact]
    public async Task Can_Create_Code_For_Immutable_Class_Entity_With_ConcurrencyChecks()
    {
        // Arrange
        var sourceModel = new DataObjectInfoBuilder().WithName("MyEntity").AddFields(new FieldInfoBuilder().WithName("MyField").WithTypeName(typeof(int).FullName!)).Build();
        var settings = new PipelineSettingsBuilder()
            .WithEntityClassType(EntityClassType.ImmutableClass)
            .WithDefaultEntityNamespace("MyNamespace")
            .WithConcurrencyCheckBehavior(ConcurrencyCheckBehavior.AllFields)
            .Build();
        var context = new EntityContext(sourceModel, settings, CultureInfo.InvariantCulture);
        var entityPipeline = Scope!.ServiceProvider.GetRequiredService<IPipelineBuilder<EntityContext>>().Build();
        var templateEngine = Scope.ServiceProvider.GetRequiredService<ITemplateEngine>();
        var templateFactory = Scope.ServiceProvider.GetRequiredService<ITemplateFactory>();
        TemplateTypeIdentifier identifier = new TemplateTypeIdentifier(typeof(CsharpClassGenerator), templateFactory);
        var builder = new System.Text.StringBuilder();

        // Act
        var result = await entityPipeline.Process(context);
        result.ThrowIfInvalid();
        var viewModel = new CsharpClassGeneratorViewModel
        {
            Model = [context.Builder.Build()],
            Settings = new CsharpClassGeneratorSettingsBuilder()
                .WithEncoding(Encoding.UTF8)
                .WithCultureInfo(CultureInfo.InvariantCulture)
                .Build()
        };
        await templateEngine.Render(new RenderTemplateRequest(identifier, viewModel, builder), CancellationToken.None);

        // Assert
        builder.ToString().Should().Be(@"using System;
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
}
