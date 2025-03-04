﻿namespace DataFramework.Pipelines.PagedEntityRetrieverSettings.Components;

public class AddPagedEntityRetrieverSettingsMembersComponent : IPipelineComponent<PagedEntityRetrieverSettingsContext>
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddPagedEntityRetrieverSettingsMembersComponent(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public Task<Result> ProcessAsync(PipelineContext<PagedEntityRetrieverSettingsContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(typeof(IPagedDatabaseEntityRetrieverSettings))
            .AddProperties(GetPagedEntityRetrieverSettingsClassProperties(context));

        return Task.FromResult(Result.Continue());
    }

    private IEnumerable<PropertyBuilder> GetPagedEntityRetrieverSettingsClassProperties(PipelineContext<PagedEntityRetrieverSettingsContext> context)
    {
        yield return new PropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.TableName))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements($"return {_csharpExpressionDumper.Dump(context.Request.SourceModel.GetDatabaseTableName())};");

        yield return new PropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.Fields))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements(string.Concat
            (
                "return ",
                _csharpExpressionDumper.Dump(string.Join(", ", context.Request.SourceModel.Fields.Where(x => x.UseOnSelect).Select(x => x.GetDatabaseFieldName())).Replace(Environment.NewLine, Environment.NewLine + "                ")),
                ";"
            ));

        yield return new PropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.DefaultOrderBy))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements($"return {_csharpExpressionDumper.Dump(context.Request.SourceModel.DefaultOrderByFields).Transform(x => x == "null" ? "string.Empty" : x)};");

        yield return new PropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.DefaultWhere))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements($"return {_csharpExpressionDumper.Dump(context.Request.SourceModel.DefaultWhereClause).Transform(x => x == "null" ? "string.Empty" : x)};");

        yield return new PropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.OverridePageSize))
            .WithType(typeof(int?))
            .WithHasSetter(false)
            .AddGetterStringCodeStatements($"return {context.Request.SourceModel.OverridePageSize ?? -1};");
    }
}
