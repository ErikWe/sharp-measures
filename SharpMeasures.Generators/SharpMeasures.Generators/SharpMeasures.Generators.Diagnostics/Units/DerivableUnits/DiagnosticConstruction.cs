namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

public static partial class DiagnosticConstruction
{
    public static Diagnostic NullUnitDerivationID(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullUnitDerivationID, location);
    }

    public static Diagnostic DuplicateUnitDerivationID(Location? location, string derivationID, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateUnitDerivationID, location, unitTypeName, derivationID);
    }

    public static Diagnostic UnrecognizedUnitDerivationID(Location? location, string derivationID, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnrecognizedUnitDerivationID, location, derivationID, unitTypeName);
    }

    public static Diagnostic IncompatibleDerivedUnitListSize(Location? location, int expectedCount, int actualCount)
    {
        return Diagnostic.Create(DiagnosticRules.IncompatibleDerivedUnitListSize, location, expectedCount, actualCount);
    }

    public static Diagnostic UnitNotDerivable(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitNotDerivable, location, unitName);
    }

    public static Diagnostic AmbiguousDerivationSignatureNotSpecified(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.AmbiguousDerivationSignatureNotSpecified, location, unitName);
    }
}
