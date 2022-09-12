namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Globalization;

public static partial class DiagnosticConstruction
{
    public static Diagnostic MultipleDerivationSignaturesButNotNamed(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.MultipleDerivationSignaturesButNotNamed, location, unitName);
    }

    public static Diagnostic AmbiguousDerivationSignatureNotSpecified(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.AmbiguousDerivationSignatureNotSpecified, location, unitName);
    }

    public static Diagnostic DuplicateUnitDerivationID(Location? location, string derivationID, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateUnitDerivationID, location, unitTypeName, derivationID);
    }

    public static Diagnostic DuplicateUnitDerivationSignature(Location? location, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateUnitDerivationSignature, location, unitTypeName);
    }

    public static Diagnostic UnrecognizedUnitDerivationID(Location? location, string derivationID, string unitTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.UnrecognizedUnitDerivationID, location, unitTypeName, derivationID);
    }

    public static Diagnostic IncompatibleDerivedUnitListSize(Location? location, int expectedCount, int actualCount)
    {
        return Diagnostic.Create(DiagnosticRules.IncompatibleDerivedUnitListSize, location, expectedCount, actualCount);
    }

    public static Diagnostic UnitNotDerivable(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitNotDerivable, location, unitName);
    }

    public static Diagnostic DerivationSignatureNotPermutable(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.DerivationSignatureNotPermutable, location);
    }

    public static Diagnostic UnmatchedDerivationExpressionUnit(Location? location, int requestedIndex)
    {
        return Diagnostic.Create(DiagnosticRules.UnmatchedDerivationExpressionUnit, location, requestedIndex.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic ExpressionDoesNotIncludeUnit(Location? location, int index)
    {
        return Diagnostic.Create(DiagnosticRules.ExpressionDoesNotIncludeUnit, location, index.ToString(CultureInfo.InvariantCulture));
    }
}
