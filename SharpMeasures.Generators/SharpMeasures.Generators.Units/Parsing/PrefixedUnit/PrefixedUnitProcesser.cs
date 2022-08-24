namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

using System;

internal interface IPrefixedUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawPrefixedUnitDefinition, PrefixedUnitLocations>
{
    public abstract Diagnostic? UnrecognizedMetricPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedBinaryPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition);
}

internal class PrefixedUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, RawPrefixedUnitDefinition, PrefixedUnitLocations, PrefixedUnitDefinition>
{
    private IPrefixedUnitProcessingDiagnostics Diagnostics { get; }

    public PrefixedUnitProcesser(IPrefixedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<PrefixedUnitDefinition> Process(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        return ValidateUnitName(context, definition)
            .Validate(() => ValidateDependantOn(context, definition))
            .Validate(() => ValidatePrefix(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((interpretedPlural) => ProduceResult(definition, interpretedPlural));
    }

    private static PrefixedUnitDefinition ProduceResult(RawPrefixedUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetMetricPrefixName)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.MetricPrefixName, definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.BinaryPrefixName, definition.Locations);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawPrefixedUnitDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetMetricPrefixName || definition.Locations.ExplicitlySetBinaryPrefixName;

        return base.VerifyRequiredPropertiesSet(definition).Validate(() => ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidatePrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMetricPrefixName)
        {
            return ValidateMetricPrefix(context, definition);
        }
        
        if (definition.Locations.ExplicitlySetBinaryPrefixName)
        {
            return ValidateBinaryPrefix(context, definition);
        }

        return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
    }

    private IValidityWithDiagnostics ValidateMetricPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        var validMetricPrefix = Enum.IsDefined(typeof(MetricPrefixName), definition.MetricPrefixName);

        return ValidityWithDiagnostics.Conditional(validMetricPrefix, () => Diagnostics.UnrecognizedMetricPrefix(context, definition));
    }

    private IValidityWithDiagnostics ValidateBinaryPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        var validBinaryPrefix = Enum.IsDefined(typeof(BinaryPrefixName), definition.BinaryPrefixName);

        return ValidityWithDiagnostics.Conditional(validBinaryPrefix, () => Diagnostics.UnrecognizedBinaryPrefix(context, definition));
    }
}
