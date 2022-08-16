namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

using System;
using System.Linq;

internal interface IPrefixedUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawPrefixedUnitDefinition, PrefixedUnitLocations>
{
    public abstract Diagnostic? UnrecognizedMetricPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedBinaryPrefix(IUnitProcessingContext context, RawPrefixedUnitDefinition definition);
}

internal class PrefixedUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, RawPrefixedUnitDefinition, PrefixedUnitLocations, UnresolvedPrefixedUnitDefinition>
{
    private IPrefixedUnitProcessingDiagnostics Diagnostics { get; }

    public PrefixedUnitProcesser(IPrefixedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedPrefixedUnitDefinition> Process(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedPrefixedUnitDefinition>(allDiagnostics);
        }

        var processedPlural = ProcessPlural(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedPlural.Diagnostics);

        if (processedPlural.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedPrefixedUnitDefinition>(allDiagnostics);
        }

        var product = CreateProduct(definition, processedPlural.Result);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected override bool VerifyRequiredPropertiesSet(RawPrefixedUnitDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition) && (definition.Locations.ExplicitlySetMetricPrefixName || definition.Locations.ExplicitlySetBinaryPrefixName);
    }

    private IValidityWithDiagnostics CheckValidity(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        return IterativeValidation.DiagnoseAndMergeWhileValid(context, definition, CheckDependantUnitValidity, CheckPrefixValidity);
    }

    private IValidityWithDiagnostics CheckPrefixValidity(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMetricPrefixName)
        {
            return CheckMetricPrefixValidity(context, definition);
        }
        else if (definition.Locations.ExplicitlySetBinaryPrefixName)
        {
            return CheckBinaryPrefixValidity(context, definition);
        }

        return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
    }

    private IValidityWithDiagnostics CheckMetricPrefixValidity(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (Enum.IsDefined(typeof(MetricPrefixName), definition.MetricPrefixName) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedMetricPrefix(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckBinaryPrefixValidity(IUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (Enum.IsDefined(typeof(BinaryPrefixName), definition.BinaryPrefixName) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedBinaryPrefix(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static UnresolvedPrefixedUnitDefinition CreateProduct(RawPrefixedUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetMetricPrefixName)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.MetricPrefixName, definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.BinaryPrefixName, definition.Locations);
    }
}
