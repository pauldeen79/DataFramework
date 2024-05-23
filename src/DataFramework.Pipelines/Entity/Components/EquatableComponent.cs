namespace DataFramework.Pipelines.Entity.Components;

public class EquatableComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new EquatableComponent();
}

public class EquatableComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Request.Settings.EntityClassType != EntityClassType.ImmutableClass)
        {
            return Task.FromResult(Result.Continue());
        }

        context.Request.Builder.AddInterfaces($"IEquatable<{context.Request.SourceModel.Name}>");

        context.Request.Builder.AddMethods(
            new MethodBuilder().WithName(nameof(Equals))
                               .WithReturnType(typeof(bool))
                               .WithOverride()
                               .AddParameter("obj", typeof(object))
                               .AddStringCodeStatements($"return Equals(obj as {context.Request.SourceModel.Name});"),

            new MethodBuilder().WithName(nameof(IEquatable<object>.Equals))
                               .WithReturnType(typeof(bool))
                               .AddParameter("other", context.Request.SourceModel.Name)
                               .AddStringCodeStatements($"return other != null &&{Environment.NewLine}       {GetEntityEqualsProperties(context.Request.SourceModel)};"),

            new MethodBuilder().WithName(nameof(GetHashCode))
                               .WithReturnType(typeof(int))
                               .WithOverride()
                               .AddStringCodeStatements("int hashCode = 235838129;")
                               .AddStringCodeStatements(context.Request.SourceModel.Fields.Select(f => Type.GetType(f.PropertyTypeName.FixTypeName()).IsValueType
                                  ? $"hashCode = hashCode * -1521134295 + {f.CreatePropertyName(context.Request.SourceModel)}.GetHashCode();"
                                  : $"hashCode = hashCode * -1521134295 + EqualityComparer<{f.PropertyTypeName.FixTypeName()}>.Default.GetHashCode({f.CreatePropertyName(context.Request.SourceModel)});"))
                               .AddStringCodeStatements("return hashCode;"),

            new MethodBuilder().WithName("==")
                               .WithReturnType(typeof(bool))
                               .WithStatic()
                               .WithOperator()
                               .AddParameter("left", context.Request.SourceModel.Name)
                               .AddParameter("right", context.Request.SourceModel.Name)
                               .AddStringCodeStatements($"return EqualityComparer<{context.Request.SourceModel.Name}>.Default.Equals(left, right);"),

            new MethodBuilder().WithName("!=")
                               .WithReturnType(typeof(bool))
                               .WithStatic()
                               .WithOperator()
                               .AddParameter("left", context.Request.SourceModel.Name)
                               .AddParameter("right", context.Request.SourceModel.Name)
                               .AddStringCodeStatements("return !(left == right);")
            );
        return Task.FromResult(Result.Continue());
    }
    
    private static string GetEntityEqualsProperties(DataObjectInfo instance)
        => string.Join(" &&"
            + Environment.NewLine
            + "       ", instance.Fields.Select(f => $"{f.CreatePropertyName(instance)} == other.{f.CreatePropertyName(instance)}"));

}
