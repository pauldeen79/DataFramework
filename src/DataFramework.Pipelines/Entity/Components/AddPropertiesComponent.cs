namespace DataFramework.Pipelines.Entity.Components;

public class AddPropertiesComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new AddPropertiesComponent();
}

public class AddPropertiesComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        var cultureInfo = context.Request.FormatProvider.ToCultureInfo();

        context.Request.Builder
            .AddProperties(context.Request.SourceModel.Fields.Select(field =>
                new PropertyBuilder()
                    .WithName(field.CreatePropertyName(context.Request.SourceModel))
                    .Fill(field)
                    .WithHasSetter(!field.IsComputed && field.CanSet && context.Request.Settings.EntityClassType.HasPropertySetter())
                    .AddAttributes(GetEntityClassPropertyAttributes(field, context.Request.SourceModel.Name, context.Request.Settings))
                    .AddGetterCodeStatements(GetGetterCodeStatements(field, context.Request.Settings.EntityClassType, cultureInfo))
                    .AddSetterStringCodeStatements(GetSetterCodeStatements(field, context.Request.Settings.EntityClassType, cultureInfo))))
            .AddProperties(context.Request.SourceModel.GetUpdateConcurrencyCheckFields(context.Request.Settings.ConcurrencyCheckBehavior).Select(field =>
                new PropertyBuilder()
                    .WithName($"{field.Name}Original")
                    .Fill(field)
                    .WithIsNullable()
                    .WithHasSetter(context.Request.Settings.EntityClassType.HasPropertySetter())
                    .AddAttributes(context.Request.Settings.EntityClassType.IsImmutable()
                        ? Enumerable.Empty<AttributeBuilder>()
                        : [new AttributeBuilder().AddNameAndParameter("System.ComponentModel.ReadOnly", true)])
                    .AddGetterStringCodeStatements(GetGetterCodeStatementsForOriginal(field, context.Request.Settings.EntityClassType, cultureInfo))
                    .AddSetterStringCodeStatements(GetSetterCodeStatementsForOriginal(field, context.Request.Settings.EntityClassType, cultureInfo))));

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

    private static List<CodeStatementBaseBuilder> GetGetterCodeStatements(FieldInfo field, EntityClassType entityClassType, CultureInfo cultureInfo)
    {
        //var statements = field.Metadata.GetValues<ICodeStatement>(Entities.ComputedFieldStatement).Select(x => x.CreateBuilder()).ToList();
        var statements = new List<CodeStatementBaseBuilder>();

        if (statements.Count == 0 && entityClassType == EntityClassType.ObservablePoco)
        {
            statements.Add(new StringCodeStatementBuilder().WithStatement($"return _{field.Name.Sanitize().ToPascalCase(cultureInfo)};"));
        }
        return statements;
    }

    private static IEnumerable<string> GetSetterCodeStatements(FieldInfo field, EntityClassType entityClassType, CultureInfo cultureInfo)
    {
        if (entityClassType == EntityClassType.ObservablePoco)
        {
            yield return $"_{field.Name.Sanitize().ToPascalCase(cultureInfo)} = value;";
            yield return string.Format(@$"if (PropertyChanged is not null) PropertyChanged(this, new {typeof(PropertyChangedEventArgs).FullName}(""{0}""));", field.Name.Sanitize());
        }
    }

    private static IEnumerable<string> GetGetterCodeStatementsForOriginal(FieldInfo field, EntityClassType entityClassType, CultureInfo cultureInfo)
    {
        if (entityClassType == EntityClassType.ObservablePoco)
        {
            yield return $"return _{field.Name.Sanitize().ToPascalCase(cultureInfo)}Original;";
        }
    }

    private static IEnumerable<string> GetSetterCodeStatementsForOriginal(FieldInfo field, EntityClassType entityClassType, CultureInfo cultureInfo)
    {
        if (entityClassType == EntityClassType.ObservablePoco)
        {
            yield return $"_{field.Name.Sanitize().ToPascalCase(cultureInfo)}Original = value;";
            yield return $@"if (PropertyChanged is not null) PropertyChanged(this, new {typeof(PropertyChangedEventArgs).FullName}(""{field.Name.Sanitize()}""));";
        }
    }
}
