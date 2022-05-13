namespace SharpMeasures.Generators.Diagnostics.DerivableUnits;

using Microsoft.CodeAnalysis;

internal static class EmptyUnitDerivationSignatureDiagnostics
{
    public static Diagnostic Create(Location location) => Diagnostic.Create(DiagnosticRules.InvalidUnitDerivationExpression, location);
    public static Diagnostic Create(MinimalLocation location) => Create(location.ToLocation());
}
