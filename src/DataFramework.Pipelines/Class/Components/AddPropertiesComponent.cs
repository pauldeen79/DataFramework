namespace DataFramework.Pipelines.Class.Components;

public class AddPropertiesComponent : IPipelineComponent<ClassContext>
{
    public Task<Result> ProcessAsync(PipelineContext<ClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddProperties(context.Request.SourceModel.Fields.Select(field =>
                new PropertyBuilder()
                    .WithName(field.CreatePropertyName(context.Request.SourceModel))
                    .Fill(field)
                    .WithHasSetter(!field.IsComputed && field.CanSet && context.Request.Settings.EntityClassType.HasPropertySetter())
                    .AddAttributes(field.GetEntityClassPropertyAttributes(context.Request.SourceModel.Name, context.Request.Settings.AddComponentModelAttributes))
                    .AddGetterCodeStatements(field.GetterCodeStatements.Select(x => x.ToBuilder()))))
            .AddProperties(context.Request.SourceModel.GetUpdateConcurrencyCheckFields(context.Request.Settings.ConcurrencyCheckBehavior).Select(field =>
                new PropertyBuilder()
                    .WithName($"{field.Name}Original")
                    .Fill(field)
                    .WithIsNullable()
                    .WithHasSetter(context.Request.Settings.EntityClassType.HasPropertySetter())
                    .AddAttributes(context.Request.Settings.EntityClassType.IsImmutable()
                        ? Enumerable.Empty<AttributeBuilder>()
                        : [new AttributeBuilder().AddNameAndParameter(typeof(ReadOnlyAttribute).FullName.ReplaceSuffix(nameof(System.Attribute), string.Empty, StringComparison.Ordinal), true)])));

        return Task.FromResult(Result.Continue());
    }
}
