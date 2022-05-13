namespace SharpMeasures.Generators.Diagnostics.DerivableUnits;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class InvalidUnitDerivationExpressionDiagnostics
{
    public static Diagnostic Create(Location location, string expression) => Diagnostic.Create(DiagnosticRules.InvalidUnitDerivationExpression, location, expression);
    public static Diagnostic Create(MinimalLocation location, string expression) => Create(location.ToLocation(), expression);
}
