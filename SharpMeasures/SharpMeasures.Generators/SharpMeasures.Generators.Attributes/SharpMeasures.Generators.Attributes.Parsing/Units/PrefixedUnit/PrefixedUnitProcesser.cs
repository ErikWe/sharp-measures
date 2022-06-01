namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System;

public interface IPrefixedUnitDiagnostics : IDependantUnitDiagnostics<RawPrefixedUnitDefinition>
{
    public abstract Diagnostic? UnrecognizedMetricPrefix(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedBinaryPrefix(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition);
}

public class PrefixedUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawPrefixedUnitDefinition, PrefixedUnitDefinition>
{
    private IPrefixedUnitDiagnostics Diagnostics { get; }

    public PrefixedUnitProcesser(IPrefixedUnitDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<PrefixedUnitDefinition> Process(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        var validity = CheckValidity(context, definition);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<PrefixedUnitDefinition>(validity.Diagnostics);
        }

        PrefixedUnitDefinition product = CreateResult(definition);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }

    private IValidityWithDiagnostics CheckValidity(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckDependantUnitValidity, CheckPrefixValidity);
    }

    private IValidityWithDiagnostics CheckPrefixValidity(IDependantUnitProcessingContext context, RawPrefixedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMetricPrefixName)
        {
            return CheckMetricPrefixValidity(context, definition);
        }

        return CheckBinaryPrefixValidity(context, definition);
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
