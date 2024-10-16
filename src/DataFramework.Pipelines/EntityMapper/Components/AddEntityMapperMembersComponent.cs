namespace DataFramework.Pipelines.EntityMapper.Components;

public class AddEntityMapperMembersComponentBuilder : IEntityMapperComponentBuilder
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddEntityMapperMembersComponentBuilder(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public IPipelineComponent<EntityMapperContext> Build()
        => new AddEntityMapperMembersComponent(_csharpExpressionDumper);
}

public class AddEntityMapperMembersComponent : IPipelineComponent<EntityMapperContext>
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddEntityMapperMembersComponent(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public Task<Result> Process(PipelineContext<EntityMapperContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(typeof(IDatabaseEntityMapper<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)))
            .AddMethods(GetEntityMapperClassMethods(context.Request.SourceModel, context.Request.Settings.EntityClassType, context.Request.Settings.DefaultEntityNamespace, context.Request.FormatProvider.ToCultureInfo()));

        return Task.FromResult(Result.Continue());
    }

    private IEnumerable<MethodBuilder> GetEntityMapperClassMethods(DataObjectInfo instance, EntityClassType entityClassType, string entitiesNamespace, CultureInfo cultureInfo)
    {
        yield return new MethodBuilder()
            .WithName(nameof(IDatabaseEntityMapper<object>.Map))
            .WithReturnTypeName(instance.GetEntityFullName(entitiesNamespace))
            .AddParameter("reader", typeof(IDataReader))
            .AddStringCodeStatements(GetEntityInitializationCode(instance, entityClassType, entitiesNamespace, cultureInfo));
    }

    private IEnumerable<string> GetEntityInitializationCode(DataObjectInfo instance, EntityClassType entityClassType, string entitiesNamespace, CultureInfo cultureInfo)
    {
        yield return $"return new {instance.GetEntityFullName(entitiesNamespace)}";

        yield return entityClassType.IsImmutable()
            ? "("
            : "{";

        var fields = instance.Fields.Where(x => x.UseOnSelect).ToArray();
        foreach (var field in fields.Select((item, index) => new { item, index }))
        {
            var comma = field.index + 1 < fields.Length
                ? "," 
                : string.Empty;

            yield return entityClassType.IsImmutable()
                ? $"    {field.item.Name.ToPascalCase(cultureInfo)}: {typeof(CrossCutting.Data.Sql.Extensions.DataReaderExtensions).FullName}.{field.item.SqlReaderMethodName}(reader, \"{field.item.GetDatabaseFieldName()}\"){comma}"
                : $"    {field.item.Name} = {typeof(CrossCutting.Data.Sql.Extensions.DataReaderExtensions).FullName}.{field.item.SqlReaderMethodName}(reader, \"{field.item.GetDatabaseFieldName()}\"){comma}";
        }

        foreach (var keyValuePair in instance.CustomEntityMappings.Select((item, index) => new { item, index }))
        {
            var comma = keyValuePair.index + 1 < instance.CustomEntityMappings.Count
                ? ","
                : string.Empty;

            yield return entityClassType.IsImmutable()
                ? $"    {keyValuePair.item.PropertyName.ToPascalCase(cultureInfo)}: {_csharpExpressionDumper.Dump(keyValuePair.item.Mapping)}{comma}"
                : $"    {keyValuePair.item.PropertyName} = {_csharpExpressionDumper.Dump(keyValuePair.item.Mapping)}{comma}";
        }

        yield return entityClassType.IsImmutable()
            ? ");"
            : "};";
    }
}
