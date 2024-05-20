namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract class DataFrameworkCSharpClassBase : CsharpClassGeneratorPipelineCodeGenerationProviderBase
{
    protected DataFrameworkCSharpClassBase(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService)
    {
        CsharpExpressionDumper = csharpExpressionDumper;
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

    protected ICsharpExpressionDumper CsharpExpressionDumper { get; }

    protected async Task<TypeBase[]> GetPipelineModels()
    => await GetNonCoreModels($"{CodeGenerationRootNamespace}.Models.Pipelines").ConfigureAwait(false);

    protected override bool SkipNamespaceOnTypenameMappings(string @namespace)
        => @namespace == $"{CodeGenerationRootNamespace}.Models.Pipelines";

    protected override IEnumerable<TypenameMappingBuilder> CreateAdditionalTypenameMappings()
        => GetType().Assembly.GetTypes()
            .Where(x => x.IsInterface && x.Namespace == $"{CodeGenerationRootNamespace}.Models.Pipelines" && !GetCustomBuilderTypes().Contains(x.GetEntityClassName()))
            .SelectMany(x => CreateCustomTypenameMappings(x, "DataFramework.Pipelines", "DataFramework.Pipelines.Builders"))
            //TODO: Replace with domains from our own project
            /*.Concat(
            [
                new TypenameMappingBuilder().WithSourceType(typeof(ArgumentValidationType)).WithTargetTypeName($"DataFramework.Pipelines.Domains.{nameof(ArgumentValidationType)}"),
            ])*/;

    private IEnumerable<TypenameMappingBuilder> CreateCustomTypenameMappings(Type modelType, string entityNamespace, string builderNamespace) =>
        [
            new TypenameMappingBuilder()
                .WithSourceType(modelType)
                .WithTargetTypeName($"{entityNamespace}.{modelType.GetEntityClassName()}"),
            new TypenameMappingBuilder()
                .WithSourceTypeName($"{entityNamespace}.{modelType.GetEntityClassName()}")
                .WithTargetTypeName($"{entityNamespace}.{modelType.GetEntityClassName()}")
                .AddMetadata
                (
                    new MetadataBuilder().WithValue(builderNamespace).WithName(MetadataNames.CustomBuilderNamespace),
                    new MetadataBuilder().WithValue("{TypeName.ClassName}Builder").WithName(MetadataNames.CustomBuilderName),
                    new MetadataBuilder().WithValue("[Name][NullableSuffix].ToBuilder()[ForcedNullableSuffix]").WithName(MetadataNames.CustomBuilderSourceExpression),
                    new MetadataBuilder().WithValue("[Name][NullableSuffix].Build()[ForcedNullableSuffix]").WithName(MetadataNames.CustomBuilderMethodParameterExpression)
                ),
        ];
}
