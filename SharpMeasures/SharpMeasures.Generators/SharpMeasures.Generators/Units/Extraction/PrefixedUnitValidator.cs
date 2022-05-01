namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using System;

internal class PrefixedUnitValidator : DependantUnitDefinitionValidator<PrefixedUnitParameters>
{
    public PrefixedUnitValidator(INamedTypeSymbol unitType) : base(unitType) { }

    public override ExtractionValidity Check(AttributeData attributeData, PrefixedUnitParameters parameters)
    {
        if (base.Check(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalidUnit)
        {
            return invalidUnit;
        }

        if (!ValidPrefix(parameters))
        {
            return ExtractionValidity.Invalid(CreateUndefinedPrefixDiagnostics(attributeData, parameters));
        }

        return ExtractionValidity.Valid;
    }

    private static bool ValidPrefix(PrefixedUnitParameters parameters)
    {
        return parameters.SpecifiedPrefixType switch
        {
            PrefixedUnitParameters.PrefixType.Metric => ValidMetricPrefix(parameters),
            PrefixedUnitParameters.PrefixType.Binary => ValidBinaryPrefix(parameters),
            _ => false
        };
    }

    private static bool ValidMetricPrefix(PrefixedUnitParameters parameters)
        => Enum.IsDefined(typeof(MetricPrefixName), parameters.MetricPrefixName);

    private static bool ValidBinaryPrefix(PrefixedUnitParameters parameters)
        => Enum.IsDefined(typeof(BinaryPrefixName), parameters.BinaryPrefixName);

    private static Diagnostic? CreateUndefinedPrefixDiagnostics(AttributeData attributeData, PrefixedUnitParameters parameters)
    {
        return parameters.SpecifiedPrefixType switch
        {
            PrefixedUnitParameters.PrefixType.Metric => CreateUndefinedMetricPrefixDiagnostics(attributeData),
            PrefixedUnitParameters.PrefixType.Binary => CreateUndefinedBinaryPrefixDiagnostics(attributeData),
            _ => null
        };
    }

    private static Diagnostic? CreateUndefinedMetricPrefixDiagnostics(AttributeData attributeData)
    {
        if (MetricPrefixNameArgumentSyntax(attributeData) is AttributeArgumentSyntax argumentSyntax)
        {
            return UndefinedPrefixDiagnostics.Create<MetricPrefixName>(argumentSyntax);
        }

        return null;
    }

    private static Diagnostic? CreateUndefinedBinaryPrefixDiagnostics(AttributeData attributeData)
    {
        if (BinaryPrefixNameArgumentSyntax(attributeData) is AttributeArgumentSyntax argumentSyntax)
        {
            return UndefinedPrefixDiagnostics.Create<BinaryPrefixName>(argumentSyntax);
        }

        return null;
    }

    protected static AttributeArgumentSyntax? MetricPrefixNameArgumentSyntax(AttributeData attributeData)
        => attributeData.GetArgumentSyntax(MetricPrefixNameArgumentIndex(attributeData));
    protected static AttributeArgumentSyntax? BinaryPrefixNameArgumentSyntax(AttributeData attributeData)
        => attributeData.GetArgumentSyntax(BinaryPrefixNameArgumentIndex(attributeData));

    protected override int NameArgumentIndex(AttributeData attributeData) => PrefixedUnitParser.NameIndex(attributeData);
    protected override int PluralArgumentIndex(AttributeData attributeData) => PrefixedUnitParser.PluralIndex(attributeData);
    protected override int DependantOnArgumentIndex(AttributeData attributeData) => PrefixedUnitParser.FromIndex(attributeData);
    protected static int MetricPrefixNameArgumentIndex(AttributeData attributeData) => PrefixedUnitParser.MetricPrefixNameIndex(attributeData);
    protected static int BinaryPrefixNameArgumentIndex(AttributeData attributeData) => PrefixedUnitParser.BinaryPrefixNameindex(attributeData);
}
