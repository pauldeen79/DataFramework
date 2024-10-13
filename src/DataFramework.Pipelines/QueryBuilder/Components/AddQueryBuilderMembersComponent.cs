﻿namespace DataFramework.Pipelines.QueryBuilder.Components;

public class AddQueryBuilderMembersComponentBuilder : IQueryBuilderComponentBuilder
{
    public IPipelineComponent<QueryBuilderContext> Build()
        => new AddQueryBuilderMembersComponent();
}

public class AddQueryBuilderMembersComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> Process(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        var queryFullName = context.Request.SourceModel.GetQueryFullName(context.Request.Settings.QueryBuilderNamespace);
        var nullableSuffix = context.Request.Settings.EnableNullableContext
            ? "?"
            : string.Empty;

        context.Request.Builder
            .AddConstructors(
                new ConstructorBuilder().WithChainCall("base()"),
                new ConstructorBuilder().AddParameter("source", typeof(IQuery)).ChainCallToBaseUsingParameters()
            )
            .AddMethods(
                new MethodBuilder().WithName(nameof(IQueryBuilder.Build)).WithOverride().WithReturnType(typeof(IQuery)).AddStringCodeStatements("return BuildTyped();"),
                new MethodBuilder().WithName("BuildTyped").WithReturnTypeName(queryFullName).AddStringCodeStatements($"return new {queryFullName}(Limit, Offset, Filter{nullableSuffix}.BuildTyped(), OrderByFields{nullableSuffix}.Select(x => x.Build()));")
            )
            .WithBaseClass(typeof(QueryFramework.Core.Builders.QueryBuilder));

        return Task.FromResult(Result.Continue());
    }
}
