namespace DataFramework.CodeGeneration.CodeGenerationProviders;

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
}
