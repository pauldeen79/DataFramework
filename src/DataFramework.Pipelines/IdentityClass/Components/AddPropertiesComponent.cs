namespace DataFramework.Pipelines.IdentityClass.Components;

public class AddPropertiesComponentBuilder : IIdentityClassComponentBuilder
{
    public IPipelineComponent<IdentityClassContext> Build()
        => new AddPropertiesComponent();
}

public class AddPropertiesComponent : IPipelineComponent<IdentityClassContext>
{
    public Task<Result> Process(PipelineContext<IdentityClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddProperties(context.Request.SourceModel.GetIdentityFields().Select(field =>
                new PropertyBuilder()
                    .WithName(field.CreatePropertyName(context.Request.SourceModel))
                    .Fill(field)
                    .WithHasSetter(!field.IsComputed && field.CanSet && context.Request.Settings.EntityClassType.HasPropertySetter())
                    .AddAttributes(GetEntityClassPropertyAttributes(field, context.Request.SourceModel.Name, context.Request.Settings))
                    .AddGetterCodeStatements(context.Request.GetGetterCodeStatements(context.Request.SourceModel, field))));

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
}
