namespace ErikWe.SharpMeasures.SourceGenerators.Analyzers.Diagnostics;

using ErikWe.SharpMeasures.Attributes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;

public static class AttributeDiagnostics
{
    private static string ScalarQuantityAttributeFullName { get; } = typeof(ScalarQuantityAttribute).FullName;
    private static string UnitAttributeFullName { get; } = typeof(UnitAttribute).FullName;
    private static string BiasedUnitAttributeFullName { get; } = typeof(BiasedUnitAttribute).FullName;

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

            if (candidateName == ScalarQuantityAttributeFullName)
            {
                ScalarQuantityAttributeDiagnostics.Analyze(context, namedTypeSymbol, attribute);
            }
            else if (candidateName == UnitAttributeFullName)
            {
                UnitAttributeDiagnostics.Analyze(context, namedTypeSymbol, attribute);
            }
            else if (candidateName == BiasedUnitAttributeFullName)
            {
                BiasedUnitAttributeDiagnostics.Analyze(context, namedTypeSymbol, attribute);
            }
        }
    }
}
