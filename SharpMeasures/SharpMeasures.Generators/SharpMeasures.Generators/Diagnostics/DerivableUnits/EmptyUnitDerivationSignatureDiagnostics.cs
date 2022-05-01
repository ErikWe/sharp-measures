namespace SharpMeasures.Generators.Diagnostics.DerivableUnits;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class EmptyUnitDerivationSignatureDiagnostics
{
    public static Diagnostic Create(InitializerExpressionSyntax initializerSyntax)
        => Diagnostic.Create(DiagnosticRules.InvalidUnitDerivationExpression, initializerSyntax.GetLocation());
}
