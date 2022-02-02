namespace DataFramework.ModelFramework;

public record GeneratorSettings
{
    public static readonly GeneratorSettings Default = new GeneratorSettings(RenderMetadataAsAttributesTypes.Validation | RenderMetadataAsAttributesTypes.Custom,
                                                                             EntityClassType.Poco,
                                                                             true,
                                                                             true,
                                                                             true);

    public RenderMetadataAsAttributesTypes DefaultRenderMetadataAsAttributes { get; }
    public EntityClassType DefaultEntityClassType { get; }
    public bool EnableNullableContext { get; }
    public bool CreateCodeGenerationHeaders { get; }
    public bool AddValidationCodeInConstructor { get; }

    public GeneratorSettings(RenderMetadataAsAttributesTypes defaultRenderMetadataAsAttributes,
                             EntityClassType defaultEntityClassType,
                             bool enableNullableContext,
                             bool createCodeGenerationHeaders,
                             bool addValidationCodeInConstructor)
    {
        DefaultRenderMetadataAsAttributes = defaultRenderMetadataAsAttributes;
        DefaultEntityClassType = defaultEntityClassType;
        EnableNullableContext = enableNullableContext;
        CreateCodeGenerationHeaders = createCodeGenerationHeaders;
        AddValidationCodeInConstructor = addValidationCodeInConstructor;
    }
}
