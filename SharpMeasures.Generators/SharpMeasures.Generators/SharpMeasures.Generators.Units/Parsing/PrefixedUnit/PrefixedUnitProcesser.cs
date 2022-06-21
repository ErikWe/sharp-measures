namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

using System;

internal interface IPrefixedUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawPrefixedUnitDefinition>
{
    public abstract Diagnostic? UnrecognizedMetricPrefix(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedBinaryPrefix(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition);
}

internal class PrefixedUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawPrefixedUnitDefinition, PrefixedUnitDefinition>
{
    private IPrefixedUnitProcessingDiagnostics Diagnostics { get; }

    public PrefixedUnitProcesser(IPrefixedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<PrefixedUnitDefinition> Process(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        var validity = CheckValidity(context, definition);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<PrefixedUnitDefinition>(validity.Diagnostics);
        }

        PrefixedUnitDefinition product = CreateResult(definition);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }

    protected override bool VerifyRequiredPropertiesSet(RawPrefixedUnitDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition)
            && (definition.Locations.ExplicitlySetMetricPrefixName || definition.Locations.ExplicitlySetBinaryPrefixName);
    }

    private IValidityWithDiagnostics CheckValidity(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckDependantUnitValidity, CheckPrefixValidity);
    }

    private IValidityWithDiagnostics CheckPrefixValidity(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
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

    private IValidityWithDiagnostics CheckMetricPrefixValidity(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (Enum.IsDefined(typeof(MetricPrefixName), definition.MetricPrefixName) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedMetricPrefix(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckBinaryPrefixValidity(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (Enum.IsDefined(typeof(BinaryPrefixName), definition.BinaryPrefixName) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedBinaryPrefix(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static PrefixedUnitDefinition CreateResult(RawPrefixedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMetricPrefixName)
        {
            return new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.MetricPrefixName, definition.Locations);
        }

        return new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.BinaryPrefixName, definition.Locations);
    }
}
