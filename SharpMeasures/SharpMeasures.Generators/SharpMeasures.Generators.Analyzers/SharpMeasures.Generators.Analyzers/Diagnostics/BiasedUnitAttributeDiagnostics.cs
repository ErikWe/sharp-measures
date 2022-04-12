namespace SharpMeasures.Generators.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using System;
using System.Collections.Immutable;

internal static class BiasedUnitAttributeDiagnostics
{
    private static Type BiasedUnitAttributeType { get; } = typeof(GeneratedBiasedUnitAttribute);

    public static void Analyze(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol, AttributeData attribute)
    {
        TypeIsNotPartialDiagnostics.AnalyzeNamedType(context, namedTypeSymbol, BiasedUnitAttributeType);
    }
}