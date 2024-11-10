namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract class DataFrameworkCSharpClassBase : CsharpClassGeneratorPipelineCodeGenerationProviderBase
{
    protected DataFrameworkCSharpClassBase(IPipelineService pipelineService) : base(pipelineService)
    {
    }

    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string LastGeneratedFilesFilename => string.Empty;
    public override Encoding Encoding => Encoding.UTF8;

    protected override Type EntityCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type EntityConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override Type BuilderCollectionType => typeof(ObservableCollection<>);

    protected override string ProjectName => "DataFramework";
    protected override string CoreNamespace => "DataFramework.Domain";
    protected override bool CopyAttributes => true;
    protected override bool CopyInterfaces => true;
    protected override bool GenerateMultipleFiles => false;
    protected override bool EnableGlobalUsings => true;

    protected async Task<TypeBase[]> GetPipelineModels()
        => await GetNonCoreModels($"{CodeGenerationRootNamespace}.Models.Pipelines").ConfigureAwait(false);

    protected override bool SkipNamespaceOnTypenameMappings(string @namespace)
        => @namespace == $"{CodeGenerationRootNamespace}.Models.Pipelines";

    protected override IEnumerable<TypenameMappingBuilder> CreateAdditionalTypenameMappings()
        => GetType().Assembly.GetTypes()
            .Where(x => x.IsInterface && x.Namespace == $"{CodeGenerationRootNamespace}.Models.Pipelines" && !GetCustomBuilderTypes().Contains(x.GetEntityClassName()))
            .SelectMany(x => CreateCustomTypenameMappings(x, "DataFramework.Pipelines", "DataFramework.Pipelines.Builders"))
            .Concat(
            [
                new TypenameMappingBuilder().WithSourceType(typeof(ConcurrencyCheckBehavior)).WithTargetTypeName($"DataFramework.Pipelines.Domains.{nameof(ConcurrencyCheckBehavior)}"),
                new TypenameMappingBuilder().WithSourceType(typeof(EntityClassType)).WithTargetTypeName($"DataFramework.Pipelines.Domains.{nameof(EntityClassType)}"),
            ])
            .Concat(typeof(CheckConstraint).Assembly.GetExportedTypes().Where(x => x.Namespace == "DatabaseFramework.Domain").Select(x => new TypenameMappingBuilder()
                .WithSourceType(x)
                .WithTargetType(x)
                .AddMetadata(CreateTypenameMappingMetadata(x))
            ))
            .Concat(typeof(TypeBase).Assembly.GetExportedTypes().Where(x => x.Namespace == "ClassFramework.Domain").Select(x => new TypenameMappingBuilder()
                .WithSourceType(x)
                .WithTargetType(x)
                .AddMetadata(CreateTypenameMappingMetadata(x))
            ));

    private static IEnumerable<TypenameMappingBuilder> CreateCustomTypenameMappings(Type modelType, string entityNamespace, string buildersNamespace)
        =>
        [
            new TypenameMappingBuilder()
                .WithSourceType(modelType)
                .WithTargetTypeName($"{entityNamespace}.{modelType.GetEntityClassName()}"),
            new TypenameMappingBuilder()
                .WithSourceTypeName($"{entityNamespace}.{modelType.GetEntityClassName()}")
                .WithTargetTypeName($"{entityNamespace}.{modelType.GetEntityClassName()}")
                .AddMetadata(CreateTypenameMappingMetadata(buildersNamespace)),
        ];
}
