namespace DataFramework.Pipelines.EntityMapper.Components;

public class AddEntityMapperMembersComponent : IPipelineComponent<EntityMapperContext>
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddEntityMapperMembersComponent(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public Task<Result> ProcessAsync(PipelineContext<EntityMapperContext> context, CancellationToken token)
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

        var fields = instance.Fields.Where(x => x.UseOnSelect && !instance.CustomEntityMappings.Any(y => y.PropertyName == x.Name)).ToArray();
        var totalLength = fields.Length + instance.CustomEntityMappings.Count;
        var counter = 0;

        foreach (var field in fields)
        {
            counter++;
            var comma = counter < totalLength
                ? "," 
                : string.Empty;

            yield return entityClassType.IsImmutable()
                ? $"    {field.Name.ToCamelCase(cultureInfo)}: {typeof(CrossCutting.Data.Sql.Extensions.DataReaderExtensions).FullName}.{field.SqlReaderMethodName}(reader, \"{field.GetDatabaseFieldName()}\"){comma}"
                : $"    {field.Name} = {typeof(CrossCutting.Data.Sql.Extensions.DataReaderExtensions).FullName}.{field.SqlReaderMethodName}(reader, \"{field.GetDatabaseFieldName()}\"){comma}";
        }

        foreach (var keyValuePair in instance.CustomEntityMappings)
        {
            counter++;
            var comma = counter < totalLength
                ? ","
                : string.Empty;

            yield return entityClassType.IsImmutable()
                ? $"    {keyValuePair.PropertyName.ToCamelCase(cultureInfo)}: {_csharpExpressionDumper.Dump(keyValuePair.Mapping)}{comma}"
                : $"    {keyValuePair.PropertyName} = {_csharpExpressionDumper.Dump(keyValuePair.Mapping)}{comma}";
        }

        yield return entityClassType.IsImmutable()
            ? ");"
            : "};";
    }
}
