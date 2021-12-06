namespace DataFramework.ModelFramework
{
    public record GeneratorSettings
    {
        public static readonly GeneratorSettings Default = new GeneratorSettings(RenderMetadataAsAttributesType.Validation,
                                                                                 EntityClassType.Poco,
                                                                                 true,
                                                                                 true);

        public RenderMetadataAsAttributesType DefaultRenderMetadataAsAttributes { get; }
        public EntityClassType DefaultEntityClassType { get; }
        public bool EnableNullableContext { get; }
        public bool CreateCodeGenerationHeaders { get; }

        public GeneratorSettings(RenderMetadataAsAttributesType defaultRenderMetadataAsAttributes,
                                 EntityClassType defaultEntityClassType,
                                 bool enableNullableContext,
                                 bool createCodeGenerationHeaders)
        {
            DefaultRenderMetadataAsAttributes = defaultRenderMetadataAsAttributes;
            DefaultEntityClassType = defaultEntityClassType;
            EnableNullableContext = enableNullableContext;
            CreateCodeGenerationHeaders = createCodeGenerationHeaders;
        }
    }
}
