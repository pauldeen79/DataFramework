﻿namespace DataFramework.Pipelines.EntityMapper.Components;

public class AddGeneratorAttributeComponentBuilder : IEntityMapperComponentBuilder
{
    public IPipelineComponent<EntityMapperContext> Build()
        => new AddGeneratorAttributeComponent();
}

public class AddGeneratorAttributeComponent : IPipelineComponent<EntityMapperContext>
{
    public Task<Result> Process(PipelineContext<EntityMapperContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddAttributes(new AttributeBuilder()
                .WithName(typeof(GeneratedCodeAttribute))
                .ForCodeGenerator("DataFramework.Pipelines.EntityMapperGenerator", Constants.Version)
            );

        return Task.FromResult(Result.Continue());
    }
}