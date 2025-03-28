﻿namespace DataFramework.Pipelines.QueryFieldInfoProvider.Components;

public class AddQueryFieldInfoMembersComponent : IPipelineComponent<QueryFieldInfoProviderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<QueryFieldInfoProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        var queryFullName = context.Request.SourceModel.GetQueryFullName(context.Request.Settings.QueryNamespace);
        var queryFieldInfoFullName = context.Request.SourceModel.GetQueryFieldInfoFullName(context.Request.Settings.QueryFieldInfoNamespace);
        //IQuery query, out IQueryFieldInfo? result
        context.Request.Builder
            .AddInterfaces(typeof(IQueryFieldInfoProvider))
            .AddMethods(new MethodBuilder()
                .WithName(nameof(IQueryFieldInfoProvider.TryCreate))
                .AddParameters(
                    new ParameterBuilder().WithName("query").WithType(typeof(IQuery)),
                    new ParameterBuilder().WithName("result").WithType(typeof(IQueryFieldInfo)).WithIsNullable().WithIsOut()
                )
                .WithReturnType(typeof(bool))
                .AddStringCodeStatements(
                    $"if (query is {queryFullName})",
                    "{",
                    $"    result = new {queryFieldInfoFullName}();",
                    "    return true;",
                    "}",
                    "result = default;",
                    "return false;"
                )
            );

        return Task.FromResult(Result.Continue());
    }
}
