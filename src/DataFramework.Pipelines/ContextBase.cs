namespace DataFramework.Pipelines;

public abstract class ContextBase
{
    public PipelineSettings Settings { get; }
    public IFormatProvider FormatProvider { get; }

    protected ContextBase(PipelineSettings settings, IFormatProvider formatProvider)
    {
        Settings = settings.IsNotNull(nameof(settings));
        FormatProvider = formatProvider.IsNotNull(nameof(formatProvider));
    }

    public IEnumerable<CodeStatementBaseBuilder> GetGetterCodeStatements(DataObjectInfo dataObjectInfo, FieldInfo field)
        => Settings.CodeStatementMappings
            .Where(x => AreEqual(dataObjectInfo, x.SourceDataObjectInfo) && AreEqual(field, x.SourceFieldInfo))
            .SelectMany(x => x.CodeStatements);

    public bool AreEqual(FieldInfo field, FieldInfo sourceFieldInfo)
        => field.Name == sourceFieldInfo.Name;

    public bool AreEqual(DataObjectInfo dataObjectInfo, DataObjectInfo sourceDataObjectInfo)
        => dataObjectInfo.Name == sourceDataObjectInfo.Name
            && dataObjectInfo.TypeName == sourceDataObjectInfo.TypeName;
}

public abstract class ContextBase<TSourceModel> : ContextBase
{
    protected ContextBase(TSourceModel sourceModel, PipelineSettings settings, IFormatProvider formatProvider) : base(settings, formatProvider)
    {
        SourceModel = sourceModel.IsNotNull(nameof(sourceModel));
    }

    public TSourceModel SourceModel { get; }
}
