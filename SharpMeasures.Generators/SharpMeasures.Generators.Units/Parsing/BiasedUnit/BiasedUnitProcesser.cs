namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Globalization;

internal interface IBiasedUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawBiasedUnitDefinition, BiasedUnitLocations>
{
    public abstract Diagnostic? NullExpression(IUnitProcessingContext context, RawBiasedUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IUnitProcessingContext context, RawBiasedUnitDefinition definition);
}

internal class BiasedUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, RawBiasedUnitDefinition, BiasedUnitLocations, BiasedUnitDefinition>
{
    private IBiasedUnitProcessingDiagnostics Diagnostics { get; }

    public BiasedUnitProcesser(IBiasedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<BiasedUnitDefinition> Process(IUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Validate(() => ValidateDependantOn(context, definition))
            .Validate(() => ValidateExpression(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((interpretedPlural) => ProduceResult(definition, interpretedPlural));
    }

    private static BiasedUnitDefinition ProduceResult(RawBiasedUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetBias)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.Bias.ToString(CultureInfo.InvariantCulture), definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.Expression!, definition.Locations);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawBiasedUnitDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetBias || definition.Locations.ExplicitlySetExpression;

        return base.VerifyRequiredPropertiesSet(definition).Validate(ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidateExpression(IUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpression is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        return ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.EmptyExpression(context, definition));
    }
}
