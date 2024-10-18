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
                    .AddAttributes(field.GetEntityClassPropertyAttributes(context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace).GetClassName(), context.Request.Settings.AddComponentModelAttributes))
                    .AddGetterCodeStatements(field.GetterCodeStatements.Select(x => x.ToBuilder()))));

        return Task.FromResult(Result.Continue());
    }
}
