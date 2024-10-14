namespace DataFramework.Pipelines.QueryFieldInfoProvider.Components;

public class AddQueryFieldInfoProviderMembersComponentBuilder : IQueryFieldInfoProviderComponentBuilder
{
    public IPipelineComponent<QueryFieldInfoProviderContext> Build()
        => new AddQueryFieldInfoMembersComponent();
}

public class AddQueryFieldInfoMembersComponent : IPipelineComponent<QueryFieldInfoProviderContext>
{
    public Task<Result> Process(PipelineContext<QueryFieldInfoProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        var queryFullName = context.Request.SourceModel.GetQueryFullName(context.Request.Settings.QueryNamespace);
        var queryFieldInfoFullName = context.Request.SourceModel.GetQueryFieldInfoFullName(context.Request.Settings.QueryFieldInfoNamespace);

        context.Request.Builder
            .AddInterfaces(typeof(IQueryFieldInfoProvider))
            .AddMethods(new MethodBuilder()
                .WithName(nameof(IQueryFieldInfoProvider.TryCreate))
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
