namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System;

public interface IPrefixedUnitDiagnostics : IDependantUnitDiagnostics<PrefixedUnitDefinition>
{
    public abstract Diagnostic? UnrecognizedMetricPrefix(IDependantUnitValidatorContext context, PrefixedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedBinaryPrefix(IDependantUnitValidatorContext context, PrefixedUnitDefinition definition);
}

public class PrefixedUnitValidator : ADependantUnitValidator<IDependantUnitValidatorContext, PrefixedUnitDefinition>
{
    private IPrefixedUnitDiagnostics Diagnostics { get; }

    public PrefixedUnitValidator(IPrefixedUnitDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics CheckValidity(IDependantUnitValidatorContext context, PrefixedUnitDefinition definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return ValidityWithDiagnostics.DiagnoseAndMergeWhileValid(context, definition, base.CheckValidity, CheckPrefixValidity);
    }

    private IValidityWithDiagnostics CheckPrefixValidity(IDependantUnitValidatorContext context, PrefixedUnitDefinition definition)
    {
        return definition.ParsingData.SpecifiedPrefixType switch
        {
            PrefixedUnitParsingData.PrefixType.Metric => CheckMetricPrefixValidity(context, definition),
            PrefixedUnitParsingData.PrefixType.Binary => CheckBinaryPrefixValidity(context, definition),
            _ => ValidityWithDiagnostics.InvalidWithoutDiagnostics
        };
    }

    private IValidityWithDiagnostics CheckMetricPrefixValidity(IDependantUnitValidatorContext context, PrefixedUnitDefinition definition)
    {
        if (Enum.IsDefined(typeof(MetricPrefixName), definition.MetricPrefixName) is false)
        {
            return CreateInvalidity(Diagnostics.UnrecognizedMetricPrefix(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckBinaryPrefixValidity(IDependantUnitValidatorContext context, PrefixedUnitDefinition definition)
    {
        if (Enum.IsDefined(typeof(BinaryPrefixName), definition.BinaryPrefixName) is false)
        {
            return CreateInvalidity(Diagnostics.UnrecognizedBinaryPrefix(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
