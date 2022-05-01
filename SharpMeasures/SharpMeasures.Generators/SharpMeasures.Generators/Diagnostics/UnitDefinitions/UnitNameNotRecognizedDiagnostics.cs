namespace SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal class UnitNameNotRecognizedDiagnostics
{
    public static Diagnostic Create(LiteralExpressionSyntax valueSyntax, INamedTypeSymbol unitType)
        => Diagnostic.Create(DiagnosticRules.InvalidUnitName, valueSyntax.GetLocation(), valueSyntax.ToString(), unitType.Name);
}
