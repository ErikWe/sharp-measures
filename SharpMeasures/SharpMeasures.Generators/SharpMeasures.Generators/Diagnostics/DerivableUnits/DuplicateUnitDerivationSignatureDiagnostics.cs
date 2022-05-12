namespace SharpMeasures.Generators.Diagnostics.DerivableUnits;

using Microsoft.CodeAnalysis;

internal static class DuplicateUnitDerivationSignatureDiagnostics
{
    public static Diagnostic Create(Location signatureSyntaxLocation, string unitName, string signature)
        => Diagnostic.Create(DiagnosticRules.DuplicateUnitDerivationSignature, signatureSyntaxLocation, unitName, signature);

    public static Diagnostic Create(MinimalLocation signatureSyntaxLocation, string unitName, string signature)
        => Create(signatureSyntaxLocation.ToLocation(), unitName, signature);
}
