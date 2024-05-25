using System.ComponentModel.DataAnnotations;

namespace DataFramework.Pipelines.Entity.Components;

public class AddConstructorComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new AddConstructorComponent();
}

public class AddConstructorComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (!context.Request.Settings.EntityClassType.IsImmutable())
        {
            // Poco and observable poco entities can simply use the default constructor
            return Task.FromResult(Result.Continue());
        }

        var cultureInfo = context.Request.FormatProvider.ToCultureInfo();

        context.Request.Builder.AddConstructors(new ConstructorBuilder()
            .AddStringCodeStatements(GetEntityClassConstructorCodeStatements(context.Request, cultureInfo))
            .AddParameters(GetEditableFieldsWithConcurrencyCheckFields(context.Request).Select(f => f.ToParameterBuilder(cultureInfo)))
            );

        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<string> GetEntityClassConstructorCodeStatements(EntityContext request, CultureInfo cultureInfo)
    {
        foreach (var field in GetEditableFieldsWithConcurrencyCheckFields(request))
        {
            yield return $"this.{field.CreatePropertyName(request.SourceModel)} = {field.Name.Sanitize().ToPascalCase(cultureInfo)};";
        }

        if (request.Settings.AddValidationCodeInConstructor)
        {
            yield return $"{typeof(Validator).FullName}.ValidateObject(this, new {typeof(ValidationContext).FullName}(this, null, null), true);";
        }
    }

    private static IEnumerable<FieldInfo> GetEditableFieldsWithConcurrencyCheckFields(EntityContext request)
    {
        foreach (var field in request.SourceModel.Fields.Where(f => !f.IsComputed && f.CanSet))
        {
            yield return field;
        }

        foreach (var field in request.SourceModel.GetUpdateConcurrencyCheckFields(request.Settings.ConcurrencyCheckBehavior))
        {
            if (!field.IsIdentityField
                && !field.IsDatabaseIdentityField
                && !field.UseForConcurrencyCheck
                && request.Settings.ConcurrencyCheckBehavior != ConcurrencyCheckBehavior.AllFields)
            {
                continue;
            }

            yield return new FieldInfoBuilder(field)
                .WithName($"{field.Name}Original")
                .WithIsNullable()
                .WithDefaultValue("default")
                .Build();
        }
    }
}
