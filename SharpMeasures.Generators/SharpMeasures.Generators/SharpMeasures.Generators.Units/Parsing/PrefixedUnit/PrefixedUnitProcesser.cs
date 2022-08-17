namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

using System;

internal interface IPrefixedUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<UnprocessedPrefixedUnitDefinition, PrefixedUnitLocations>
{
    public abstract Diagnostic? UnrecognizedMetricPrefix(IUnitProcessingContext context, UnprocessedPrefixedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedBinaryPrefix(IUnitProcessingContext context, UnprocessedPrefixedUnitDefinition definition);
}

internal class PrefixedUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, UnprocessedPrefixedUnitDefinition, PrefixedUnitLocations, RawPrefixedUnitDefinition>
{
    private IPrefixedUnitProcessingDiagnostics Diagnostics { get; }

    public PrefixedUnitProcesser(IPrefixedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<RawPrefixedUnitDefinition> Process(IUnitProcessingContext context, UnprocessedPrefixedUnitDefinition definition)
    {
        return ValidateUnitName(context, definition)
            .Validate(() => ValidateDependantOn(context, definition))
            .Validate(() => ValidatePrefix(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((interpretedPlural) => ProduceResult(definition, interpretedPlural));
    }

    private static RawPrefixedUnitDefinition ProduceResult(UnprocessedPrefixedUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetMetricPrefixName)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.MetricPrefixName, definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.BinaryPrefixName, definition.Locations);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(UnprocessedPrefixedUnitDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetMetricPrefixName || definition.Locations.ExplicitlySetBinaryPrefixName;

        return base.VerifyRequiredPropertiesSet(definition).Validate(() => ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidatePrefix(IUnitProcessingContext context, UnprocessedPrefixedUnitDefinition definition)
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

    private IValidityWithDiagnostics ValidateMetricPrefix(IUnitProcessingContext context, UnprocessedPrefixedUnitDefinition definition)
    {
        if (Enum.IsDefined(typeof(MetricPrefixName), definition.MetricPrefixName) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedMetricPrefix(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics ValidateBinaryPrefix(IUnitProcessingContext context, UnprocessedPrefixedUnitDefinition definition)
    {
        if (Enum.IsDefined(typeof(BinaryPrefixName), definition.BinaryPrefixName) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedBinaryPrefix(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
