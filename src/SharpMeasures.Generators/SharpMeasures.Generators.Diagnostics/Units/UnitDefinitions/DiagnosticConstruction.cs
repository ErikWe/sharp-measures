namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticConstruction
{
    public static Diagnostic InvalidUnitInstanceName(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitInstanceName, location, unitName);
    }

    public static Diagnostic NullUnitInstanceName(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnitInstanceName, location);
    }

    public static Diagnostic InvalidUnitInstancePluralForm(Location? location, string pluralCode, string singularForm)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitInstancePluralForm, location, pluralCode, singularForm);
    }

    public static Diagnostic NullUnitInstancePluralForm(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnitInstancePluralForm, location);
    }

    public static Diagnostic DuplicateUnitInstanceName(Location? location, string unitName, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateUnitInstanceName, location, unitTypeName, unitName);
    }

    public static Diagnostic UnitInstanceNameReservedByUnitInstancePluralForm(Location? location, string unitName, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitInstanceNameReservedByUnitInstancePluralForm, location, unitTypeName, unitName);
    }

    public static Diagnostic DuplicateUnitInstancePluralForm(Location? location, string unitPluralForm, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateUnitInstancePluralForm, location, unitTypeName, unitPluralForm);
    }

    public static Diagnostic UnitInstancePluralFormReservedByUnitInstanceName(Location? location, string unitPluralForm, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitInstancePluralFormReservedByUnitInstanceName, location, unitTypeName, unitPluralForm);
    }

    public static Diagnostic UnrecognizedUnitInstanceName(Location? location, string unitName, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnrecognizedUnitInstanceName, location, unitName, unitTypeName);
    }

    public static Diagnostic NullUnrecognizedUnitInstanceName(Location? location, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnrecognizedUnitInstanceName, location, unitTypeName);
    }

    public static Diagnostic NullUnrecognizedUnitInstanceNameUnknownType(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnrecognizedUnitInstanceNameUnknownType, location);
    }

    public static Diagnostic CyclicallyModifiedUnitInstances(Location? location, string unitName, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.CyclicallyModifiedUnitInstances, location, unitName, unitTypeName);
    }

    public static Diagnostic DerivableUnitShouldNotUseFixed(Location? location, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DerivableUnitShouldNotUseFixed, location, unitTypeName);
    }

    public static Diagnostic NullScaledUnitExpression(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullScaledUnitExpression, location);
    }

    public static Diagnostic EmptyScaledUnitExpression(Location? location) => NullScaledUnitExpression(location);

    public static Diagnostic NullBiasedUnitExpression(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullBiasedUnitExpression, location);
    }

    public static Diagnostic EmptyBiasedUnitExpression(Location? location) => NullBiasedUnitExpression(location);

    public static Diagnostic BiasedUnitDefinedButUnitNotBiased(Location? location, string unitName, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.BiasedUnitDefinedButUnitNotBiased, location, unitName, unitTypeName);
    }

    public static Diagnostic UnitWithBiasTermCannotBeDerived(Location? location, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitWithBiasTermCannotBeDerived, location, unitTypeName);
    }
}
