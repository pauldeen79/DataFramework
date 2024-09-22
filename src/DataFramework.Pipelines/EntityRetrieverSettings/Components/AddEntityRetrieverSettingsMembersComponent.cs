namespace DataFramework.Pipelines.EntityRetrieverSettings.Components;

public class AddEntityRetrieverSettingsMembersComponentBuilder : IEntityRetrieverSettingsComponentBuilder
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddEntityRetrieverSettingsMembersComponentBuilder(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public IPipelineComponent<EntityRetrieverSettingsContext> Build()
        => new AddEntityRetrieverSettingsMembersComponent(_csharpExpressionDumper);
}

public class AddEntityRetrieverSettingsMembersComponent : IPipelineComponent<EntityRetrieverSettingsContext>
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddEntityRetrieverSettingsMembersComponent(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public Task<Result> Process(PipelineContext<EntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(typeof(IDatabaseEntityRetrieverSettings))
            .AddProperties(GetEntityRetrieverSettingsClassProperties(context));

        return Task.FromResult(Result.Continue());
    }

    private IEnumerable<PropertyBuilder> GetEntityRetrieverSettingsClassProperties(PipelineContext<EntityRetrieverSettingsContext> context)
    {
        yield return new PropertyBuilder()
            .WithName(nameof(IDatabaseEntityRetrieverSettings.TableName))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements($"return {context.Request.SourceModel.GetDatabaseTableName()};");

        yield return new PropertyBuilder()
            .WithName(nameof(IDatabaseEntityRetrieverSettings.Fields))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements(string.Concat
            (
                "return ",
                string.Join(", ", _csharpExpressionDumper.Dump(context.Request.SourceModel.Fields.Where(x => x.UseOnSelect).Select(x => x.GetDatabaseFieldName())).Replace(Environment.NewLine, Environment.NewLine + "                ")),
                ";"
            ));

        yield return new PropertyBuilder()
            .WithName(nameof(IDatabaseEntityRetrieverSettings.DefaultOrderBy))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements($"return {_csharpExpressionDumper.Dump(context.Request.SourceModel.DefaultOrderByFields)};");

        yield return new PropertyBuilder()
            .WithName(nameof(IDatabaseEntityRetrieverSettings.DefaultWhere))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements($"return {_csharpExpressionDumper.Dump(context.Request.SourceModel.DefaultWhereClause)};");
    }
}
