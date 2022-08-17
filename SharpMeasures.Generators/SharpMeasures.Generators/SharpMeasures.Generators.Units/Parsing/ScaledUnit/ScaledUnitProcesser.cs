namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Globalization;

internal interface IScaledUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<UnprocessedScaledUnitDefinition, ScaledUnitLocations>
{
    public abstract Diagnostic? NullExpression(IUnitProcessingContext context, UnprocessedScaledUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IUnitProcessingContext context, UnprocessedScaledUnitDefinition definition);
}

internal class ScaledUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, UnprocessedScaledUnitDefinition, ScaledUnitLocations, RawScaledUnitDefinition>
{
    private IScaledUnitProcessingDiagnostics Diagnostics { get; }

    public ScaledUnitProcesser(IScaledUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<RawScaledUnitDefinition> Process(IUnitProcessingContext context, UnprocessedScaledUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Validate(() => ValidateDependantOn(context, definition))
            .Validate(() => ValidateExpression(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((interpretedPlural) => ProduceResult(definition, interpretedPlural));
    }

    private static RawScaledUnitDefinition ProduceResult(UnprocessedScaledUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetScale)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.Scale.ToString(CultureInfo.InvariantCulture), definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.Expression!, definition.Locations);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(UnprocessedScaledUnitDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetScale || definition.Locations.ExplicitlySetExpression;

        return base.VerifyRequiredPropertiesSet(definition).Validate(ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidateExpression(IUnitProcessingContext context, UnprocessedScaledUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpression is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        return ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IUnitProcessingContext context, UnprocessedScaledUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IUnitProcessingContext context, UnprocessedScaledUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.EmptyExpression(context, definition));
    }
}
