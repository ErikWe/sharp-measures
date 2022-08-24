namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Globalization;

internal interface IScaledUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawScaledUnitDefinition, ScaledUnitLocations>
{
    public abstract Diagnostic? NullExpression(IUnitProcessingContext context, RawScaledUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IUnitProcessingContext context, RawScaledUnitDefinition definition);
}

internal class ScaledUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, RawScaledUnitDefinition, ScaledUnitLocations, ScaledUnitDefinition>
{
    private IScaledUnitProcessingDiagnostics Diagnostics { get; }

    public ScaledUnitProcesser(IScaledUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ScaledUnitDefinition> Process(IUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Validate(() => ValidateDependantOn(context, definition))
            .Validate(() => ValidateExpression(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((interpretedPlural) => ProduceResult(definition, interpretedPlural));
    }

    private static ScaledUnitDefinition ProduceResult(RawScaledUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetScale)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.Scale.ToString(CultureInfo.InvariantCulture), definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.Expression!, definition.Locations);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawScaledUnitDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetScale || definition.Locations.ExplicitlySetExpression;

        return base.VerifyRequiredPropertiesSet(definition).Validate(ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidateExpression(IUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpression is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        return ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.EmptyExpression(context, definition));
    }
}
