namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

public static partial class DiagnosticConstruction
{
    public static Diagnostic InvalidUnitName(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitName, location, unitName);
    }

    public static Diagnostic NullUnitName(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnitName, location);
    }

    public static Diagnostic InvalidUnitPluralForm(Location? location, string pluralCode, string singularForm)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitPluralForm, location, pluralCode, singularForm);
    }

    public static Diagnostic NullUnitPluralForm(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnitPluralForm, location);
    }

    public static Diagnostic DuplicateUnitName(Location? location, string unitName, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateUnitName, location, unitTypeName, unitName);
    }

    public static Diagnostic DuplicateUnitPluralForm(Location? location, string unitPluralForm, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateUnitPluralForm, location, unitTypeName, unitPluralForm);
    }

    public static Diagnostic UnrecognizedUnitName(Location? location, string unitName, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnrecognizedUnitName, location, unitName, unitTypeName);
    }

    public static Diagnostic NullUnrecognizedUnitName(Location? location, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnrecognizedUnitName, location, unitTypeName);
    }

    public static Diagnostic CyclicDependency(Location? location, string unitName, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.CyclicUnitDependency, location, unitName, unitTypeName);
    }

    public static Diagnostic UnrecognizedPrefix(Location? location, MetricPrefixName metricPrefix)
    {
        return UnrecognizedPrefix(location, metricPrefix.ToString(), typeof(MetricPrefixName).Name);
    }

    public static Diagnostic UnrecognizedPrefix(Location? location, BinaryPrefixName binaryPrefix)
    {
        return UnrecognizedPrefix(location, binaryPrefix.ToString(), typeof(BinaryPrefixName).Name);
    }

    public static Diagnostic UnrecognizedPrefix(Location? location, string prefix, string prefixTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnrecognizedPrefix, location, prefix, prefixTypeName);
    }
}
