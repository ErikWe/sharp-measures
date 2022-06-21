namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IBiasedUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawBiasedUnitDefinition>
{
    public abstract Diagnostic? UnitNotIncludingBiasTerm(IBiasedUnitProcessingContext context, RawBiasedUnitDefinition definition);
    public abstract Diagnostic? NullExpression(IBiasedUnitProcessingContext context, RawBiasedUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IBiasedUnitProcessingContext context, RawBiasedUnitDefinition definition);
}

internal interface IBiasedUnitProcessingContext : IDependantUnitProcessingContext
{
    public abstract bool UnitIncludesBiasTerm { get; }
}

internal class BiasedUnitProcesser : ADependantUnitProcesser<IBiasedUnitProcessingContext, RawBiasedUnitDefinition, BiasedUnitDefinition>
{
    private IBiasedUnitProcessingDiagnostics Diagnostics { get; }

    public BiasedUnitProcesser(IBiasedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<BiasedUnitDefinition> Process(IBiasedUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        if (context.UnitIncludesBiasTerm is false)
        {
            return OptionalWithDiagnostics.Empty<BiasedUnitDefinition>(Diagnostics.UnitNotIncludingBiasTerm(context, definition));
        }

        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<BiasedUnitDefinition>();
        }

        var validity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckDependantUnitValidity, CheckExpressionValidity);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<BiasedUnitDefinition>(validity.Diagnostics);
        }

        BiasedUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.Bias,
            definition.Expression, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }

    protected override bool VerifyRequiredPropertiesSet(RawBiasedUnitDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition) && (definition.Locations.ExplicitlySetBias || definition.Locations.ExplicitlySetExpression);
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IBiasedUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpression is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Expression is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullExpression(context, definition));
        }

        if (definition.Expression.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyExpression(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
