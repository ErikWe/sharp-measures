namespace SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class InvalidUnitPluralFormDiagnostics
{
    public static Diagnostic Create(LiteralExpressionSyntax valueSyntax)
        => Diagnostic.Create(DiagnosticRules.InvalidUnitPluralForm, valueSyntax.GetLocation(), valueSyntax.ToString());
}
