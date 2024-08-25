namespace DataFramework.Pipelines.DependencyInjection.Components;

public class AddDependencyInjectionMembersComponentBuilder : IDependencyInjectionComponentBuilder
{
    private readonly IFormattableStringParser _formattableStringParser;

    public AddDependencyInjectionMembersComponentBuilder(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public IPipelineComponent<DependencyInjectionContext> Build()
        => new AddDependencyInjectionMembersComponent(_formattableStringParser);
}

public class AddDependencyInjectionMembersComponent : IPipelineComponent<DependencyInjectionContext>
{
    private readonly IFormattableStringParser _formattableStringParser;

    public AddDependencyInjectionMembersComponent(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public Task<Result> Process(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        var result = _formattableStringParser.Parse(context.Request.Settings.DependencyInjectionMethodName, context.Request.FormatProvider, context.Request);
        if (!result.IsSuccessful())
        {
            return Task.FromResult((Result)result);
        }

        context.Request.Builder
            .AddMethods(new MethodBuilder()
                .WithName(result.Value!.ToString())
                .WithVisibility(context.Request.Settings.DependencyInjectionVisibility)
                .WithStatic()
                .WithExtensionMethod()
                .AddParameter("serviceCollection", typeof(IServiceCollection))
            );

        return Task.FromResult(Result.Continue());
    }
}
