namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System;

internal interface IPrefixedUnitInstanceProcessingDiagnostics : IModifiedUnitInstanceProcessingDiagnostics<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>
{
    public abstract Diagnostic? UnrecognizedMetricPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition);
    public abstract Diagnostic? UnrecognizedBinaryPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition);
}

internal class PrefixedUnitInstanceProcesser : AModifiedUnitInstanceProcesser<IUnitInstanceProcessingContext, RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations, PrefixedUnitInstanceDefinition>
{
    private IPrefixedUnitInstanceProcessingDiagnostics Diagnostics { get; }

    public PrefixedUnitInstanceProcesser(IPrefixedUnitInstanceProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<PrefixedUnitInstanceDefinition> Process(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition)
    {
        return ValidateUnitInstanceName(context, definition)
            .Validate(() => ValidateOriginalUnitInstance(context, definition))
            .Validate(() => ValidatePrefix(context, definition))
            .Merge(() => ProcessUnitInstancePluralForm(context, definition))
            .Transform((interpretedPluralForm) => ProduceResult(definition, interpretedPluralForm));
    }

    private static PrefixedUnitInstanceDefinition ProduceResult(RawPrefixedUnitInstanceDefinition definition, string interpretedPluralForm)
    {
        if (definition.Locations.ExplicitlySetMetricPrefix)
        {
            return new(definition.Name!, interpretedPluralForm, definition.OriginalUnitInstance!, definition.MetricPrefix!.Value, definition.Locations);
        }

        return new(definition.Name!, interpretedPluralForm, definition.OriginalUnitInstance!, definition.BinaryPrefix!.Value, definition.Locations);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawPrefixedUnitInstanceDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetMetricPrefix || definition.Locations.ExplicitlySetBinaryPrefix;

        return base.VerifyRequiredPropertiesSet(definition).Validate(() => ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidatePrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMetricPrefix)
        {
            return ValidateMetricPrefix(context, definition);
        }
        
        if (definition.Locations.ExplicitlySetBinaryPrefix)
        {
            return ValidateBinaryPrefix(context, definition);
        }

        return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
    }

    private IValidityWithDiagnostics ValidateMetricPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition)
    {
        var validMetricPrefix = Enum.IsDefined(typeof(MetricPrefixName), definition.MetricPrefix);

        return ValidityWithDiagnostics.Conditional(validMetricPrefix, () => Diagnostics.UnrecognizedMetricPrefix(context, definition));
    }

    private IValidityWithDiagnostics ValidateBinaryPrefix(IUnitInstanceProcessingContext context, RawPrefixedUnitInstanceDefinition definition)
    {
        var validBinaryPrefix = Enum.IsDefined(typeof(BinaryPrefixName), definition.BinaryPrefix);

        return ValidityWithDiagnostics.Conditional(validBinaryPrefix, () => Diagnostics.UnrecognizedBinaryPrefix(context, definition));
    }
}
