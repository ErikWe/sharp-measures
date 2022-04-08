namespace ErikWe.SharpMeasures.SourceGenerators.Analyzers.Diagnostics;

using ErikWe.SharpMeasures.Attributes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;

public static class TypeIsNotPartialDiagnostics
{
    public readonly static DiagnosticDescriptor Rule = new DiagnosticDescriptor(
        id: DiagnosticIDs.TypeIsNotPartial,
        title: "Source generation requires types to be partial",
        messageFormat: "To allow source generation, as indicated by the attribute {0}, {1} has to be made partial",
        category: "Syntax",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    private static string[] RelevantAttributeNames { get; } = new[]
    {
        typeof(UnitAttribute).FullName,
        typeof(BiasedUnitAttribute).FullName,
        typeof(ScalarQuantityAttribute).FullName
    };

    internal static void Register(AnalysisContext context)
    {
        context.RegisterSymbolAction(AnalyseNamedTypeSymbol, SymbolKind.NamedType);
    }

    private static void AnalyseNamedTypeSymbol(SymbolAnalysisContext context)
    {
        INamedTypeSymbol namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

        if (namedTypeSymbol.GetAttributes() is not ImmutableArray<AttributeData> { IsEmpty: false } attributes)
        {
            return;
        }

        if (namedTypeSymbol.DeclaringSyntaxReferences.Length is not 1)
        {
            return;
        }

        if (GetRelevantAttribute(attributes) is not string relevantAttribute)
        {
            return;
        }

        if (namedTypeSymbol.DeclaringSyntaxReferences[0].GetSyntax() is not TypeDeclarationSyntax declarationSyntax
            || IsPartial(declarationSyntax))
        {
            return;
        }

        Diagnostic diagnostics = Diagnostic.Create(Rule, declarationSyntax.GetLocation(), relevantAttribute, namedTypeSymbol.Name);
        context.ReportDiagnostic(diagnostics);
    }

    private static string? GetRelevantAttribute(ImmutableArray<AttributeData> attributes)
    {
        foreach (AttributeData attribute in attributes)
        {
            if (attribute.AttributeClass?.ToDisplayString() is not string candidateName)
            {
                continue;
            }

            foreach (string relevantName in RelevantAttributeNames)
            {
                if (candidateName == relevantName)
                {
                    return relevantName;
                }
            }
        }

        return null;
    }

    private static bool IsPartial(TypeDeclarationSyntax declarationSyntax)
    {
        foreach (SyntaxToken token in declarationSyntax.Modifiers)
        {
            if (isPartialModifier(token))
            {
                return true;
            }
        }

        return false;

        static bool isPartialModifier(SyntaxToken token) => token.IsKind(SyntaxKind.PartialKeyword);
    }
}
