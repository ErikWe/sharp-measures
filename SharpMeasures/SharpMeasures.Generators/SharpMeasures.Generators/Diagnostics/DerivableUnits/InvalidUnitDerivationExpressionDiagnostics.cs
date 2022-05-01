namespace SharpMeasures.Generators.Diagnostics.DerivableUnits;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class InvalidUnitDerivationExpressionDiagnostics
{
    public static Diagnostic Create(LiteralExpressionSyntax valueSyntax)
        => Diagnostic.Create(DiagnosticRules.InvalidUnitDerivationExpression, valueSyntax.GetLocation(), valueSyntax.ToString());
}
