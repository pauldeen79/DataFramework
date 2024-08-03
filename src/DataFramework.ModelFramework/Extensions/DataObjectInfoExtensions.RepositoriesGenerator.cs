namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToRepositoryClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToRepositoryClassBuilder(settings).BuildTyped();

    public static ClassBuilder ToRepositoryClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        => new ClassBuilder()
            .WithName($"{instance.Name}Repository")
            .WithNamespace(instance.GetRepositoriesNamespace())
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(Repositories.Visibility, () => instance.IsVisible.ToVisibility()))
            .WithBaseClass(typeof(Repository<,>).CreateGenericTypeName(instance.GetEntityFullName(), instance.GetEntityIdentityFullName()))
            .AddInterfaces(GetRepositoryClassInterfaces(instance))
            .AddMethods(GetRepositoryClassMethods(instance))
            .AddConstructors(GetRepositoryClassConstructors(instance))
            .AddAttributes(GetRepositoryClassAttributes(instance));

    private static IEnumerable<string> GetRepositoryClassInterfaces(IDataObjectInfo instance)
        => instance.Metadata
            .Where(md => md.Name == Repositories.Interfaces)
            .Select(md => md.Value.ToStringWithNullCheck().FixGenericParameter(instance.GetEntityFullName()))
            .Union([$"{instance.GetRepositoriesInterfaceNamespace()}.I{instance.Name}Repository"]);

    private static IEnumerable<ClassMethodBuilder> GetRepositoryClassMethods(IDataObjectInfo instance)
        => instance.Metadata.GetValues<IClassMethod>(Repositories.Method).Select(x => new ClassMethodBuilder(x));

    private static IEnumerable<ClassConstructorBuilder> GetRepositoryClassConstructors(IDataObjectInfo instance)
    {
        yield return new ClassConstructorBuilder()
            .AddParameter("commandProcessor", typeof(IDatabaseCommandProcessor<>).CreateGenericTypeName(instance.GetEntityFullName()))
            .AddParameter("entityRetriever", typeof(IDatabaseEntityRetriever<>).CreateGenericTypeName(instance.GetEntityFullName()))
            .AddParameter("identitySelectCommandProvider", typeof(IDatabaseCommandProvider<>).CreateGenericTypeName(instance.GetEntityFullName()))
            .AddParameter("pagedEntitySelectCommandProvider", typeof(IPagedDatabaseCommandProvider))
            .AddParameter("entitySelectCommandProvider", typeof(IDatabaseCommandProvider))
            .AddParameter("entityCommandProvider", typeof(IDatabaseCommandProvider<>).CreateGenericTypeName(instance.GetEntityFullName()))
            .ChainCallToBaseUsingParameters();
    }

    private static IEnumerable<AttributeBuilder> GetRepositoryClassAttributes(IDataObjectInfo instance)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Entities.RepositoryGenerator");

        foreach (var attribute in instance.Metadata.GetValues<IAttribute>(Repositories.Attribute))
        {
            yield return new AttributeBuilder(attribute);
        }
    }
}
