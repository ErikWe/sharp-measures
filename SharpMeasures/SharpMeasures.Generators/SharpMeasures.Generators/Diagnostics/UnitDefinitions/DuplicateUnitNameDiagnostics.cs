namespace SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class DuplicateUnitNameDiagnostics
{
    public static Diagnostic Create(BaseTypeDeclarationSyntax declarationSyntax, string unitName)
        => Diagnostic.Create(DiagnosticRules.DuplicateUnitName, declarationSyntax.GetLocation(), declarationSyntax.Identifier.Text, unitName);

    public static Diagnostic Create(LiteralExpressionSyntax expressionSyntax, string unitType)
        => Diagnostic.Create(DiagnosticRules.DuplicateUnitName, expressionSyntax.GetLocation(), expressionSyntax.ToString(), unitType);
}
