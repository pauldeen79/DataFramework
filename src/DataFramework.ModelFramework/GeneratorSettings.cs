namespace DataFramework.ModelFramework
{
    public record GeneratorSettings
    {
        public static readonly GeneratorSettings Default = new GeneratorSettings(RenderMetadataAsAttributesType.Validation, EntityClassType.Poco);

        public RenderMetadataAsAttributesType DefaultRenderMetadataAsAttributes { get; }
        public EntityClassType DefaultEntityClassType { get; }

        public GeneratorSettings(RenderMetadataAsAttributesType defaultRenderMetadataAsAttributes,
                                 EntityClassType defaultEntityClassType)
        {
            DefaultRenderMetadataAsAttributes = defaultRenderMetadataAsAttributes;
            DefaultEntityClassType = defaultEntityClassType;
        }
    }
}
