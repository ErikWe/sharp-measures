namespace SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class InvalidUnitNameDiagnostics
{
    public static Diagnostic Create(LiteralExpressionSyntax valueSyntax)
        => Diagnostic.Create(DiagnosticRules.InvalidUnitName, valueSyntax.GetLocation(), valueSyntax.ToString());
}
