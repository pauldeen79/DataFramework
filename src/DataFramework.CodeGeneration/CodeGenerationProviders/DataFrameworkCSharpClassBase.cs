﻿namespace DataFramework.CodeGeneration.CodeGenerationProviders;

public abstract class DataFrameworkCSharpClassBase : CSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string DefaultFileName => string.Empty; // not used because we're using multiple files, but it's abstract so we need to fill ilt

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type RecordConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override string ProjectName => "DataFramework";
    protected override bool AddBackingFieldsForCollectionProperties => false; // I might want to set this to true, but this gives compilation errors in generated code of base class :(
    protected override bool AddPrivateSetters => false;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Shared;
    protected override string FileNameSuffix => ".generated";
    protected override bool InheritFromInterfaces => true;

    protected override string FormatInstanceTypeName(ITypeBase instance, bool forCreate)
    {
        if (instance.Namespace == "DataFramework.Core")
        {
            return forCreate
                ? "DataFramework.Core." + instance.Name
                : "DataFramework.Abstractions.I" + instance.Name;
        }

        return string.Empty;
    }

    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        var propertyName = property.Name.ToString();

        if (typeName.StartsWith("DataFramework.Abstractions.I", StringComparison.InvariantCulture))
        {
            property.ConvertSinglePropertyToBuilderOnBuilder
            (
                typeName.Replace("Abstractions.I", "Core.Builders.") + "Builder"
            );
        }
        else if (typeName.Contains("Collection<DataFramework."))
        {
            property.ConvertCollectionPropertyToBuilderOnBuilder
            (
                false,
                typeof(ValueCollection<>).WithoutGenerics(),
                typeName.Replace("Abstractions.I", "Core.Builders.").ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture),
                null
            );
        }
        else if (typeName.IsBooleanTypeName() || typeName.IsNullableBooleanTypeName())
        {
            property.SetDefaultArgumentValueForWithMethod(true);
            if (_propertiesWithDefaultValueTrue.Contains(propertyName))
            {
                property.SetDefaultValueForBuilderClassConstructor(new ModelFramework.Common.Literal("true"));
            }
        }
        else if (typeName.IsStringTypeName())
        {
            property.ConvertStringPropertyToStringBuilderPropertyOnBuilder(UseLazyInitialization);
        }

        if (propertyName == nameof(ITypeContainer.TypeName) && typeName.IsStringTypeName())
        {
            property.AddBuilderOverload(new Overload("WithType", "With{2}(type?.AssemblyQualifiedName!);", new[] { new Parameter(false, false, false, typeof(Type).FullName!, false, false, Enumerable.Empty<IAttribute>(), Enumerable.Empty<ModelFramework.Common.Contracts.IMetadata>(), "type", null) }));
        }

        if (propertyName == nameof(IMetadataContainer.Metadata) && typeName.GetGenericArguments().GetClassName() == nameof(Abstractions.IMetadata))
        {
            property.AddBuilderOverload(new OverloadBuilder()
                .AddParameter("name", typeof(string))
                .AddParameter("value", typeof(object), true)
                .WithInitializeExpression("Add{4}(new MetadataBuilder().WithName(name).WithValue(value));")
                .Build());
        }
    }

    protected static Type[] GetDataFrameworkModelTypes()
        => new[]
        {
            typeof(IDataObjectInfo),
            typeof(IFieldInfo),
            typeof(Abstractions.IMetadata)
        };

    private static readonly string[] _propertiesWithDefaultValueTrue = new[]
    {
        nameof(IFieldInfo.IsVisible),
        nameof(IFieldInfo.IsPersistable),
        nameof(IFieldInfo.CanGet),
        nameof(IFieldInfo.CanSet),
        nameof(IDataObjectInfo.IsQueryable),
        nameof(IDataObjectInfo.IsVisible)
    };
}
