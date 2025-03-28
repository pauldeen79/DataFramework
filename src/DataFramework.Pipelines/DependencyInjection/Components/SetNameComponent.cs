﻿namespace DataFramework.Pipelines.DependencyInjection.Components;

public class SetNameComponent : IPipelineComponent<DependencyInjectionContext>
{
    private readonly IFormattableStringParser _formattableStringParser;

    public SetNameComponent(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public Task<Result> ProcessAsync(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        var result = _formattableStringParser.Parse(context.Request.Settings.DependencyInjectionClassName, context.Request.FormatProvider, context.Request);
        if (!result.IsSuccessful())
        {
            return Task.FromResult((Result)result);
        }

        context.Request.Builder
            .WithName(result.Value!.ToString())
            .WithNamespace(context.Request.Settings.DependencyInjectionNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
