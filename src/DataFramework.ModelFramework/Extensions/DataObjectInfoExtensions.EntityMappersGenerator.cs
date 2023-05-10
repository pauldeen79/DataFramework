namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToEntityMapperClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToEntityMapperClassBuilder(settings).BuildTyped();

    public static ClassBuilder ToEntityMapperClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        => new ClassBuilder()
            .WithName($"{instance.Name}EntityMapper")
            .WithNamespace(instance.GetEntityMapperNamespace())
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(EntityMappers.Visibility, () => instance.IsVisible.ToVisibility()))
            .AddInterfaces(typeof(IDatabaseEntityMapper<>).CreateGenericTypeName(instance.GetEntityFullName()))
            .AddAttributes(GetEntityMapperClassAttributes(instance))
            .AddMethods(GetEntityMapperClassMethods(instance, instance.GetEntityClassType(settings.DefaultEntityClassType)));

    private static IEnumerable<AttributeBuilder> GetEntityMapperClassAttributes(IDataObjectInfo instance)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.EntityMapperGenerator");

        foreach (var attribute in instance.Metadata.GetValues<IAttribute>(EntityMappers.Attribute))
        {
            yield return new AttributeBuilder(attribute);
        }
    }

    private static IEnumerable<ClassMethodBuilder> GetEntityMapperClassMethods(IDataObjectInfo instance, EntityClassType entityClassType)
    {
        yield return new ClassMethodBuilder()
            .WithName(nameof(IDatabaseEntityMapper<object>.Map))
            .WithTypeName(instance.GetEntityFullName())
            .AddParameter("reader", typeof(IDataReader))
            .AddLiteralCodeStatements(GetEntityInitializationCode(instance, entityClassType));
    }

    private static IEnumerable<string> GetEntityInitializationCode(IDataObjectInfo instance, EntityClassType entityClassType)
    {
        yield return $"return new {instance.GetEntityFullName()}";
        yield return entityClassType.IsImmutable()
            ? "("
            : "{";
        foreach (var field in instance.Fields.Where(x => x.UseOnSelect()))
        {
            yield return entityClassType.IsImmutable()
                ? $"    {field.Name.ToPascalCase()}: reader.{field.GetSqlReaderMethodName()}(\"{field.GetDatabaseFieldName()}\"),"
                : $"    {field.Name} = reader.{field.GetSqlReaderMethodName()}(\"{field.GetDatabaseFieldName()}\"),";
        }
        foreach (var keyValuePair in instance.Metadata.GetValues<KeyValuePair<string, object>>(EntityMappers.CustomMapping))
        {
            yield return entityClassType.IsImmutable()
                ? $"    {keyValuePair.Key.ToPascalCase()}: {keyValuePair.Value.CsharpFormat()},"
                : $"    {keyValuePair.Key} = {keyValuePair.Value.CsharpFormat()},";
        }
        yield return entityClassType.IsImmutable()
            ? ");"
            : "};";
    }
}
