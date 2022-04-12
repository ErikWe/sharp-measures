namespace SharpMeasures.Generators.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using System;

internal static class TypeIsNotPartialDiagnostics
{
    public static void AnalyzeNamedType(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol, Type relevantAttribute)
    {
        if (namedTypeSymbol.DeclaringSyntaxReferences.Length is not 1)
        {
            return;
        }

        if (namedTypeSymbol.DeclaringSyntaxReferences[0].GetSyntax() is not TypeDeclarationSyntax declarationSyntax
            || IsPartial(declarationSyntax))
        {
            return;
        }

        Diagnostic diagnostics = Diagnostic.Create(DiagnosticRules.TypeIsNotPartial, declarationSyntax.Identifier.GetLocation(),
            relevantAttribute.FullName, namedTypeSymbol.Name);

        context.ReportDiagnostic(diagnostics);
    }

    private static bool IsPartial(TypeDeclarationSyntax declarationSyntax)
    {
        foreach (SyntaxToken token in declarationSyntax.Modifiers)
        {
            if (IsPartialModifier(token))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsPartialModifier(SyntaxToken token) => token.IsKind(SyntaxKind.PartialKeyword);
}
