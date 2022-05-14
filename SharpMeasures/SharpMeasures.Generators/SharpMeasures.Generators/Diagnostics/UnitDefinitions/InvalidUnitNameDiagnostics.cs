namespace SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class InvalidUnitNameDiagnostics
{
    public static Diagnostic Create(LiteralExpressionSyntax valueSyntax)
        => Diagnostic.Create(DiagnosticRules.InvalidUnitName, valueSyntax.GetLocation(), valueSyntax.ToString());

    public static Diagnostic Create(Location location, string unitName) => Diagnostic.Create(DiagnosticRules.InvalidUnitName, location, unitName);
    public static Diagnostic Create(MinimalLocation location, string unitName) => Create(location.ToLocation(), unitName);
}
