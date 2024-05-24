namespace DataFramework.Pipelines.Entity.Components;

public class ObservableComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new ObservableComponent();
}

public class ObservableComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Request.Settings.EntityClassType != EntityClassType.ObservablePoco)
        {
            return Task.FromResult(Result.Continue());
        }

        var cultureInfo = context.Request.FormatProvider.ToCultureInfo();

        context.Request.Builder
            .AddInterfaces(typeof(INotifyPropertyChanged))
            .AddFields(new FieldBuilder()
                .WithName(nameof(INotifyPropertyChanged.PropertyChanged))
                .WithType(typeof(PropertyChangedEventHandler))
                .WithEvent()
                .WithIsNullable()
                .WithVisibility(Visibility.Public)
            )
            .AddFields(context.Request.SourceModel.Fields.Select(field => new FieldBuilder().FillFrom(field, cultureInfo)))
            .AddFields(context.Request.SourceModel.GetUpdateConcurrencyCheckFields(context.Request.Settings.ConcurrencyCheckBehavior).Select(field =>
                new FieldBuilder()
                    .FillFrom(field, cultureInfo)
                    .WithName($"_{field.Name.ToPascalCase(cultureInfo)}Original")
                    .WithIsNullable(true)))
            .AddMethods(new MethodBuilder()
                .WithName("HandlePropertyChanged")
                .AddParameter("propertyName", typeof(string))
                .WithProtected()
                .AddStringCodeStatements("PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));")
            );

        return Task.FromResult(Result.Continue());
    }
}
