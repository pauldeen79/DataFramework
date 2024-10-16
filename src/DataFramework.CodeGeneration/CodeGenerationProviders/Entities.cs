﻿namespace DataFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Entities : DataFrameworkCSharpClassBase
{
    public Entities(IPipelineService pipelineService) : base(pipelineService)
    {
    }

    public override string Path => "DataFramework.Domain";

    public override async Task<IEnumerable<TypeBase>> GetModel()
        => await GetEntities(await GetCoreModels(), "DataFramework.Domain");
}
