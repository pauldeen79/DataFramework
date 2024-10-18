namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class AddQueryFieldInfoMembersComponentBuilder : IQueryFieldInfoComponentBuilder
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddQueryFieldInfoMembersComponentBuilder(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public IPipelineComponent<QueryFieldInfoContext> Build()
        => new AddQueryFieldInfoMembersComponent(_csharpExpressionDumper);
}

public class AddQueryFieldInfoMembersComponent : IPipelineComponent<QueryFieldInfoContext>
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddQueryFieldInfoMembersComponent(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public Task<Result> Process(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(typeof(IQueryFieldInfo))
            .AddFields(GetQueryFieldProviderClassFields(context))
            .AddConstructors(GetQueryFieldProviderClassConstructors(context))
            .AddMethods(GetQueryFieldProviderClassMethods(context));

        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<FieldBuilder> GetQueryFieldProviderClassFields(PipelineContext<QueryFieldInfoContext> context)
        => context.Request.Settings.QueryFieldInfoFields.Select(x => x.ToBuilder());

    private static IEnumerable<ConstructorBuilder> GetQueryFieldProviderClassConstructors(PipelineContext<QueryFieldInfoContext> context)
    {
        var constructorParameters = context.Request.Settings.QueryFieldInfoConstructorParameters.Select(x => x.ToBuilder()).ToArray();
        var constructorStatements = context.Request.Settings.QueryFieldInfoConstructorCodeStatements.Select(x => x.ToBuilder()).ToArray();
        if (constructorParameters.Length > 0 || constructorStatements.Length > 0)
        {
            yield return new ConstructorBuilder()
                .AddParameters(constructorParameters)
                .AddCodeStatements(constructorStatements);
        }
    }

    private IEnumerable<MethodBuilder> GetQueryFieldProviderClassMethods(PipelineContext<QueryFieldInfoContext> context)
    {
        yield return new MethodBuilder()
            .WithName(nameof(IQueryFieldInfo.GetAllFields))
            .WithReturnType(typeof(IEnumerable<string>))
            .AddStringCodeStatements(context.Request.SourceModel.Fields.Where(x => x.UseOnSelect).Select(x => $"yield return {_csharpExpressionDumper.Dump(x.CreatePropertyName(context.Request.SourceModel))};"))
            .AddCodeStatements(context.Request.Settings.QueryFieldInfoGetAllFieldsCodeStatements.Select(x => x.ToBuilder()));

        yield return new MethodBuilder()
            .WithName(nameof(IQueryFieldInfo.GetDatabaseFieldName))
            .AddParameter("queryFieldName", typeof(string))
            .WithReturnType(typeof(string))
            .WithReturnTypeIsNullable()
            .AddCodeStatements(context.Request.Settings.QueryFieldInfoGetDatabaseFieldNameCodeStatements.Select(x => x.ToBuilder()))
            .AddStringCodeStatements($"return GetAllFields().FirstOrDefault(x => x.Equals(queryFieldName, {nameof(StringComparison)}.{nameof(StringComparison.OrdinalIgnoreCase)}));");
    }
}
