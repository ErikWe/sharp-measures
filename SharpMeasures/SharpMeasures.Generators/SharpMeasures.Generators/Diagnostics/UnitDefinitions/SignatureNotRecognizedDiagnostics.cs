namespace SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class SignatureNotRecognizedDiagnostics
{
    public static Diagnostic Create(AttributeArgumentSyntax argumentSyntax, INamedTypeSymbol unitType)
        => Diagnostic.Create(DiagnosticRules.DerivedUnitSignatureNotRecognized, argumentSyntax.GetLocation(), unitType.Name);
}
