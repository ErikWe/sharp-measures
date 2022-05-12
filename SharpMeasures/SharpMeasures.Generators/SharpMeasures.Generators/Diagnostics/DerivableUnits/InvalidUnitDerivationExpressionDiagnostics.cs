namespace SharpMeasures.Generators.Diagnostics.DerivableUnits;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class InvalidUnitDerivationExpressionDiagnostics
{
    public static Diagnostic Create(LiteralExpressionSyntax expressionSyntax)
        => Diagnostic.Create(DiagnosticRules.InvalidUnitDerivationExpression, expressionSyntax.GetLocation(), expressionSyntax.ToString());
}
