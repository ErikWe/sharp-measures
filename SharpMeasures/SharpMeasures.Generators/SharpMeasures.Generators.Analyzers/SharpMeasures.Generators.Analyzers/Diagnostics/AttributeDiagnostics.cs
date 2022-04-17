namespace SharpMeasures.Generators.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;

public static class AttributeDiagnostics
{
    internal static void Register(AnalysisContext context)
    {
        context.RegisterSymbolAction(SearchForTypeDeclarationWithAttribute, SymbolKind.NamedType);
    }

    private static void SearchForTypeDeclarationWithAttribute(SymbolAnalysisContext context)
    {
        INamedTypeSymbol namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

        if (namedTypeSymbol.GetAttributes() is not ImmutableArray<AttributeData> { IsEmpty: false } attributes)
        {
            return;
        }

        DelegateAttributeAnalysis(context, namedTypeSymbol, attributes);
    }

    private static void DelegateAttributeAnalysis(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol, ImmutableArray<AttributeData> attributes)
    {
        foreach (AttributeData attribute in attributes)
        {
            if (attribute.AttributeClass?.ToDisplayString() is not string candidateName)
            {
                continue;
            }

            if (candidateName == typeof(GeneratedScalarQuantityAttribute).FullName)
            {
                ScalarQuantityAttributeDiagnostics.Analyze(context, namedTypeSymbol, attribute);
            }
            else if (candidateName == typeof(GeneratedUnitAttribute).FullName)
            {
                UnitAttributeDiagnostics.Analyze(context, namedTypeSymbol, attribute);
            }
        }
    }
}
