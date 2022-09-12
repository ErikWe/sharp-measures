namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IScaledUnitInstanceProcessingDiagnostics : IModifiedUnitInstanceProcessingDiagnostics<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>
{
    public abstract Diagnostic? NullExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition);
    public abstract Diagnostic? EmptyExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition);
}

internal class ScaledUnitInstanceProcesser : AModifiedUnitInstanceProcesser<IUnitInstanceProcessingContext, RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations, ScaledUnitInstanceDefinition>
{
    private IScaledUnitInstanceProcessingDiagnostics Diagnostics { get; }

    public ScaledUnitInstanceProcesser(IScaledUnitInstanceProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ScaledUnitInstanceDefinition> Process(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitInstanceName(context, definition))
            .Validate(() => ValidateOriginalUnitInstance(context, definition))
            .Validate(() => ValidateExpression(context, definition))
            .Merge(() => ProcessUnitInstancePluralForm(context, definition))
            .Transform((interpretedPluralForm) => ProduceResult(definition, interpretedPluralForm));
    }

    private static ScaledUnitInstanceDefinition ProduceResult(RawScaledUnitInstanceDefinition definition, string interpretedPluralForm)
    {
        if (definition.Locations.ExplicitlySetScale)
        {
            return new(definition.Name!, interpretedPluralForm, definition.OriginalUnitInstance!, definition.Scale!.Value, definition.Locations);
        }

        return new(definition.Name!, interpretedPluralForm, definition.OriginalUnitInstance!, definition.Expression!, definition.Locations);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawScaledUnitInstanceDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetScale || definition.Locations.ExplicitlySetExpression;

        return base.VerifyRequiredPropertiesSet(definition).Validate(ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidateExpression(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpression is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        return ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IUnitInstanceProcessingContext context, RawScaledUnitInstanceDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.EmptyExpression(context, definition));
    }
}
