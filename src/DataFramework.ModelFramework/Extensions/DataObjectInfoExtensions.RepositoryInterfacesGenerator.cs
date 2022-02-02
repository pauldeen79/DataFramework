namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IInterface ToRepositoryInterface(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToRepositoryInterfaceBuilder(settings).Build();

    public static InterfaceBuilder ToRepositoryInterfaceBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        => new InterfaceBuilder()
            .WithName($"I{instance.Name}Repository")
            .WithNamespace(instance.GetRepositoriesInterfaceNamespace())
            .WithPartial()
            .WithVisibility(instance.Metadata.GetValue(Repositories.Visibility, () => instance.IsVisible.ToVisibility()))
            .AddMetadata(instance.Metadata.Convert().Select(x => new global::ModelFramework.Common.Builders.MetadataBuilder(x)))
            .AddInterfaces(GetRepositoryInterfaceInterfaces(instance))
            .AddMethods(GetRepositoryClassMethods(instance))
            .AddAttributes(GetRepositoryInterfaceAttributes());

    private static IEnumerable<string> GetRepositoryInterfaceInterfaces(IDataObjectInfo instance)
    {
        yield return $"CrossCutting.Data.Abstractions.IRepository<{instance.GetEntityFullName()}, {instance.GetEntityIdentityFullName()}>";
    }

    private static IEnumerable<AttributeBuilder> GetRepositoryInterfaceAttributes()
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Entities.RepositoryGenerator");
    }
}
