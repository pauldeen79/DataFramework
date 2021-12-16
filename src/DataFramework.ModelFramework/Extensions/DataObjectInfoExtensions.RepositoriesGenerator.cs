using System.Collections.Generic;
using System.Linq;
using CrossCutting.Data.Abstractions;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static IClass ToRepositoryClass(this IDataObjectInfo instance, GeneratorSettings settings)
            => instance.ToRepositoryClassBuilder(settings).Build();

        public static ClassBuilder ToRepositoryClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
            => new ClassBuilder()
                .WithName($"{instance.Name}Repository")
                .WithNamespace(instance.GetRepositoriesNamespace())
                .WithPartial()
                .WithVisibility(instance.Metadata.GetValue(Repositories.Visibility, () => instance.IsVisible.ToVisibility()))
                .WithBaseClass($"CrossCutting.Data.Core.Repository<{instance.GetEntityFullName()}, {instance.GetEntityIdentityFullName()}>")
                .AddMetadata(instance.Metadata.Convert())
                .AddInterfaces(GetRepositoryClassInterfaces(instance))
                .AddMethods(GetRepositoryClassMethods(instance))
                .AddConstructors(GetRepositoryClassConstructors(instance))
                .AddAttributes(GetRepositoryClassAttributes(instance));

        private static IEnumerable<string> GetRepositoryClassInterfaces(IDataObjectInfo instance)
            => instance.Metadata
                .Where(md => md.Name == Repositories.Interfaces)
                .Select(md => md.Value.ToStringWithNullCheck().FixGenericParameter(instance.GetEntityFullName()))
                .Union(new[] { $"{instance.GetRepositoriesInterfaceNamespace()}.I{instance.Name}Repository" });

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
                .WithChainCall("base(commandProcessor, entityRetriever, identitySelectCommandProvider, pagedEntitySelectCommandProvider, entitySelectCommandProvider, entityCommandProvider)");
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
}
