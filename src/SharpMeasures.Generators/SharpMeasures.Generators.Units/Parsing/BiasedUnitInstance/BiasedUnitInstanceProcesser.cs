namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IBiasedUnitInstanceProcessingDiagnostics : IModifiedUnitInstanceProcessingDiagnostics<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations>
{
    public abstract Diagnostic? NullExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition);
    public abstract Diagnostic? EmptyExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition);
}

internal sealed class BiasedUnitInstanceProcesser : AModifiedUnitInstanceProcesser<IUnitInstanceProcessingContext, RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations, BiasedUnitInstanceDefinition>
{
    private IBiasedUnitInstanceProcessingDiagnostics Diagnostics { get; }

    public BiasedUnitInstanceProcesser(IBiasedUnitInstanceProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<BiasedUnitInstanceDefinition> Process(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitInstanceName(context, definition))
            .Validate(() => ValidateOriginalUnitInstance(context, definition))
            .Validate(() => ValidateExpression(context, definition))
            .Merge(() => ProcessUnitInstancePluralForm(context, definition))
            .Transform((interpretedPluralForm) => ProduceResult(definition, interpretedPluralForm));
    }

    private static BiasedUnitInstanceDefinition ProduceResult(RawBiasedUnitInstanceDefinition definition, string interpretedPluralForm)
    {
        if (definition.Locations.ExplicitlySetBias)
        {
            return new(definition.Name!, interpretedPluralForm, definition.OriginalUnitInstance!, definition.Bias!.Value, definition.Locations);
        }

        return new(definition.Name!, interpretedPluralForm, definition.OriginalUnitInstance!, definition.Expression!, definition.Locations);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawBiasedUnitInstanceDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetBias || definition.Locations.ExplicitlySetExpression;

        return base.VerifyRequiredPropertiesSet(definition).Validate(ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidateExpression(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpression is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        return ValidateExpressionNotNull(context, definition)
            .Validate(() => ValidateExpressionNotEmpty(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IUnitInstanceProcessingContext context, RawBiasedUnitInstanceDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length > 0, () => Diagnostics.EmptyExpression(context, definition));
    }
}
