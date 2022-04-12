namespace SharpMeasures.Generators.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using System;
using System.Collections.Immutable;

internal static class ScalarQuantityAttributeDiagnostics
{
    private static Type ScalarQuantityAttributeType { get; } = typeof(GeneratedScalarQuantityAttribute);

    public static void Analyze(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol, AttributeData attribute)
    {
        TypeIsNotPartialDiagnostics.AnalyzeNamedType(context, namedTypeSymbol, ScalarQuantityAttributeType);
    }
}