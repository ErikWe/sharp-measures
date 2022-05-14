namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

using System;

internal class PrefixedUnitValidator : DependantUnitDefinitionValidator<PrefixedUnitDefinition>
{
    public PrefixedUnitValidator(INamedTypeSymbol unitType) : base(unitType) { }

    public override ExtractionValidity Check(AttributeData attributeData, PrefixedUnitDefinition definition)
    {
        if (base.Check(attributeData, definition) is ExtractionValidity { IsInvalid: true } invalidUnit)
        {
            return invalidUnit;
        }

        if (ValidPrefix(definition) is false)
        {
            return ExtractionValidity.Invalid(CreateUndefinedPrefixDiagnostics(definition));
        }

        return ExtractionValidity.Valid;
    }

    private static bool ValidPrefix(PrefixedUnitDefinition definition)
    {
        return Branch(definition.ParsingData.SpecifiedPrefixType, definition, ValidMetricPrefix, ValidBinaryPrefix);
    }

    private static bool ValidMetricPrefix(PrefixedUnitDefinition parameters) => Enum.IsDefined(typeof(MetricPrefixName), parameters.MetricPrefixName);
    private static bool ValidBinaryPrefix(PrefixedUnitDefinition parameters) => Enum.IsDefined(typeof(BinaryPrefixName), parameters.BinaryPrefixName);

    private static Diagnostic? CreateUndefinedPrefixDiagnostics(PrefixedUnitDefinition definition)
    {
        return Branch(definition.ParsingData.SpecifiedPrefixType, definition, CreateUndefinedMetricPrefixDiagnostics, CreateUndefinedBinaryPrefixDiagnostics);
    }

    private static Diagnostic CreateUndefinedMetricPrefixDiagnostics(PrefixedUnitDefinition definition)
    {
        return Diagnostic.Create(DiagnosticRules.UndefinedPrefix, definition.Locations.MetricPrefixName, definition.MetricPrefixName, typeof(MetricPrefixName));
    }

    private static Diagnostic CreateUndefinedBinaryPrefixDiagnostics(PrefixedUnitDefinition definition)
    {
        return Diagnostic.Create(DiagnosticRules.UndefinedPrefix, definition.Locations.BinaryPrefixName, definition.BinaryPrefixName, typeof(BinaryPrefixName));
    }

    private static T? Branch<T>(PrefixedUnitParsingData.PrefixType prefixType, Func<T> metricDelegate, Func<T> binaryDelegate)
    {
        return prefixType switch
        {
            PrefixedUnitParsingData.PrefixType.Metric => metricDelegate(),
            PrefixedUnitParsingData.PrefixType.Binary => binaryDelegate(),
            _ => default
        };
    }

    private static TOut? Branch<TIn, TOut>(PrefixedUnitParsingData.PrefixType prefixType, TIn input, Func<TIn, TOut> metricDelegate, Func<TIn, TOut> binaryDelegate)
    {
        return Branch(prefixType, () => metricDelegate(input), () => binaryDelegate(input));
    }
}
