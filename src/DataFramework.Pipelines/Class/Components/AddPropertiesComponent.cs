﻿namespace DataFramework.Pipelines.Class.Components;

public class AddPropertiesComponentBuilder : IClassComponentBuilder
{
    public IPipelineComponent<ClassContext> Build()
        => new AddPropertiesComponent();
}

public class AddPropertiesComponent : IPipelineComponent<ClassContext>
{
    public Task<Result> Process(PipelineContext<ClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddProperties(context.Request.SourceModel.Fields.Select(field =>
                new PropertyBuilder()
                    .WithName(field.CreatePropertyName(context.Request.SourceModel))
                    .Fill(field)
                    .WithHasSetter(!field.IsComputed && field.CanSet && context.Request.Settings.EntityClassType.HasPropertySetter())
                    .AddAttributes(GetEntityClassPropertyAttributes(field, context.Request.SourceModel.Name, context.Request.Settings))
                    .AddGetterCodeStatements(GetGetterCodeStatements(context.Request.Settings, context.Request.SourceModel, field))))
            .AddProperties(context.Request.SourceModel.GetUpdateConcurrencyCheckFields(context.Request.Settings.ConcurrencyCheckBehavior).Select(field =>
                new PropertyBuilder()
                    .WithName($"{field.Name}Original")
                    .Fill(field)
                    .WithIsNullable()
                    .WithHasSetter(context.Request.Settings.EntityClassType.HasPropertySetter())
                    .AddAttributes(context.Request.Settings.EntityClassType.IsImmutable()
                        ? Enumerable.Empty<AttributeBuilder>()
                        : [new AttributeBuilder().AddNameAndParameter("System.ComponentModel.ReadOnly", true)])));

        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<AttributeBuilder> GetEntityClassPropertyAttributes(FieldInfo field, string instanceName, PipelineSettings settings)
    {
        if (settings.AddComponentModelAttributes && string.IsNullOrEmpty(field.DisplayName) && field.Name == instanceName)
        {
            //if the field name is equal to the DataObjectInstance name, then the property will be renamed to keep the C# compiler happy.
            //in this case, we would like to add a DisplayName attribute, so the property looks right in the UI. (PropertyGrid etc.)
            yield return new AttributeBuilder().AddNameAndParameter("System.ComponentModel.DataAnnotations.DisplayName", field.Name);
        }
    }

    private static IEnumerable<CodeStatementBaseBuilder> GetGetterCodeStatements(PipelineSettings settings, DataObjectInfo dataObjectInfo, FieldInfo field)
        => settings.CodeStatementMappings
            .Where(x => AreEqual(dataObjectInfo, x.SourceDataObjectInfo) && AreEqual(field, x.SourceFieldInfo))
            .SelectMany(x => x.CodeStatements);

    private static bool AreEqual(FieldInfo field, FieldInfo sourceFieldInfo)
        => field.Name == sourceFieldInfo.Name;

    private static bool AreEqual(DataObjectInfo dataObjectInfo, DataObjectInfo sourceDataObjectInfo)
        => dataObjectInfo.Name == sourceDataObjectInfo.Name
            && dataObjectInfo.TypeName == sourceDataObjectInfo.TypeName;
}