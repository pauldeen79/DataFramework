namespace DataFramework.Pipelines.IdentityCommandProvider.Components;

public class AddIdentityCommandProviderMembersComponentBuilder : IIdentityCommandProviderComponentBuilder
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddIdentityCommandProviderMembersComponentBuilder(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public IPipelineComponent<IdentityCommandProviderContext> Build()
        => new AddIdentityCommandProviderMembersComponent(_csharpExpressionDumper);
}

public class AddIdentityCommandProviderMembersComponent : IPipelineComponent<IdentityCommandProviderContext>
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddIdentityCommandProviderMembersComponent(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public Task<Result> Process(PipelineContext<IdentityCommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithBaseClass(typeof(IdentityDatabaseCommandProviderBase<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace)))
            .AddConstructors(new ConstructorBuilder().WithChainCall($"base(new {context.Request.SourceModel.GetEntityRetrieverFullName(context.Request.Settings.EntityRetrieverNamespace)}())"))
            .AddMethods(new MethodBuilder()
                .WithName("GetFields")
                .WithProtected()
                .WithOverride()
                .WithVisibility(Visibility.Private)
                .WithReturnType(typeof(IEnumerable<IdentityDatabaseCommandProviderField>))
                .AddParameter("source", context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace))
                .AddParameter("operation", typeof(DatabaseOperation))
                .AddStringCodeStatements(context.Request.SourceModel.GetIdentityFields().Select(x => $"yield return new {nameof(IdentityDatabaseCommandProviderField)}({_csharpExpressionDumper.Dump(x.CreatePropertyName(context.Request.SourceModel))}, {_csharpExpressionDumper.Dump(x.GetDatabaseFieldName())});")));

        return Task.FromResult(Result.Continue());
    }    
}
